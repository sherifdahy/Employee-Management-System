using Serilog;

namespace App.BLL
{
    public class LoggerService : ILoggerService
    {
        public void LogInfo(string message, object data = null)
        {
            if (data == null)
                Log.Information(message);
            else
                Log.Information("{Message} | Data: {@Data}", message, data);
        }

        public void LogWarning(string message, object data = null)
        {
            if (data == null)
                Log.Warning(message);
            else
                Log.Warning("{Message} | Data: {@Data}", message, data);
        }

        public void LogError(Exception ex, string message = null, object data = null)
        {
            if (message == null)
                Log.Error(ex, "Exception occurred");
            else if (data == null)
                Log.Error(ex, message);
            else
                Log.Error(ex, "{Message} | Data: {@Data}", message, data);
        }

    }
}
