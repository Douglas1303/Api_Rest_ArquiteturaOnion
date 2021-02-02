using Newtonsoft.Json;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Infra.CrossCutting.Models
{
    [ExcludeFromCodeCoverage]
    public class LogModel : ILogModel
    {
        public string MethodName { get; set; }
        public string Message { get; set; }
        public string Localization { get; set; }
        public LogType Type { get; set; }

        public LogModel()
        {
        }

        public LogModel(string methodName, string message, LogType type)
        {
            MethodName = methodName;
            Message = message;
            Type = type;
            RecLog();
        }

        public void RecLog(string methodName, string message, LogType type)
        {
            MethodName = methodName;
            Message = message;
            Type = type;
            RecLog();
        }

        public void RecLog(Exception ex, [CallerMemberName] string methodName = "", [CallerLineNumber] int line = 0, [CallerFilePath] string filePath = "")
        {
            MethodName = methodName;
            Message = $"{ JsonConvert.SerializeObject(new { Exception = ex }) }";
            Localization = $"FilePath: {filePath}, Line: {line}";
            Type = LogType.LogError;
            RecLog();
        }

        private void RecLog()
        {
            var template = "{MethodName} - {Message} - {Localization}";
            var message = $"{MethodName} - {Message} -  {Localization}";
            switch (Type)
            {
                case LogType.LogInformation:
                    Log.Information(template, message);
                    break;

                case LogType.LogDebug:
                    Log.Debug(template, message);
                    break;

                case LogType.LogError:
                    Log.Error(template, message);
                    break;

                case LogType.LogWarning:
                    Log.Warning(template, message);
                    break;
            }
        }
    }
}