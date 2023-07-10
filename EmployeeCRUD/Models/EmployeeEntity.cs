namespace EmployeeCRUD.Models
{
    public class EmployeeEntity
    {
        public List<EmployeeModel>? employees { get; set; }
    }

    public class EmployeeModel
    {
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public string? Location { get; set; }
        public string? EmailID { get; set; }
    }
}
