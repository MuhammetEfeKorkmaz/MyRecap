using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Recap.Core.Entities.Concrete;

namespace Recap.Core.Utilities.ErrorLogger.Factories
{

    public class NoteFactory : IErrorLoggerLocalManager
    {
        ErrorLoggerModel errorLoggerModel;
        IHttpContextAccessor httpContextAccessor;
        public NoteFactory()
        {
            httpContextAccessor = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            //httpContextAccessor.HttpContext.User.AddIdentity(new ClaimsIdentity("UserInfo", "Id", "1"));


        }
        public void LocalSave(CustomExceptionExtensionModel model)
        { 
            var UserId_ = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserId").Value;
            var UserName = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserName").Value;
            var UserEmail = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserEmail").Value;

            int UserId = 0;
            int.TryParse(UserId_, out UserId);
            errorLoggerModel.UserId = UserId;
            errorLoggerModel.UserName = UserName;
            errorLoggerModel.UserEmail = UserEmail;


            string path = Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName;
            path = Path.Combine(path, "Logs");
            path = Path.Combine(path, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string test = Path.Combine(path, $"{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.txt");
            int sayac = 1;
            while (File.Exists(test))
            {
                test = Path.Combine(path, $"{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{sayac}.txt");
                sayac++;
            }

            errorLoggerModel.ErrorMessage = model.ErrorMessage;
            errorLoggerModel.ErrorStackTrace = model.ErrorStackTrace;
            errorLoggerModel.ErrorDetail = model.ErrorDetail;
            errorLoggerModel.Level = model.Level;

            using (StreamWriter sw = File.CreateText(test))
            {
                sw.WriteLine($"UserId: { errorLoggerModel.UserId}");
                sw.WriteLine($"UserName: { errorLoggerModel.UserName}");
                sw.WriteLine($"UserEmail: { errorLoggerModel.UserEmail}");
                sw.WriteLine($"ErrorDetail: { errorLoggerModel.ErrorDetail}");
                sw.WriteLine($"ErrorMessage: { errorLoggerModel.ErrorMessage}");
                sw.WriteLine($"ErrorStackTrace: { errorLoggerModel.ErrorStackTrace}");
                sw.WriteLine($"Level: { errorLoggerModel.Level}");
            }
             
        }

        public string LocalSaveWithTryCache(CustomExceptionExtensionModel model)
        {
            try
            {
                var UserId_ = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserId").Value;
                var UserName = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserName").Value;
                var UserEmail = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserEmail").Value;

                int UserId = 0;
                int.TryParse(UserId_, out UserId);
                errorLoggerModel.UserId = UserId;
                errorLoggerModel.UserName = UserName;
                errorLoggerModel.UserEmail = UserEmail;


                string path = Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName;
                path = Path.Combine(path, "Logs");
                path = Path.Combine(path, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string test = Path.Combine(path, $"{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.txt");
                int sayac = 1;
                while (File.Exists(test))
                {
                    test = Path.Combine(path, $"{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{sayac}.txt");
                    sayac++;
                }

                errorLoggerModel.ErrorMessage = model.ErrorMessage;
                errorLoggerModel.ErrorStackTrace = model.ErrorStackTrace;
                errorLoggerModel.ErrorDetail = model.ErrorDetail;
                errorLoggerModel.Level = model.Level;

                using (StreamWriter sw = File.CreateText(test))
                {
                    sw.WriteLine($"UserId: { errorLoggerModel.UserId}");
                    sw.WriteLine($"UserName: { errorLoggerModel.UserName}");
                    sw.WriteLine($"UserEmail: { errorLoggerModel.UserEmail}");
                    sw.WriteLine($"ErrorDetail: { errorLoggerModel.ErrorDetail}");
                    sw.WriteLine($"ErrorMessage: { errorLoggerModel.ErrorMessage}");
                    sw.WriteLine($"ErrorStackTrace: { errorLoggerModel.ErrorStackTrace}");
                    sw.WriteLine($"Level: { errorLoggerModel.Level}");
                }
                return String.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
