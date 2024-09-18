namespace blazorSwagger.Models
{
    public interface IEmployeeStoreDatabaseSettings
    {
        string EmployeesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
