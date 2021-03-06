using SqlServerGraphDb.Persistence.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerGraphDb.Persistence.Persistence
{
    public class BulkDataUploadPersistence : IBulkDataUploadPersistence
    {
        private readonly IConnectionFactory _connectionFactory;

        public BulkDataUploadPersistence(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> AddBulkData(string tableName, DataTable table)
        {
            bool dataUploadedAndCommited = false;
            using (SqlConnection connection = _connectionFactory.GetDatabaseConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
                    {
                        try
                        {
                            sqlBulkCopy.BulkCopyTimeout = 3000;
                            sqlBulkCopy.BatchSize = 500;
                            sqlBulkCopy.DestinationTableName = tableName;
                            sqlBulkCopy.EnableStreaming = true;
                            await sqlBulkCopy.WriteToServerAsync(table);
                            transaction.Commit();
                            dataUploadedAndCommited = true;
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            connection.Close();
                        }
                    }
                }
            }
            return dataUploadedAndCommited;
        }
    }
}
