using SqlServerGraphDb.Persistence.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerGraphDb.CommandQueryHandler.ResponseModels.QueryResponseModels
{
    public class TaskDetailsResponseModel
    {
        public IEnumerable<TaskMetadata> TaskDetails{ get; set; }
    }
}
