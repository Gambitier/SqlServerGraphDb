using SqlServerGraphDb.CommandQueryHandler.CSVReaderModels;
using System;
using System.Linq;
using static SqlServerGraphDb.Persistence.Enums.Enums;

namespace SqlServerGraphDb.CommandQueryHandler.Utilities
{
    internal class RelationEdgeDataMapper
    {
        public static Tuple<FileType, int> ProcessNodeData(string data, char Separator = '-')
        {
            var dataList = data.Split(Separator).ToList();

            FileType fileType;
            Enum.TryParse(dataList.FirstOrDefault(), true, out fileType);

            int id;
            int.TryParse(dataList.LastOrDefault(), out id);


            return new Tuple<FileType, int>(fileType, id);
        }

        public static RelationType ProcessRelationEdgeData(string relation)
        {
            RelationType relationType;
            Enum.TryParse(relation, true, out relationType);
            return relationType;
        }
    }
}
