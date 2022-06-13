using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Recap.Core.Entities.Concrete;
using Recap.Core.Utilities.Email;
using Recap.Core.Utilities.ErrorLogger;
using System.Net;
using System.Text;

namespace Recap.Core.Extensions.PacketCustomException
{

    public class CustomExceptionExtension
    {
        private RequestDelegate _next;

        public CustomExceptionExtension(RequestDelegate next)
        {
            _next = next;
        }
      
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            { 
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }

        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            IEnumerable<ValidationFailure> errors;
            CustomExceptionExtensionModel returnModel = new CustomExceptionExtensionModel();


            #region Critical olmayan hatalar
            if (e.GetType() == typeof(ValidationException))
            {
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400;

                StringBuilder st = new StringBuilder();
                foreach (var item in errors)
                    st.AppendLine($"PropertyName: {item.PropertyName},  ErrorMessage: {item.ErrorMessage}");

                returnModel.ErrorMessage = e.Message;
                returnModel.ErrorStackTrace = e.StackTrace;
                returnModel.ErrorDetail = st.ToString();
                returnModel.Level = 1;
                EkIslemler(returnModel);
                return httpContext.Response.WriteAsync(returnModel.ToString());
            }
            returnModel.ErrorMessage = e.Message;
            returnModel.ErrorStackTrace = e.StackTrace;
            if (e.Message == "!Yetkiniz Yok")
            {
                returnModel.ErrorStackTrace = string.Empty;
                returnModel.ErrorMessage = "Yetkiniz Yok";
                returnModel.Level = 1;
                httpContext.Response.StatusCode = 401;
                return httpContext.Response.WriteAsync(returnModel.ToString());
            }
            if (e.Message.StartsWith("!"))
            {
                returnModel.Level = 1;
                httpContext.Response.StatusCode = 401;
                return httpContext.Response.WriteAsync(returnModel.ToString());
            }
            #endregion



            #region Critical Hatalar  loglanacak
            string UserId_ = string.Empty;
            string UserName = string.Empty;
            string UserEmail = string.Empty;
            try
            {
                UserId_ = httpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserId").Value;
                UserName = httpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserName").Value;
                UserEmail = httpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserEmail").Value;
            }
            catch
            {
                returnModel.Level = 1;
                httpContext.Response.StatusCode = 500;
                return httpContext.Response.WriteAsync(returnModel.ToString());
            }

            returnModel.Level = 2;
            httpContext.Response.StatusCode = 500;
            EkIslemler(returnModel);
            return httpContext.Response.WriteAsync(returnModel.ToString());
            #endregion


        }
        private void EkIslemler(CustomExceptionExtensionModel model)
        {
            string LogDbReturn = string.Empty;
            string emailReturn = string.Empty;
            IErrorLoggerDBManager errorLoggerDBManager = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IErrorLoggerDBManager>();
            IErrorLoggerLocalManager errorLoggerLocalManager = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IErrorLoggerLocalManager>();
            IEmailManager emailManager = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IEmailManager>();


            if (errorLoggerDBManager != null)
            {
                LogDbReturn = errorLoggerDBManager.DBSaveWithTryCache(model);
                if (!string.IsNullOrEmpty(LogDbReturn))
                    if (errorLoggerLocalManager != null)
                        errorLoggerLocalManager.LocalSaveWithTryCache(model);
            }
            else if (errorLoggerLocalManager != null)
                LogDbReturn = errorLoggerLocalManager.LocalSaveWithTryCache(model);



            if (emailManager != null)
                emailReturn = emailManager.SenderDeveloperWithTryCache(model);



            if (!string.IsNullOrEmpty(LogDbReturn))
                model.ErrorDetail = $"{LogDbReturn} " + model.ErrorDetail;


            if (!string.IsNullOrEmpty(emailReturn))
                model.ErrorDetail = $"{emailReturn} " + model.ErrorDetail;
        }
    }
}
