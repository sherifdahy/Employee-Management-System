using App.Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public Task<OperationResult<ICollection<Email>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<Email>> GetByIdAsync(int id)
        {
            try
            {
                var email = await _unitOfWork.Emails.FindAsync(x => x.Id == id);
                if (email is null) throw new InvalidOperationException("الحساب غير موجود");

                return OperationResult<Email>.Ok(email);
            }
            catch (Exception ex) 
            {
                return OperationResult<Email>.Fail(ex.Message);
            }
        }
    }
}
