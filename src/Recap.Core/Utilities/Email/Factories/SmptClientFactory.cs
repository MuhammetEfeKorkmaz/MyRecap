using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Recap.Core.Entities.Concrete;
using System.Net;
using System.Net.Mail;

namespace Recap.Core.Utilities.Email.Factories
{
    public class SmptClientFactory : IEmailManager
    {
        EmailModel emailModel = new EmailModel();
        IHttpContextAccessor httpContextAccessor;
        public SmptClientFactory(string _configurationModelJson)
        {
            emailModel = JsonConvert.DeserializeObject<EmailModel>(_configurationModelJson);
            httpContextAccessor = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }
       
        public void SenderDeveloper(CustomExceptionExtensionModel model)
        {
            var UserId_ = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserId").Value;
            var UserName = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserName").Value;
            var UserEmail = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserEmail").Value;

            int UserId = 0;
            int.TryParse(UserId_, out UserId);

            emailModel.Subject = $"Uygulama Hatası Düzey:{model.Level}, Kullanıcı Adı:{UserName}, Kullanıcı Email: {UserEmail}";
            emailModel.Body = $"Kullanıcı Id:{UserId}{Environment.NewLine}Kullanıcı Adı:{UserName}{Environment.NewLine}Kullanıcı Email:{UserEmail}{Environment.NewLine}" +
                $"Öncelik Kodu:{model.Level}{Environment.NewLine}{Environment.NewLine}" +
                $"Mesaj:{model.ErrorMessage}{Environment.NewLine}{Environment.NewLine}" +
                $"StackTrace:{model.ErrorStackTrace}{Environment.NewLine}{Environment.NewLine}" +
                $"Detay:{model.ErrorDetail}{Environment.NewLine}{Environment.NewLine}";

            SmtpClientBase(emailModel);

        }
        public string SenderDeveloperWithTryCache(CustomExceptionExtensionModel model)
        {
            try
            {
                var UserId_ = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserId").Value;
                var UserName = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserName").Value;
                var UserEmail = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserEmail").Value;

                int UserId = 0;
                int.TryParse(UserId_, out UserId);

                emailModel.Subject = $"Uygulama Hatası Düzey:{model.Level}, Kullanıcı Adı:{UserName}, Kullanıcı Email: {UserEmail}";
                emailModel.Body = $"Kullanıcı Id:{UserId}{Environment.NewLine}Kullanıcı Adı:{UserName}{Environment.NewLine}Kullanıcı Email:{UserEmail}{Environment.NewLine}" +
                    $"Öncelik Kodu:{model.Level}{Environment.NewLine}{Environment.NewLine}" +
                    $"Mesaj:{model.ErrorMessage}{Environment.NewLine}{Environment.NewLine}" +
                    $"StackTrace:{model.ErrorStackTrace}{Environment.NewLine}{Environment.NewLine}" +
                    $"Detay:{model.ErrorDetail}{Environment.NewLine}{Environment.NewLine}";

                return SmtpClientBaseWithTryCache(emailModel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        public void SenderUser(string RecipientUserEmail, string RecipientUserName, string RecipientUserSurName, string RecipientUserPassword)
        {
            emailModel.Body = $"Merhaba Sayın {RecipientUserName} {RecipientUserSurName};";
            emailModel.Body = emailModel.Body + Environment.NewLine;
            emailModel.Body = emailModel.Body + $"Uygulama Şifreniz:{RecipientUserPassword}";
            emailModel.Body = emailModel.Body + Environment.NewLine;
            emailModel.Body = emailModel.Body + "İyi Günler Dileriz.";

            emailModel.Subject = "Sersim Bilgi Teknolojileri Uygulama Şifreniz Sistem Tarafından Gönderilmiştir.";

            foreach (var item in emailModel.CCList)
                emailModel.CCList.Add(item);
            emailModel.CCList.Clear();

            emailModel.TOList.Clear();
            emailModel.TOList.Add(RecipientUserEmail);

            SmtpClientBase(emailModel);
        }
        public string SenderUserWithTryCache(string RecipientUserEmail, string RecipientUserName, string RecipientUserSurName, string RecipientUserPassword)
        {
            try
            {
                emailModel.Body = $"Merhaba Sayın {RecipientUserName} {RecipientUserSurName};";
                emailModel.Body = emailModel.Body + Environment.NewLine;
                emailModel.Body = emailModel.Body + $"Uygulama Şifreniz:{RecipientUserPassword}";
                emailModel.Body = emailModel.Body + Environment.NewLine;
                emailModel.Body = emailModel.Body + "İyi Günler Dileriz.";

                emailModel.Subject = "Sersim Bilgi Teknolojileri Uygulama Şifreniz Sistem Tarafından Gönderilmiştir.";

                foreach (var item in emailModel.CCList)
                    emailModel.CCList.Add(item);
                emailModel.CCList.Clear();

                emailModel.TOList.Clear();
                emailModel.TOList.Add(RecipientUserEmail);

                return SmtpClientBaseWithTryCache(emailModel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }



        private void SmtpClientBase(EmailModel model)
        {

            using (SmtpClient smtp = new SmtpClient())
            {
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(model.EmailFrom);
                message.From = fromAddress;

                for (int i = 0; i < model.CCList.Count; i++)
                    message.CC.Add(model.CCList[i]);

                for (int i = 0; i < model.TOList.Count; i++)
                    message.To.Add(model.TOList[i]);

                for (int i = 0; i < model.BCCList.Count; i++)
                    message.Bcc.Add(model.BCCList[i]);


                message.Subject = model.Subject;
                message.IsBodyHtml = true;
                message.Body = model.Body;

                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                smtp.Port = model.Port;
                smtp.Host = model.HostName;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(model.EmailFrom, model.Password, model.HostName);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }

        }
        private string SmtpClientBaseWithTryCache(EmailModel model)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    MailMessage message = new MailMessage();
                    MailAddress fromAddress = new MailAddress(model.EmailFrom);
                    message.From = fromAddress;

                    for (int i = 0; i < model.CCList.Count; i++)
                        message.CC.Add(model.CCList[i]);

                    for (int i = 0; i < model.TOList.Count; i++)
                        message.To.Add(model.TOList[i]);

                    for (int i = 0; i < model.BCCList.Count; i++)
                        message.Bcc.Add(model.BCCList[i]);


                    message.Subject = model.Subject;
                    message.IsBodyHtml = true;
                    message.Body = model.Body;

                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.High;

                    smtp.Port = model.Port;
                    smtp.Host = model.HostName;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(model.EmailFrom, model.Password, model.HostName);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
