using App.Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<ApplicationUser,string>> LoginAsync(string username, string password)
        {
            var user = await _unitOfWork.ApplicationUsers.FindAsync(x=>x.Email == username && x.Password == password);
            if(user != null)
            {
                return OperationResult<ApplicationUser,string>.Ok(user);
            }
            else
            {
                return OperationResult<ApplicationUser, string>.Fail("اسم المستخدمة او كلمة المرور خطأ من فضلك حاول مرة اخري");
            }
        }
    }
}
