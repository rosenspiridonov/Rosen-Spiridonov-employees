using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using Employees.Models;
using Employees.Services;

namespace Employees.Controllers
{
    public class HomeController : Controller
    {
        private const string INVALID_FILE_FORMAT_MSG = "Format not supported. Please select a CSV file.";
        private const string INVALID_FILE_MSG = "Invalid CSV file.";
        private const string INVALID_DATE_MSG = "Invalid date.";

        private readonly IEmployeeService employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (!file.FileName.ToLower().EndsWith(".csv")) 
            {
                ViewData["File error"] = INVALID_FILE_FORMAT_MSG;
                return View("Index");
            }

            var records = new List<Employee>();

            try
            {
                records = this.employeeService.GetEmployeesFromFile(file).ToList();
            }
            catch (HeaderValidationException)
            {
                ViewData["File error"] = INVALID_FILE_MSG;
                return View("Index");
            }
            catch (ReaderException)
            {
                ViewData["File error"] = INVALID_DATE_MSG;
                return View("Index");
            }
            
            var result = this.employeeService.GetEmployeesPair(records);
            
            return this.View("Results", result);
        }
    }
}
