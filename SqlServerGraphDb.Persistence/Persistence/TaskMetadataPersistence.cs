using Dapper;
using SqlServerGraphDb.Persistence.Constants;
using SqlServerGraphDb.Persistence.DataModels;
using SqlServerGraphDb.Persistence.Factory;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static SqlServerGraphDb.Persistence.Enums.Enums;

namespace SqlServerGraphDb.Persistence.Persistence
{
    class TaskMetadataPersistence : ITaskMetadataPersistence
    {
        private readonly IConnectionFactory _connectionFactory;

        public TaskMetadataPersistence(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<TaskMetadata>> GetTaskMetadataByTaskId(int taskId)
        {
            var connection = _connectionFactory.GetDatabaseConnection();
            string query = StoredProcedureConstants.spTaskMetadata_GetAllByTaskId;
            var response = await connection
                .QueryAsync<TaskMetadata>(query, new { TaskId = taskId }, null, null, CommandType.StoredProcedure);

            return response;
        }

        public async Task<bool> UploadTaskFile(int taskId, int fileType, string fileName)
        {
            var connection = _connectionFactory.GetDatabaseConnection();
            string query = StoredProcedureConstants.spTaskMetada_Create;

            var param = new DynamicParameters();
            param.Add("TaskId", taskId);
            param.Add("FileType", fileType);
            param.Add("FileName", fileName);
            param.Add("FileProcessingStatus", (int)FileProcessingStatus.Uploaded);
            param.Add("NewRecordInserted", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(query, param, null, null, CommandType.StoredProcedure);
            var NewRecordInserted = param.Get<bool>("NewRecordInserted");
            return NewRecordInserted;
        }
    }
}
