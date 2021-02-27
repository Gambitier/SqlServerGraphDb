using SqlServerGraphDb.Persistence.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerGraphDb.Persistence.Persistence
{
    public interface ITaskMetadataPersistence
    {
        Task<bool> UploadTaskFile(int taskId, int fileType, string fileName);

        Task<IEnumerable<TaskMetadata>> GetTaskMetadataByTaskId(int taskId);
    }
}