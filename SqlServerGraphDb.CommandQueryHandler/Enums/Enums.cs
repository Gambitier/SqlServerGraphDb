
namespace SqlServerGraphDb.CommandQueryHandler.Enums
{
    public class Enums
    {
        public enum FileType
        {
            Job = 1,
            Operation = 2,
            Project = 3,
            Relation = 4
        }

        public enum ExecutionStatus
        {
            Success = 1, //processed all files and data inserted to db successfully
            Failed = 2, //Some error occured, =fileMissing, =invalidFileFormat-filetype
        }
    }
}
