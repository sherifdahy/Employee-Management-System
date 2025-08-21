using App.Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.TransactionItemCategoryService
{
    public class TransactionItemCategoryService : ITransactionItemCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionItemCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        public async Task<OperationResult<IEnumerable<TransactionItemCategory>>> GetAllAsync(int pageIndex, int pageSize, string value)
        {
            Expression<Func<TransactionItemCategory, bool>> exception =
                x => string.IsNullOrWhiteSpace(value) || x.Name.Contains(value) ;
            int skip = pageIndex * pageSize ;
            int totalCount = await _unitOfWork.TransactionItemCategories.CountAsync(exception);
            var categories = await _unitOfWork.TransactionItemCategories.FindAllAsync(exception,skip,pageSize,x=>x.Id,true);
            return OperationResult<IEnumerable<TransactionItemCategory>>.Ok(categories);
        }
    }
}
