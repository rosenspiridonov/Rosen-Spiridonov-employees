using System.Globalization;
using CsvHelper;
using Employees.Models;
using Employees.Common.ClassMaps;

namespace Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<Employee> GetEmployeesFromFile(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<EmployeeMap>();
            return csv.GetRecords<Employee>().ToList();
        }

        public List<EmployeesViewModel> GetEmployeesPair(List<Employee> employees)
        {
            var groups = employees
                .GroupBy(e => e.ProjectId)
                .ToList();

            var pairs = new Dictionary<string, List<EmployeesViewModel>>();
            var longestDuration = TimeSpan.MinValue;
            var bestPairKey = string.Empty;

            foreach (var group in groups)
            {
                var sortedEmployees = group
                    .OrderBy(g => g.DateFrom)
                    .ToList();

                for (int i = 0; i < sortedEmployees.Count - 1; i++)
                {
                    var employee1 = sortedEmployees[i];

                    for (int j = i + 1; j < sortedEmployees.Count; j++)
                    {
                        var employee2 = sortedEmployees[j];

                        var duration = employee1.DateTo - employee2.DateFrom;
                        if (duration.Days < 0)
                        {
                            continue;
                        }

                        var firstEmployeeId = Math.Min(employee1.EmpId, employee2.EmpId);
                        var secondEmployeeId = Math.Max(employee1.EmpId, employee2.EmpId);
                        var key = firstEmployeeId + "-" + secondEmployeeId;
                        if (duration > longestDuration)
                        {
                            longestDuration = duration;
                            bestPairKey = key;
                        }

                        var data = new EmployeesViewModel
                        {
                            Employee1Id = firstEmployeeId,
                            Employee2Id = secondEmployeeId,
                            ProjectId = employee1.ProjectId,
                            DaysWorked = duration.Days,
                        };

                        if (pairs.ContainsKey(key))
                        {
                            pairs[key].Add(data);
                        }
                        else
                        {
                            pairs.Add(key, new List<EmployeesViewModel>() { data });
                        }
                    }
                }
            }

            var result = pairs[bestPairKey]
                .OrderByDescending(x => x.DaysWorked)
                .ToList();

            return result;
        }
    }
}
