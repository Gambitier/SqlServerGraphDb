using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerGraphDb.Persistence.DataModels
{
    public class GraphNode
    {
        public string Id { get; set; }
        public int DataId { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Customer { get; set; }
        public string Detail { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
