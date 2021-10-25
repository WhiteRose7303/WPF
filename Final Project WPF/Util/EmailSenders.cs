using System.Net;
using System.Net.Mail;

namespace Final_Project_WPF.Util
{
    internal class EmailSenders
    {
        public static void SendEmail(string email, string first, string last, string zipcode, string phone, string password)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("hadarovadiaschoolmanagment@gmail.com", "hadarwpf1234"),
                EnableSsl = true,
            };
            var mailmessage = new MailMessage()
            {
                From = new MailAddress("hadarovadiaschoolmanagment@gmail.com"),
                Subject = "Your Registration Has Been Successfull!",
                Body = "<h1>Great you are now registerd</h1>",
                IsBodyHtml = true,
            };

            mailmessage.To.Add(email);

            smtpClient.Send(mailmessage);
        }

        public static void updateemail(string em, string first, string last, string zipcode, string phone, string password)
        {
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("hadarovadiaschoolmanagment@gmail.com", "hadarwpf1234"),
                    EnableSsl = true,
                };
                var mailmessage = new MailMessage()
                {
                    From = new MailAddress("hadarovadiaschoolmanagment@gmail.com"),
                    Subject = "Your Acount Has Been Updated!",
                    Body = "<h1>Coming Soon</h1>",
                    IsBodyHtml = true,
                };
                mailmessage.To.Add(em);

                smtpClient.Send(mailmessage);
            }
        }

        public static void SelfRegEmail(string email, string first, string last, string zipcode, string phone, string password)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("hadarovadiaschoolmanagment@gmail.com", "hadarwpf1234"),
                EnableSsl = true,
            };
            var mailmessage = new MailMessage()
            {
                From = new MailAddress("hadarovadiaschoolmanagment@gmail.com"),
                Subject = "Your Registration Has Been Successfull!",
                Body = "<h1>Great, you are now registerd! Your will need to be aproved by a system admin, please contact your system administrator to get your account aproved.</h1>",
                IsBodyHtml = true,
            };

            mailmessage.To.Add(email);

            smtpClient.Send(mailmessage);
        }

        public static void informdelete(string email, string first, string last, string zipcode, string phone, string password)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("hadarovadiaschoolmanagment@gmail.com", "hadarwpf1234"),
                EnableSsl = true,
            };
            var mailmessage = new MailMessage()
            {
                From = new MailAddress("hadarovadiaschoolmanagment@gmail.com"),
                Subject = "Your Account Has Been Deleted...",
                Body = "<h1>An admin has deleted you account, if you think it done by mistake please contact your system admin.</h1>",
                IsBodyHtml = true,
            };

            mailmessage.To.Add(email);

            smtpClient.Send(mailmessage);
        }

        public static void adminupdate(string email, string first, string last, string zipcode, string phone, string password)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("hadarovadiaschoolmanagment@gmail.com", "hadarwpf1234"),
                EnableSsl = true,
            };
            var mailmessage = new MailMessage()
            {
                From = new MailAddress("hadarovadiaschoolmanagment@gmail.com"),
                Subject = "Your Account Has Been Updated",
                Body = "<h1>An admin has aproved your account!</h1>",
                IsBodyHtml = true,
            };

            mailmessage.To.Add(email);

            smtpClient.Send(mailmessage);
        }
    }
}