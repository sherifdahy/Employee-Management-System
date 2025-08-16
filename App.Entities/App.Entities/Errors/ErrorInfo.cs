using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class ErrorInfo
    {
        public string Code { get; }
        public string Message { get; }

        public ErrorInfo(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public override string ToString() => $"{Code}: {Message}";
    }

}
