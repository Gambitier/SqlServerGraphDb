using SqlServerGraphDb.Persistence.DataModels;
using System.Collections.Generic;

namespace SqlServerGraphDb.CommandQueryHandler.ResponseModels.QueryResponseModels
{
    public class TaskDetailsResponseModel
    {
        public IEnumerable<TaskMetadata> TaskDetails{ get; set; }
    }
}
