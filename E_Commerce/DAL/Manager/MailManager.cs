using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL.Manager
{
    public class MailManager
    {
     
            public string SendEmailId(Ent_Email mld)
            {
                SmtpSection smtpSection = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;

                using (MailMessage mm = new MailMessage(smtpSection.From, mld.Email))
                {

                    mm.Subject = mld.Subject;
                    mm.Body = mld.Body;

                    mm.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = smtpSection.Network.Host;
                        smtp.EnableSsl = smtpSection.Network.EnableSsl;
                        NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCred;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Send(mm);
                    }
                }

                return "Email sent sucessfully.";
            }



            public string AdminSendMail(Ent_Email mld)
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage(mld.Email, smtpSection.From))
                {

                    mm.Subject = mld.Subject;
                    mm.Body = mld.Body;

                    mm.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = smtpSection.Network.Host;
                        smtp.EnableSsl = smtpSection.Network.EnableSsl;
                        NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCred;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Send(mm);
                    }
                }

                return "Email sent sucessfully.";
            }
    }
 
}
