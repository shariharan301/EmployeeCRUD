using EmployeeCRUD.Models;

namespace EmployeeCRUD.Utilities
{
    public interface IEmailService
    {
        string SendEmail(ReceiverInfo recInfo);
    }
}
