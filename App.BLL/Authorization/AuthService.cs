using App.Entities;
using App.Entities.Models;
using Interfaces;
using System.Threading.Tasks;

namespace App.BLL
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        private readonly ILoggerService _loggerService;
        
        public AuthService(ILoggerService loggerService,IUnitOfWork unitOfWork,IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _encryptionService = encryptionService;
        }
        public async Task<OperationResult<ApplicationUser>> LoginAsync(string username, string password)
        {

            try
            {
                var user = await _unitOfWork.ApplicationUsers.FindAsync(x => x.Email == username);
                if (user is null)
                {
                    return OperationResult<ApplicationUser>.Fail(ErrorCatalog.Auth.UserNotFound.Message);
                }

                string decryptedPassword = _encryptionService.Decrypt(user.Password);

                if (decryptedPassword == password)
                {
                    return OperationResult<ApplicationUser>.Ok(user);
                }

                return OperationResult<ApplicationUser>.Fail(ErrorCatalog.Auth.InvalidCredentials.Message);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex,ErrorCatalog.Server.Unexpected.Message,new
                {
                    Username = username            
                });
                return OperationResult<ApplicationUser>.Fail(ex.Message);
            }
        }


        public async Task<OperationResult<ApplicationUser>> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.ApplicationUsers.GetByIdAsync(id);
            if (user != null)
            {
                return OperationResult<ApplicationUser>.Ok(user);
            }
            else
            {
                return OperationResult<ApplicationUser>.Fail("المستخدم غير موجود.");
            }
        }

        public async Task UpdateAsync(ApplicationUser applicationUser)
        {
            try
            {
                applicationUser.Password = _encryptionService.Encrypt(applicationUser.Password);
                _unitOfWork.ApplicationUsers.Update(applicationUser);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
