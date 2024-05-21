using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMongoCollection<Employee> _employeeCollection;
        private readonly IConfiguration _configuration;

        public EmployeeController(ILogger<EmployeeController> logger, IMongoDatabase database, IConfiguration configuration)
        {
            _logger = logger;
            _employeeCollection = database.GetCollection<Employee>("Employees");
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            var result = _employeeCollection.Find(_ => true).ToList();
            return result;
        }

        [HttpPost]
        public IActionResult SaveEmployee([FromBody] Employee newEmployee)
        {
            try
            {
                _employeeCollection.InsertOne(newEmployee);
                return Ok("Employee saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving the Employee Details : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("samplepage")]
        public string SamplePage()
        {
            return "Simple Get Request is working fine";
        }

        [HttpGet]
        [Route("mongoconfigs")]
        public string GetMongoConfigs()
        {
            var userName = _configuration.GetSection("username").Value ?? "";
            var password = _configuration.GetSection("password").Value ?? "";
            var connectionString = String.Format(_configuration.GetSection("ConnectionString").Value ?? "", userName, password);

            return connectionString;
        }


    }
}