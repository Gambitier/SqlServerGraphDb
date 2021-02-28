using LINQtoCSV;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;
using SqlServerGraphDb.Persistence.DataModels;
using SqlServerGraphDb.Persistence.Factory;
using SqlServerGraphDb.Persistence.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
            var taskMetadata = await _taskMetadataPersistence.GetTaskMetadataByTaskId(request.TaskId);

            var fileDirectory = Path.Combine(
                _hostingEnvironment.ContentRootPath,
                _configuration["TaskUploadsFolderName"],
                $"{request.TaskId}");

            foreach(var metadata in taskMetadata)
            {
                string tableName;
                IEnumerable<GraphNode> graphNodes;
                ReadCsvFile(fileDirectory, metadata, out tableName, out graphNodes);

                DataTable table = CreateDataTable(graphNodes, request.TaskId);

                await AddDataToDb(tableName, table);
            }


            return new ExecuteTaskResponseModel();
        }

        private async Task AddDataToDb(string tableName, DataTable table)
        {
            using (var connection = _connectionFactory.GetDatabaseConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
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

        private static DataTable CreateDataTable(IEnumerable<GraphNode> graphNodes, int TaskId)
        {
            var table = new DataTable();
            table.Columns.Add("DataId", typeof(int));
            table.Columns.Add("TaskId", typeof(int));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Customer", typeof(string));
            table.Columns.Add("Detail", typeof(string));
            table.Columns.Add("Summary", typeof(string));

            foreach (var item in graphNodes)
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

        private static void ReadCsvFile(string fileDirectory, TaskMetadata metadata, out string tableName, out IEnumerable<GraphNode> graphNodes)
        {
            var filePath = Path.Combine(fileDirectory, metadata.FileName);
            var fileType = (FileType)metadata.FileType;
            tableName = fileType.ToString();
            CsvFileDescription csvFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };

            CsvContext csvContext = new CsvContext();
            StreamReader streamReader = new StreamReader(filePath);
            graphNodes = csvContext.Read<GraphNode>(streamReader, csvFileDescription);
        }
    }
}
