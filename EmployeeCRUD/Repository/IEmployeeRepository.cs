using EmployeeCRUD.Models;

namespace EmployeeCRUD.Repository
{
    public interface IEmployeeRepository
    {
        string jsonPath { get; set; }
        string AddEmployee(EmployeeModel employee);
        EmployeeEntity GetEmployee();
        EmployeeModel GetEmployeeById(string empCode);
        string UpdateEmployee(EmployeeModel employee);
        string DeleteEmployee(string employeeCode);
    }
}
