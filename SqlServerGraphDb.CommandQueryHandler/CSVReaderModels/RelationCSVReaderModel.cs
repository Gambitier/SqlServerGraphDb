using static SqlServerGraphDb.Persistence.Enums.Enums;

namespace SqlServerGraphDb.CommandQueryHandler.CSVReaderModels
{
    internal class RelationCSVReaderModel
    {
        public string Origin { get; set; }
        public string Target { get; set; }
        public string Relation { get; set; }
    }
}
