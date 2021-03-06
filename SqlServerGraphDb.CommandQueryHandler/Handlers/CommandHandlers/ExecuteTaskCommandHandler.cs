using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SqlServerGraphDb.CommandQueryHandler.CSVReaderModels;
using SqlServerGraphDb.CommandQueryHandler.RequestModels.CommandRequestModels;
using SqlServerGraphDb.CommandQueryHandler.ResponseModels.CommandResponseModels;
using SqlServerGraphDb.CommandQueryHandler.Utilities;
using SqlServerGraphDb.Persistence.DataModels;
using SqlServerGraphDb.Persistence.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static SqlServerGraphDb.Persistence.Enums.Enums;

namespace SqlServerGraphDb.CommandQueryHandler.Handlers.CommandHandlers
{
    public class ExecuteTaskCommandHandler : IRequestHandler<ExecuteTaskRequestModel, ExecuteTaskResponseModel>
    {
        private readonly IBulkDataUploadPersistence _bulkDataUploadPersistence;
        private readonly ITaskMetadataPersistence _taskMetadataPersistence;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public ExecuteTaskCommandHandler(
            IBulkDataUploadPersistence bulkDataUploadPersistence,
            ITaskMetadataPersistence taskMetadataPersistence,
            IWebHostEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _bulkDataUploadPersistence = bulkDataUploadPersistence;
            _taskMetadataPersistence = taskMetadataPersistence;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public async Task<ExecuteTaskResponseModel> Handle(ExecuteTaskRequestModel request, CancellationToken cancellationToken)
        {
            IEnumerable<TaskMetadata> taskMetadata = await _taskMetadataPersistence.GetTaskMetadataByTaskId(request.TaskId);
            
            if (taskMetadata != null)
            {
                return new ExecuteTaskResponseModel
                {
                    Success = false,
                    Message = "Please upload files first",
                };
            }

            string fileDirectory = Path.Combine(
                _hostingEnvironment.ContentRootPath,
                _configuration["TaskUploadsFolderName"],
                $"{request.TaskId}");

            foreach (TaskMetadata metadata in taskMetadata)
            {
                FileType fileType = (FileType)metadata.FileType;
                string filePath = Path.Combine(fileDirectory, metadata.FileName);

                switch (fileType)
                {
                    case FileType.Job:
                        await ProcessNode(request, fileType, filePath);
                        break;
                    case FileType.Operation:
                        await ProcessNode(request, fileType, filePath);
                        break;
                    case FileType.Project:
                        await ProcessNode(request, fileType, filePath);
                        break;
                    case FileType.Relation:
                        await ProcessRelationshipEdge(request, fileType, filePath);
                        break;
                    default:
                        break;
                }
            }

            return new ExecuteTaskResponseModel();
        }

        private async Task ProcessRelationshipEdge(ExecuteTaskRequestModel request, FileType fileType, string filePath)
        {
            IEnumerable<RelationCSVReaderModel> csvData = CSVReader.ReadCsvFile<RelationCSVReaderModel>(filePath);
            DataTable relationTable = GetRelationDataTable(csvData, request.TaskId);

            // need to use dataInsertSuccess for returning response to api
            bool dataInsertSuccess = await _bulkDataUploadPersistence.AddBulkData(fileType.ToString(), relationTable);
        }

        private async Task ProcessNode(ExecuteTaskRequestModel request, FileType fileType, string filePath)
        {
            IEnumerable<GraphNode> graphNodes = CSVReader.ReadCsvFile<GraphNode>(filePath);
            DataTable nodeDataTable = GetNodeDataTable(graphNodes, request.TaskId);

            // need to use dataInsertSuccess for returning response to api
            bool dataInsertSuccess = await _bulkDataUploadPersistence.AddBulkData(fileType.ToString(), nodeDataTable);
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
