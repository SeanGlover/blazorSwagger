using Microsoft.AspNetCore.Mvc;
using blazorSwagger.Models;
using blazorSwagger.Services;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// https://editor.swagger.io/#/
// https://code-maze.com/swagger-ui-asp-net-core-web-api/
// C:\Users\SeanGlover\source\repos\BlazorSwagger\BlazorSwagger\Server\bin\Debug\net6.0\BlazorSwagger.Server.xml

namespace blazorSwagger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeesController(IEmployeeService service) { employeeService = service; }
        private static Employee? GetEmployee(IEmployeeService service, string id)
        {
            bool isNumber = int.TryParse(id, out int value);
            if (isNumber) { return service.Get(value); }
            else { return service.Get(id); }
        }

        /// <summary>
        /// Gets the list of all Employees.
        /// </summary>
        /// <returns>The list of Employees.</returns>
        // GET: api/Employee
        [HttpGet]
        //[ProducesResponseType(List<Employee>)]
        public ActionResult<List<Employee>> Get() { return employeeService.Get(); }

        // GET api/values
        /// <summary>
        /// Returns an employee searching by id as an integer or string
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Employee
        ///     int     --> searches the employeeId
        ///     string  --> searches the Id(MongoDb)
        /// </remarks>
        /// <param name="id" example="id">The integration property ID. Events related to presentations related to this ID will be returned</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Employee> Get(string id)
        {
            Employee? employee = GetEmployee(employeeService, id);
            return employee == null ? NotFound($"Employee with Id = {id} not found") : employee;
        }

        /// <summary>
        /// Creates an Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Employee
        ///     {        
        ///       "firstName": "Mike",
        ///       "lastName": "Andrew",
        ///       "email": "Mike.Andrew@gmail.com"        
        ///     }
        /// </remarks>
        /// <param name="employee"></param>
        /// <returns>A newly created employee</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            Employee newEmployee = employeeService.Create(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        // below Patch method is just an example but doesn't make sense to use since the json field and value must be provided to update
        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPatch("{id}")]
        public ActionResult<Employee> Patch(string id, string fieldName, object fieldValue)
        {
            Employee? employeeById = GetEmployee(employeeService, id);
            if (employeeById == null) { return NotFound($"Employee with Id = {id} not found"); }
            else
            {
                employeeService.Update(employeeById.Id, fieldName, fieldValue);
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Employee> Put(string id, [FromBody] Employee employee)
        {
            Employee? employeeById = GetEmployee(employeeService, id);
            if (employeeById == null) { return NotFound($"Employee with Id = {id} not found"); }
            else { employeeService.Replace(employeeById.Id, employee); return NoContent(); }
        }

        [HttpDelete("{id}")]
        public ActionResult<Employee> Delete(string id)
        {
            Employee? employeeById = GetEmployee(employeeService, id);
            if (employeeById == null) { return NotFound($"Employee with Id = {id} not found"); }
            else { employeeService.Remove(employeeById.Id); return Ok($"Employee with Id = {id} deleted"); }
        }
    }
}
