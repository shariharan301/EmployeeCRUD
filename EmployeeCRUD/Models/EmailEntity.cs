using System.Net.Mail;

namespace EmployeeCRUD.Models
{
    public class EmailEntity
    {
    }

    public class ReceiverInfo
    {
        public MailAddress? From { get; set; }
        public List<MailAddress>? ToAddressList { get; set; }
        public List<MailAddress>? CCAddressList { get; set; }
        public string? Subject { get; set; }
        public string? BodyMessage { get; set; }
    }

    public class SenderInfo
    {
        public string? HostName { get; set; }
        public int PortNo { get; set; }
        public bool EnableSSL { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
