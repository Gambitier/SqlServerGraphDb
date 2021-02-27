using System.Threading.Tasks;

namespace SqlServerGraphDb.Persistence.Persistence
{
    public interface ITaskMetadataPersistence
    {
        Task<bool> UploadTaskFile(int taskId, int fileType, string fileName);
    }
}