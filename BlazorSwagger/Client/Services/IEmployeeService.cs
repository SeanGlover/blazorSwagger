using blazorSwagger.Models;

namespace blazorSwagger.Services
{
    public interface IEmployeeService
    {
        List<Employee> Get();
        Employee? Get(string id);
        Employee? Get(int employeeId);
        Employee Create(Employee employee);
        void Replace(string id, Employee employee);
        void Update(string id, string fieldName, object fieldValue);
        void Remove(string id);
    }
}
