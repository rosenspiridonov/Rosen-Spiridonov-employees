using System.Globalization;

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Employees.Common.Converters
{
    public class DateTimeTypeConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) 
            => DateTime.Parse(text, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal);

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) 
            => ((DateTime)value).ToString("o", CultureInfo.InvariantCulture);
    }
}
