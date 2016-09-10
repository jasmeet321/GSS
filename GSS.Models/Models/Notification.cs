using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace GSS.Models.Models
{
    public class Notification
    {

        public static bool SendPasswordRetrieval(string emailAddress, string username, string password)
        {
            try
            {
                var emailMessage = new StringBuilder();
                emailMessage.Append("<br />");
                emailMessage.Append("Hello,");
                emailMessage.Append(username + "," + " as you have requested for a password recovery.");
                emailMessage.Append("<br />");
                emailMessage.Append("So your new password has been genrated <br />");
                emailMessage.Append("<br />");
                emailMessage.Append("Please find below yor login details");
                emailMessage.Append("<br />");
                emailMessage.Append("User Name:-" + " " + username);
                emailMessage.Append("<br />");
                emailMessage.Append("Password:-" + " " + password);
                emailMessage.Append("<br />");
                emailMessage.Append("<br />");
                emailMessage.Append("Thanks!!");
                emailMessage.Append("<br />");
                emailMessage.Append("Sikh Society Nazareth Pa gurdwara management comitee..");
                var email = new MailMessage { From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]) };
                email.To.Add(new MailAddress(emailAddress));
                email.Subject = "Password Recovery";
                email.Body = emailMessage.ToString();
                email.IsBodyHtml = true;

                var smtpServer = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["Host"],
                    Port = 587,
                    Credentials = new NetworkCredential("admin@gurunanaksikhsocietynazarethpa.com", "admin@123!"),
                    EnableSsl = false,
                    Timeout = 100000
                };
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtpServer.Send(email);
                return true;
            }
            catch (Exception ex)
            {
                throw ;
            }
        }


        private static string GenerateNewPassword()
        {
            const string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789#@$ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = "";
            var rnd = new Random();
            for (int i = 0; i <= 8; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }
            return strPwd;
        }

        public static string EncodePassword(string password)
        {
            //Declarations

            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public static bool SendEventUpdate(string subscribername, string eventname, string eventdetail, string username, string evendate, string subccriberemail, bool isupdate)
        {
            try
            {
                var emailMessage = new StringBuilder();
                emailMessage.Append("<br />");
                emailMessage.Append("Hello ");
                emailMessage.Append(subscribername + "," + " this is an Event update from SikhSocietyNazarethPa Gurdwara. :<br />");
                emailMessage.Append("<br />");
                emailMessage.Append("Event Details are as below :<br />");
                emailMessage.Append("Eventname :" + eventname + ": on" + " "+ evendate + ":<br />");
                emailMessage.Append("<br />");
                emailMessage.Append("Event Detail:-" + " " + eventdetail + ":<br />");
                emailMessage.Append("<br />");
                emailMessage.Append("<br />");
                emailMessage.Append("Thanks!!");
                emailMessage.Append("<br />");
                emailMessage.Append("Sikh Society Nazareth Pa Gurdwara management comitee..");
                var email = new MailMessage { From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]) };
                email.To.Add(new MailAddress(subccriberemail));
                email.Subject = eventname;
                email.Body = emailMessage.ToString();
                email.IsBodyHtml = true;

                var smtpServer = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["Host"],
                    Port = 587,
                    Credentials = new NetworkCredential("admin@gurunanaksikhsocietynazarethpa.com", "admin@123!"),
                    EnableSsl = false,
                    Timeout = 100000
                };
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtpServer.Send(email);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}