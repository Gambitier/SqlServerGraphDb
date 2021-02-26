using Dapper;
using SqlServerGraphDb.Persistence.Constants;
using SqlServerGraphDb.Persistence.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerGraphDb.Persistence.Persistence
{
    class TaskPersistence : ITaskPersistence
    {
        private readonly IConnectionFactory _connectionFactory;

        public TaskPersistence(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateTaskByName(string TaskName)
        {
            var connection = _connectionFactory.GetDatabaseConnection();
            string query = StoredProcedureConstants.spTask_CreateTaskByName;
            int id = await connection.ExecuteScalarAsync<int>(query, new { Name = TaskName }, null, null, CommandType.StoredProcedure);
            return id;
        }
    }
}
