using LINQtoCSV;
using System.Collections.Generic;
using System.IO;

namespace SqlServerGraphDb.CommandQueryHandler.Utilities
{
    public class CSVReader
    {
        internal static IEnumerable<T> ReadCsvFile<T>(string filePath, char separator = ',') where T : class, new()
        {
            CsvFileDescription csvFileDescription = new CsvFileDescription
            {
                SeparatorChar = separator,
                FirstLineHasColumnNames = true
            };

            CsvContext csvContext = new CsvContext();
            StreamReader streamReader = new StreamReader(filePath);
            return csvContext.Read<T>(streamReader, csvFileDescription);
        }
    }
}
