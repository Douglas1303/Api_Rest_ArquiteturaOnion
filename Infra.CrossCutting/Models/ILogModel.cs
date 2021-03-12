using System;
using System.Runtime.CompilerServices;

namespace Infra.CrossCutting.Models
{
    public interface ILogModel
    {
        void RecLog(string methodName, string message, LogType type);
        void RecLog(Exception ex, [CallerMemberName] string methodName = "", [CallerLineNumber] int line = 0, [CallerFilePath] string filePath = "");
    }
}