using System.Data;
using System.Threading.Tasks;

namespace SqlServerGraphDb.Persistence.Persistence
{
    public interface IBulkDataUploadPersistence
    {
        Task<bool> AddBulkData(string tableName, DataTable table);
    }
}