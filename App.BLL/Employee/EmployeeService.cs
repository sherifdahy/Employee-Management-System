using App.Entities.Enums;
using App.Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class EmployeeService : IEmployeeService
    {
        IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        public EmployeeService(IUnitOfWork unitOfWork,IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
        }
        public async Task CreateAsync(ApplicationUser  applicationUser)
        {
            try
            {
                applicationUser.Password = _encryptionService.Encrypt(applicationUser.Password);
                await _unitOfWork.ApplicationUsers.AddAsync(applicationUser);
                _unitOfWork.Save();
            }
            catch (Exception ex) 
            {
                
            }
        }

        public async Task<OperationResult<bool, string>> DeleteAsync(Guid id)
        {
            try
            {
                var result = await GetByIdAsync(id);
                if (!result.State)
                    return OperationResult<bool, string>.Fail(result.Message);

                result.Data.IsDeleted = true;
                _unitOfWork.ApplicationUsers.Update(result.Data);
                _unitOfWork.Save();

                return OperationResult<bool, string>.Ok(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool, string>.Fail(ex.Message);
            }
        }
        
        public async Task<Pagination<ApplicationUser>> GetAllAsync(int currentPage, int pageSize, string value)
        {
            Expression<Func<ApplicationUser, bool>> expression = x =>
                    (x.UserType == UserType.Employee) &&
                    (string.IsNullOrEmpty(value) || x.Name.Contains(value)) &&
                    (!x.IsDeleted);
            int skip = currentPage * pageSize;
            var totalCount = await _unitOfWork.ApplicationUsers.CountAsync(expression);
            var applicationUsers = await _unitOfWork.ApplicationUsers.FindAllAsync(expression, pageSize, skip);
            return new Pagination<ApplicationUser>()
            {
                TotalCount = totalCount,
                Items = applicationUsers
            };
        }
        public void UpdateRange(IEnumerable<ApplicationUser> applicationUsers)
        {
            _unitOfWork.ApplicationUsers.UpdateRange(applicationUsers);
            _unitOfWork.Save();
        }

        public async Task<OperationResult<ApplicationUser, string>> GetByIdAsync(Guid id)
        {
            try
            {

                var applicationUser = await _unitOfWork.ApplicationUsers.GetByIdAsync(id);

                if (applicationUser == null)
                {
                    return OperationResult<ApplicationUser, string>.Fail("User not found.");
                }

                return OperationResult<ApplicationUser, string>.Ok(applicationUser);
            }
            catch (Exception ex)
            {
                return OperationResult<ApplicationUser, string>.Fail(ex.Message);
            }
        }

        public OperationResult<bool, string> Update(ApplicationUser applicationUser)
        {
            try
            {
                if (applicationUser is null)
                {
                    return OperationResult<bool, string>.Fail("Application user cannot be null.");
                }

                applicationUser.UpdatedAt = DateTime.UtcNow;
                applicationUser.Password = _encryptionService.Encrypt(applicationUser.Password);
                _unitOfWork.ApplicationUsers.Update(applicationUser);
                _unitOfWork.Save();

                return OperationResult<bool, string>.Ok(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool, string>.Fail(ex.Message);
            }
        }

    }
}
