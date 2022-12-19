using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Employees.Common.Converters
{
    public class NullableDateTimeConverter : ITypeConverter
    {
        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text) || string.Equals(text, "NULL", StringComparison.OrdinalIgnoreCase))
            {
                return DateTime.UtcNow;
            }

            return DateTime.Parse(text);
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData) => string.Empty;
    }
}
