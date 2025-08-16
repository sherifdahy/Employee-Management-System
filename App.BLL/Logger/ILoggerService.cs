using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface ILoggerService
    {
        void LogInfo(string message, object data = null);
        void LogWarning(string message, object data = null);
        void LogError(Exception ex, string message = null, object data = null);
    }
}
