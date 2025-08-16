using App.Entities;
using App.Entities.Models;
using Interfaces;

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
        public async Task<OperationResult<ApplicationUser, string>> LoginAsync(string username, string password)
        {

            try
            {
                var user = await _unitOfWork.ApplicationUsers.FindAsync(x => x.Email == username);
                if (user is null)
                {
                    return OperationResult<ApplicationUser, string>.Fail(ErrorCatalog.Auth.UserNotFound.Message);
                }

                string decryptedPassword = _encryptionService.Decrypt(user.Password);

                if (decryptedPassword == password)
                {
                    return OperationResult<ApplicationUser, string>.Ok(user);
                }

                return OperationResult<ApplicationUser, string>.Fail(ErrorCatalog.Auth.InvalidCredentials.Message);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex,ErrorCatalog.Server.Unexpected.Message,new
                {
                    Username = username            
                });
                return OperationResult<ApplicationUser, string>.Fail(ex.Message);
            }
        }


        public async Task<OperationResult<ApplicationUser,string>> GetByIdAsync(Guid id)
        {
            var user = await _unitOfWork.ApplicationUsers.GetByIdAsync(id);
            if (user != null)
            {
                return OperationResult<ApplicationUser, string>.Ok(user);
            }
            else
            {
                return OperationResult<ApplicationUser, string>.Fail("المستخدم غير موجود.");
            }
        }

        public void Update(ApplicationUser applicationUser)
        {
            try
            {
                applicationUser.Password = _encryptionService.Encrypt(applicationUser.Password);
                _unitOfWork.ApplicationUsers.Update(applicationUser);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
