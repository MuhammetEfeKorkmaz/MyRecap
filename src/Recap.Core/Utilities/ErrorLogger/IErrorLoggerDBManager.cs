using Recap.Core.Entities.Concrete;

namespace Recap.Core.Utilities.ErrorLogger
{
    public interface IErrorLoggerDBManager
    {
        void DBSave(CustomExceptionExtensionModel ex);
        string DBSaveWithTryCache(CustomExceptionExtensionModel ex);
    }
}
