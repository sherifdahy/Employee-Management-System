using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.TransactionItemCategoryService
{
    public interface ITransactionItemCategoryService
    {
        Task<OperationResult<IEnumerable<TransactionItemCategory>>> GetAllAsync(int pageIndex,int pageSize,string value);
    }
}
