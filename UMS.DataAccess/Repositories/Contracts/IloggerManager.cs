

namespace UMS.DataAccess.Repositories.Contracts
{
    public interface  IloggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogError(string message);
        void LogDebug(string message);
    }
}