namespace blazorSwagger.Models
{
    public class EmployeeStoreDatabaseSettings : IEmployeeStoreDatabaseSettings
    {
        public string EmployeesCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
