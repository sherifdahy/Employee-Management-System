using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DataSync
{
    public interface IDataSync
    {
        Task<OperationResult<object, string>> ExportToFileAsync(string filePath);
        Task<OperationResult<object, string>> ImportFromFileAsync(string filePath);
    }
}
