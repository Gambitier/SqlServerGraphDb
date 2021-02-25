using System.Threading.Tasks;

namespace SqlServerGraphDb.Persistence.Persistence
{
    public interface ITaskPersistence
    {
        Task<int> CreateTaskByName(string TaskName);
    }
}