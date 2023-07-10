using EmployeeCRUD.Models;
using System.Net;
using System.Net.Mail;

namespace EmployeeCRUD.Utilities
{
    
    public class EmailService : IEmailService
    {
        SenderInfo sendInfo;
        public EmailService()
        {
            sendInfo = new SenderInfo() {
            EnableSSL = true,
            HostName= "smtp.gmail.com",
            PortNo=587,
            UserName = "hariharan301test@gmail.com",
            Password= "cpttnncjttimnbsm"
            };
        }

        public string SendEmail(ReceiverInfo recInfo)
        {
            string returnMsg;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = recInfo.From;
            foreach (MailAddress toAddress in recInfo.ToAddressList)
            {
                mailMessage.To.Add(toAddress);
            }
            if (recInfo.CCAddressList != null)
            {
                foreach (MailAddress ccAddress in recInfo.CCAddressList)
                {
                    mailMessage.CC.Add(ccAddress);
                }
            }
            mailMessage.Subject = recInfo.Subject;
            mailMessage.Body = recInfo.BodyMessage;

            SmtpClient clientMail = new SmtpClient();
            clientMail.UseDefaultCredentials = false;
            clientMail.Port = sendInfo.PortNo;
            clientMail.EnableSsl = sendInfo.EnableSSL;
            clientMail.Host = sendInfo.HostName;
            clientMail.Credentials = new NetworkCredential(sendInfo.UserName, sendInfo.Password);
            clientMail.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                clientMail.Send(mailMessage);
                returnMsg = "Mail sent successfully";
            }
            catch (Exception ex)
            {
                returnMsg = "Failed to send email";
                Console.WriteLine(ex.Message.ToString());
            }
            return returnMsg;
        }
    }
}
