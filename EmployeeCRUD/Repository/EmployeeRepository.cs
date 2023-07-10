using EmployeeCRUD.Models;
using EmployeeCRUD.Utilities;
using Newtonsoft.Json;
using System.Net.Mail;

namespace EmployeeCRUD.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private IEmailService emailService;
        private string _jsonPath = string.Empty;

        public EmployeeRepository(IEmailService _emailService)
        {
            emailService = _emailService;
        }
        public string jsonPath
        {
            get { return this._jsonPath; }
            set { this._jsonPath = value; }
        }

        public string AddEmployee(EmployeeModel employee)
        {
            string returnMsg = string.Empty;
            EmployeeEntity employees = new EmployeeEntity();
            if (employee != null)
            {
                using (StreamReader r = new StreamReader(jsonPath))
                {
                    var employeeList = JsonConvert.DeserializeObject<EmployeeEntity>(r.ReadToEnd());
                    employees = (employeeList != null && employeeList?.employees?.Count > 0) ? employeeList : new EmployeeEntity();
                }
                if (employees != null)
                {
                    var duplicateEmp = employees?.employees?.Where(t => t.EmployeeCode == employee.EmployeeCode).FirstOrDefault();
                    if (duplicateEmp != null)
                    {
                        returnMsg = "Employee detail already exist. Cannot duplicate.";
                    }
                    employees?.employees?.Add(employee);
                    string strEmployees = JsonConvert.SerializeObject(employees, Formatting.Indented);
                    System.IO.File.WriteAllText(jsonPath, strEmployees);

                    if (employee != null)
                    {
                        List<MailAddress> lstTo = new List<MailAddress>();
                        lstTo.Add(new MailAddress(employee.EmailID));

                        ReceiverInfo receiverInfo = new ReceiverInfo()
                        {
                            From = new MailAddress("no-reply@test.com"),
                            ToAddressList = lstTo,
                            Subject = "Welcome Message",
                            BodyMessage = @"Dear "+employee.EmployeeName+", <br /> Welcome to our company <br /> Thanks & Regards, <br />Company CEO.",
                        };

                        // Send email here
                        string emailResponse = emailService.SendEmail(receiverInfo);

                        returnMsg = "Employee detail added successfully";
                        if (!string.IsNullOrEmpty(emailResponse))
                        {
                            returnMsg += "\n "+emailResponse;
                        }
                    }
                }
                else
                {
                    returnMsg = "Failed to add Employee detail";
                }
            }
            return returnMsg;
        }

        public string DeleteEmployee(string employeeCode)
        {
            string returnMsg = string.Empty;
            EmployeeEntity employees = new EmployeeEntity();
            if (!string.IsNullOrEmpty(employeeCode))
            {
                using (StreamReader r = new StreamReader(_jsonPath))
                {
                    var employeeList = JsonConvert.DeserializeObject<EmployeeEntity>(r.ReadToEnd());
                    employees = (employeeList != null && employeeList?.employees?.Count > 0) ? employeeList : new EmployeeEntity();
                    var isExist = employees?.employees?.Where(t => t.EmployeeCode == employeeCode).SingleOrDefault();
                    if (isExist != null)
                    {
                        employeeList?.employees?.RemoveAll(t => t.EmployeeCode == employeeCode);
                    }
                    else
                    {
                        returnMsg = "Employee " + employeeCode + " not found in our system.";
                    }
                }

                if (employees != null)
                {
                    string strEmployees = JsonConvert.SerializeObject(employees, Formatting.Indented);
                    System.IO.File.WriteAllText(_jsonPath, strEmployees);
                    returnMsg = "Employee " + employeeCode + " deleted successfully";
                }
                else
                {
                    returnMsg = "Failed to delete Employee detail";
                }
            }
            return returnMsg;
        }

        public EmployeeEntity GetEmployee()
        {
            EmployeeEntity employees = new EmployeeEntity();
            using (StreamReader r = new StreamReader(jsonPath))
            {
                var employeeList = JsonConvert.DeserializeObject<EmployeeEntity>(r.ReadToEnd());
                employees = (employeeList != null && employeeList?.employees?.Count > 0) ? employeeList : new EmployeeEntity();
            }
            return employees;
        }

        public EmployeeModel GetEmployeeById(string empCode)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            using (StreamReader r = new StreamReader(jsonPath))
            {
                var employeeList = JsonConvert.DeserializeObject<EmployeeEntity>(r.ReadToEnd());
                var filteredItem = employeeList?.employees?.Where(t => t.EmployeeCode == empCode).SingleOrDefault();
                if (filteredItem != null)
                {
                    employeeModel = filteredItem;
                }
                return employeeModel;
            }
        }

        public string UpdateEmployee(EmployeeModel employee)
        {
            string returnMsg = string.Empty;
            EmployeeEntity employees = new EmployeeEntity();
            if (employee != null)
            {
                using (StreamReader r = new StreamReader(jsonPath))
                {
                    var employeeList = JsonConvert.DeserializeObject<EmployeeEntity>(r.ReadToEnd());
                    if (employeeList != null && employeeList?.employees?.Count > 0)
                    {
                        employees = employeeList;
                        var updateItem = employeeList?.employees?.Where(t => t.EmployeeCode == employee.EmployeeCode).FirstOrDefault();
                        if (updateItem != null)
                        {
                            updateItem.Department = employee.Department;
                            updateItem.Designation = employee.Designation;
                            updateItem.EmployeeName = employee.EmployeeName;
                            updateItem.Location = employee.Location;
                        }
                        {
                            returnMsg = "Employee detail not exist";
                        }
                    }
                    else
                    {
                        employeeList = null;
                    }
                }

                if (employees != null)
                {
                    string strEmployees = JsonConvert.SerializeObject(employees, Formatting.Indented);
                    System.IO.File.WriteAllText(jsonPath, strEmployees);
                    returnMsg = "Employee " + employee.EmployeeCode + " updated successfully";
                }
                else
                {
                    returnMsg = "Failed to update Employee detail";
                }
            }
            return returnMsg;
        }
    }
}
