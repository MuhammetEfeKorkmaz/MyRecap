using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Recap.Core.Entities.Concrete;

namespace Recap.Core.Utilities.ErrorLogger.Factories
{
    public class MsSqlServerFactory : IErrorLoggerDBManager
    {
        ErrorLoggerModel errorLoggerModel;
        IHttpContextAccessor httpContextAccessor;
        string connectionString;
        public MsSqlServerFactory(string _configurationModelJson)
        {
            connectionString = _configurationModelJson;
            httpContextAccessor = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        public void DBSave(CustomExceptionExtensionModel model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var UserId_ = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserId").Value;
                var UserName = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserName").Value;
                var UserEmail = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserEmail").Value;

                int UserId = 0;
                int.TryParse(UserId_, out UserId);
                errorLoggerModel.UserId = UserId;
                errorLoggerModel.UserName = UserName;
                errorLoggerModel.UserEmail = UserEmail;


                errorLoggerModel.ErrorMessage = model.ErrorMessage;
                errorLoggerModel.ErrorStackTrace = model.ErrorStackTrace;
                errorLoggerModel.ErrorDetail = model.ErrorDetail;
                errorLoggerModel.Level = model.Level;

                string sorgu = $"insert into SystemLogger (Tarih,UserEmail,UserName,ErrorDetail,ErrorMessage,ErrorStackTrace,UserId,Level) values (GETDATE()," +
                        $"@UserEmail,@UserName,@ErrorDetail,@ErrorMessage,@ErrorStackTrace,@UserId,@Level)";
                SqlCommand cmd = new SqlCommand(sorgu, con);
                cmd.Parameters.AddWithValue("@UserEmail", errorLoggerModel.UserEmail);
                cmd.Parameters.AddWithValue("@UserName", errorLoggerModel.UserName);
                cmd.Parameters.AddWithValue("@ErrorDetail", errorLoggerModel.ErrorDetail);
                cmd.Parameters.AddWithValue("@ErrorMessage", errorLoggerModel.ErrorMessage);
                cmd.Parameters.AddWithValue("@ErrorStackTrace", errorLoggerModel.ErrorStackTrace);
                cmd.Parameters.AddWithValue("@UserId", errorLoggerModel.UserId);
                cmd.Parameters.AddWithValue("@Level", errorLoggerModel.Level);
                con.Open();
                for (int i = 0; i < 2; i++)
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        break;
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public string DBSaveWithTryCache(CustomExceptionExtensionModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    var UserId_ = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserId").Value;
                    var UserName = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserName").Value;
                    var UserEmail = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserInfo").FirstOrDefault(x => x.ValueType == "UserEmail").Value;

                    int UserId = 0;
                    int.TryParse(UserId_, out UserId);
                    errorLoggerModel.UserId = UserId;
                    errorLoggerModel.UserName = UserName;
                    errorLoggerModel.UserEmail = UserEmail;


                    errorLoggerModel.ErrorMessage = model.ErrorMessage;
                    errorLoggerModel.ErrorStackTrace = model.ErrorStackTrace;
                    errorLoggerModel.ErrorDetail = model.ErrorDetail;
                    errorLoggerModel.Level = model.Level;

                    string sorgu = $"insert into SystemLogger (Tarih,UserEmail,UserName,ErrorDetail,ErrorMessage,ErrorStackTrace,UserId,Level) values (GETDATE()," +
                        $"@UserEmail,@UserName,@ErrorDetail,@ErrorMessage,@ErrorStackTrace,@UserId,@Level)";
                    SqlCommand cmd = new SqlCommand(sorgu, con);
                    cmd.Parameters.AddWithValue("@UserEmail", errorLoggerModel.UserEmail);
                    cmd.Parameters.AddWithValue("@UserName", errorLoggerModel.UserName);
                    cmd.Parameters.AddWithValue("@ErrorDetail", errorLoggerModel.ErrorDetail);
                    cmd.Parameters.AddWithValue("@ErrorMessage", errorLoggerModel.ErrorMessage);
                    cmd.Parameters.AddWithValue("@ErrorStackTrace", errorLoggerModel.ErrorStackTrace);
                    cmd.Parameters.AddWithValue("@UserId", errorLoggerModel.UserId);
                    cmd.Parameters.AddWithValue("@Level", errorLoggerModel.Level);
                    con.Open();
                    for (int i = 0; i < 2; i++)
                    {
                        if (con.State == System.Data.ConnectionState.Open)
                            break;
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return String.Empty;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }




        }
    }
}
