using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface ICompanyService
    {
        Task<OperationResult<bool>> CreateAsync(Company company);
        Task<OperationResult<string>> UpdateAsync(Company company);
        Task<OperationResult<Pagination<Company>>> GetAllAsync(int currentPage, int displayCount = 10, int userId = 0, string? value = null);
        Task<OperationResult<IEnumerable<Company>>> GetAllAsync();
        Task<OperationResult<string>> DeleteAsync(int id);
        Task<OperationResult<Company>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<Company>>> SearchAsync(string value);
        Task<OperationResult<IEnumerable<Company>>> GetRelatedCompaniesAsync(int userId);
        
    }
}
