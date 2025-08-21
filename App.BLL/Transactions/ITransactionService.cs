using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Transactions
{
    public interface ITransactionService
    {
        Task<OperationResult<Pagination<DailyTransaction>>> GetAllAsync(int pageIndex,int pageSize,string value);
        Task<OperationResult<DailyTransaction>> GetByIdAsync(int id);
        Task<OperationResult<object>> CreateAsync(DailyTransaction transaction);
        Task<OperationResult<object>> UpdateAsync(DailyTransaction transaction);
        Task<OperationResult<object>> DeleteAsync(int id);
    }
}
