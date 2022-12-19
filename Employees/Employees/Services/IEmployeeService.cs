using Employees.Models;

namespace Employees.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployeesFromFile(IFormFile file);

        List<EmployeesViewModel> GetEmployeesPair(List<Employee> employees);
    }
}
