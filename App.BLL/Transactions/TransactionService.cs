using App.Entities;
using App.Entities.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<object>> CreateAsync(DailyTransaction transaction)
        {
            if (transaction is null)
                return OperationResult<object>.Fail(ErrorCatalog.Validation.RequiredFieldMissing.Message);

            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.SaveAsync();

            return OperationResult<object>.Ok(null);
        }

        public Task<OperationResult<object>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<Pagination<DailyTransaction>>> GetAllAsync(int pageIndex, int pageSize, string value)
        {
            Expression<Func<DailyTransaction,bool>> expression = 
                x=> string.IsNullOrWhiteSpace(value) || x.TransactionNumber.Contains(value);

            int skip = pageIndex * pageSize;

            int count = await _unitOfWork.Transactions.CountAsync(expression);

            var transactions = await _unitOfWork.Transactions.FindAllAsync(expression,skip, pageSize,x=>x.Id,isDesc:true);

            return OperationResult<Pagination<DailyTransaction>>.Ok(new Pagination<DailyTransaction>()
            {
                Items = transactions,
                TotalCount = count
            });

        }

        public Task<OperationResult<DailyTransaction>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<OperationResult<object>> UpdateAsync(DailyTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
