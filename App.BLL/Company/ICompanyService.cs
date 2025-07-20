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
        Task CreateAsync(Company company);
        Task<Pagination<Company>> GetAllAsync(int currentPage, int displayCount = 10, int userId = 0, string? value = null);
        Task<OperationResult<string, string>> DeleteAsync(int id);
        Task<OperationResult<Company, string>> GetByIdAsync(int id);
        Task<IEnumerable<Company>> SearchAsync(string value);
        Task<IEnumerable<Company>> GetRelatedCompaniesAsync(int userId);
    }
}
