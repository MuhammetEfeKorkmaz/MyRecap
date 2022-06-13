using Recap.Core.Entities.Concrete;

namespace Recap.Core.Utilities.Email
{
    public interface IEmailManager
    {
        void SenderDeveloper(CustomExceptionExtensionModel model);
        string SenderDeveloperWithTryCache(CustomExceptionExtensionModel model);

        void SenderUser(string RecipientUserEmail, string RecipientUserName, string RecipientUserSurName, string RecipientUserPassword);
        string SenderUserWithTryCache(string RecipientUserEmail, string RecipientUserName, string RecipientUserSurName, string RecipientUserPassword);
         
    }
}
