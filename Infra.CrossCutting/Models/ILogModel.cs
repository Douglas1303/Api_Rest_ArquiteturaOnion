namespace Infra.CrossCutting.Models
{
    public interface ILogModel
    {
        void RecLog(string methodName, string message, LogType type);
    }
}