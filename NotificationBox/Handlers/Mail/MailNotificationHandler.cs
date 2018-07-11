using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotificationBox.Handlers.Mail
{
    public class MailNotificationHandler : BaseNotificationHandler, INotificationHandler<MailMessage>
    {
        public override Task<Tuple<bool, string>> SendMessage(IMessage message)
        {
            return SendMessage(message as MailMessage);
        }

        public async Task<Tuple<bool, string>> SendMessage(MailMessage message)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient(message.Host, message.Port))
                {
                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(message.From, message.To))
                    {
                        mail.Subject = message.Subject;
                        mail.IsBodyHtml = message.IsHTML;
                        mail.Body = message.Message;

                        await smtp.SendMailAsync(mail);

                        return new Tuple<bool, string>(true, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, $"Failed to mail ({ex.Message}).");
            }
        }
    }
}
