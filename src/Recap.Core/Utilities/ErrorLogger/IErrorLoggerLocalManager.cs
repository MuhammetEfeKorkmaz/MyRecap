using Recap.Core.Entities.Concrete;

namespace Recap.Core.Utilities.ErrorLogger
{
    public interface IErrorLoggerLocalManager
    {
        void LocalSave(CustomExceptionExtensionModel model);
        string LocalSaveWithTryCache(CustomExceptionExtensionModel model);
    }
}
