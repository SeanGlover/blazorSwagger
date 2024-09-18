using blazorSwagger.Models;
using MongoDB.Driver;
namespace blazorSwagger.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        public static readonly List<Employee> Employees = new();
        public EmployeeService(IEmployeeStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _employees = database.GetCollection<Employee>(settings.EmployeesCollectionName);
            foreach (var employee in _employees.Find(employee => true).ToList()) { Employees.Add(employee); }
        }
        public Employee Create(Employee employee)
        {
            _employees.InsertOne(employee);
            return employee;
        }
        public List<Employee> Get() { return _employees.Find(employee => true).ToList(); }
        public Employee? Get(string id)
        {
            List<Employee> employees = Get();
            Employee? employee = employees.Where(e => e.Id == id).FirstOrDefault();
            return employee;
        }
        public Employee Get(int employeeId) { return _employees.Find(employee => employee.EmployeeId == employeeId).FirstOrDefault(); }
        public void Remove(string id) { _employees.DeleteOne(employee => employee.Id == id); }
        public void Replace(string id, Employee employee) { _employees.ReplaceOne(emp => emp.Id == id, employee); }
        public void Update(string id, string fieldName, object fieldValue)
        {
            var filter = Builders<Employee>.Filter.Eq(s => s.Id, id);
            var update = Builders<Employee>.Update.Set(fieldName, fieldValue);
            var result = _employees.UpdateOne(filter, update);
            //return result.IsAcknowledged;
        }
    }
}
