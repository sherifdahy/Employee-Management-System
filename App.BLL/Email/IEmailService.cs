using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface IEmailService
    {
        Task<OperationResult<ICollection<Email>, string>> GetAllAsync();
        Task<OperationResult<Email, string>> GetByIdAsync(Guid id);
    }
}
