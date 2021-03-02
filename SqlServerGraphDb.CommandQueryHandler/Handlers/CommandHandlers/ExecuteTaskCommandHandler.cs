using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SqlServerGraphDb.CommandQueryHandler.CSVReaderModels;
using SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;
using SqlServerGraphDb.CommandQueryHandler.Utilities;
using SqlServerGraphDb.Persistence.DataModels;
using SqlServerGraphDb.Persistence.Factory;
using SqlServerGraphDb.Persistence.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static SqlServerGraphDb.Persistence.Enums.Enums;

namespace SqlServerGraphDb.CommandQueryHandler.Handlers.CommandHandlers
{
    public class ExecuteTaskCommandHandler : IRequestHandler<ExecuteTaskRequestModel, ExecuteTaskResponseModel>
    {
        private IConnectionFactory _connectionFactory;
        private ITaskMetadataPersistence _taskMetadataPersistence;
        private readonly IWebHostEnvironment _hostingEnvironment;
        IConfiguration _configuration;

        public ExecuteTaskCommandHandler(
            IConnectionFactory connectionFactory,
            ITaskMetadataPersistence taskMetadataPersistence,
            IWebHostEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _connectionFactory = connectionFactory;
            _taskMetadataPersistence = taskMetadataPersistence;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public async Task<ExecuteTaskResponseModel> Handle(ExecuteTaskRequestModel request, CancellationToken cancellationToken)
        {
            IEnumerable<TaskMetadata> taskMetadata = await _taskMetadataPersistence.GetTaskMetadataByTaskId(request.TaskId);

            string fileDirectory = Path.Combine(
                _hostingEnvironment.ContentRootPath,
                _configuration["TaskUploadsFolderName"],
                $"{request.TaskId}");

            foreach (TaskMetadata metadata in taskMetadata)
            {
                FileType fileType = (FileType)metadata.FileType;
                string filePath = Path.Combine(fileDirectory, metadata.FileName);

                if (metadata.FileType == (int)FileType.Job
                    || metadata.FileType == (int)FileType.Operation
                    || metadata.FileType == (int)FileType.Project)
                {
                    IEnumerable<GraphNode> graphNodes = CSVReader.ReadCsvFile<GraphNode>(filePath);
                    DataTable nodeDataTable = GetNodeDataTable(graphNodes, request.TaskId);
                    await AddBulkData(fileType.ToString(), nodeDataTable);
                }
                else if (metadata.FileType == (int)FileType.Relation)
                {
                    IEnumerable<RelationCSVReaderModel> csvData = CSVReader.ReadCsvFile<RelationCSVReaderModel>(filePath);
                    DataTable relationTable = GetRelationDataTable(csvData, request.TaskId);
                    await AddBulkData(fileType.ToString(), relationTable);
                }
            }

            return new ExecuteTaskResponseModel();
        }

        private DataTable GetRelationDataTable(IEnumerable<RelationCSVReaderModel> csvData, int TaskId)
        {
            DataTable table = new DataTable();
            table.Columns.Add("TaskId", typeof(int));
            table.Columns.Add("FromDataId", typeof(int));
            table.Columns.Add("FromTableEnum", typeof(int));
            table.Columns.Add("ToDataId", typeof(int));
            table.Columns.Add("ToTableEnum", typeof(int));
            table.Columns.Add("RelationEnum", typeof(int));

            foreach (RelationCSVReaderModel item in csvData)
            {
                Tuple<FileType, int> originData = RelationEdgeDataMapper.ProcessNodeData(item.Origin);
                Tuple<FileType, int> targetData = RelationEdgeDataMapper.ProcessNodeData(item.Target);
                RelationType relationType = RelationEdgeDataMapper.ProcessRelationEdgeData(item.Relation);

                table.Rows.Add(new object[] {
                    TaskId,
                    originData.Item2,
                    (int)originData.Item1,
                    targetData.Item2,
                    (int)targetData.Item1,
                    (int)relationType
                });
            }

            return table;
        }

        private async Task AddBulkData(string tableName, DataTable table)
        {
            using (SqlConnection connection = _connectionFactory.GetDatabaseConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            sqlBulkCopy.BulkCopyTimeout = 3000;
                            sqlBulkCopy.BatchSize = 500;
                            sqlBulkCopy.DestinationTableName = tableName;
                            sqlBulkCopy.EnableStreaming = true;
                            await sqlBulkCopy.WriteToServerAsync(table);
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            connection.Close();
                        }
                    }
                }
            }
        }

        private static DataTable GetNodeDataTable(IEnumerable<GraphNode> graphNodes, int TaskId)
        {
            DataTable table = new DataTable();
            table.Columns.Add("DataId", typeof(int));
            table.Columns.Add("TaskId", typeof(int));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Customer", typeof(string));
            table.Columns.Add("Detail", typeof(string));
            table.Columns.Add("Summary", typeof(string));

            foreach (GraphNode item in graphNodes)
            {
                table.Rows.Add(new object[] {
                    item.DataId,
                    TaskId,
                    item.Title,
                    item.Customer,
                    item.Detail,
                    item.Summary
                });
            }

            return table;
        }
    }
}
