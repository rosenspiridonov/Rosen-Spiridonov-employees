using CsvHelper.Configuration;

using Employees.Common.Converters;
using Employees.Models;

namespace Employees.Common.ClassMaps
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Map(e => e.EmpId);
            Map(e => e.ProjectId);
            Map(e => e.DateFrom).TypeConverter<DateTimeTypeConverter>();
            Map(e => e.DateTo).TypeConverter<NullableDateTimeTypeConverter>();
        }
    }
}
