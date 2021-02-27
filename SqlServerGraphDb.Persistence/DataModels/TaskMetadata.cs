using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerGraphDb.Persistence.DataModels
{
    public class TaskMetadata
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int FileType { get; set; }
        public string FileName { get; set; }
        public int FileProcessingStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
