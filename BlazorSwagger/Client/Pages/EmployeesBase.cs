using Microsoft.AspNetCore.Components;

namespace BlazorSwagger.Client.Pages
{
    public class EmployeesBase : ComponentBase
    {
        //readonly IEmployeeService employeeService;
        public EmployeesBase()
        {
            //Employees.AddRange(EmployeeService.Employees);
            //var settings=new MongoClient(builder.Configuration.GetValue<string>("EmployeeStoreDatabaseSettings:ConnectionString"))
            //var database = MongoClient.GetDatabase(settings.DatabaseName);
            //var _employees = database.GetCollection<Employee>(settings.EmployeesCollectionName);
            //Employees.AddRange();
        }
        //public EmployeesBase(IEmployeeService service) { employeeService = service; }
        //public List<Employee> Employees
        //{
        //    get
        //    {
        //        List<Employee> employees = new();
        //        foreach (Employee employee in employeeService.Get()) { employees.Add(employee); }
        //        return employees;
        //    }
        //}
    }
}
