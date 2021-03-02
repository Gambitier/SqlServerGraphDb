
namespace SqlServerGraphDb.Persistence.Enums
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

        public enum FileProcessingStatus
        {
            Uploaded = 1, // still can modify the file
            Failed = 2, // so user need to modify file to correct file
            Success = 3, //once file processing is success, dont let user upload another file
            Inprogress = 4, //no modification allowed
        }

        public enum RelationType
        {
            ParentChild = 1,
            Attachment  = 2,
        }
    }
}
