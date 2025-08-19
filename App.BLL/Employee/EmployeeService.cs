using App.Entities;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        public EmployeeService(IUnitOfWork unitOfWork,IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
        }
        
        public async Task<OperationResult<ApplicationUser>> GetByIdAsync(int id)
        {
            if (id < 0)
                return OperationResult<ApplicationUser>.Fail(ErrorCatalog.Validation.InvalidId.Message);

            var applicationUser = await _unitOfWork.ApplicationUsers.GetByIdAsync(id);

            if (applicationUser is null)
                return OperationResult<ApplicationUser>.Fail(ErrorCatalog.Database.RecordNotFound.Message);

            return OperationResult<ApplicationUser>.Ok(applicationUser);
        }

        public async Task<OperationResult<object>> UpdateAsync(ApplicationUser applicationUser)
        {
            if (applicationUser is null)
                return OperationResult<object>.Fail(ErrorCatalog.Database.RecordNotFound.Message);

            if (_unitOfWork.ApplicationUsers.IsExist(x => x.Email == applicationUser.Email && x.Id != applicationUser.Id)) 
                return OperationResult<object>.Fail(ErrorCatalog.Database.UniqueConstraintViolation.Message);

            applicationUser.UpdatedAt = DateTime.UtcNow;
            applicationUser.Password = _encryptionService.Encrypt(applicationUser.Password);
            
            _unitOfWork.ApplicationUsers.Update(applicationUser);
            
            await _unitOfWork.SaveAsync();

            return OperationResult<object>.Ok(null);
        }

        public async Task<OperationResult<object>> CreateAsync(ApplicationUser applicationUser)
        {
            if (applicationUser is null)
                return OperationResult<object>.Fail(ErrorCatalog.Database.RecordNotFound.Message);

            if (_unitOfWork.ApplicationUsers.IsExist(x => x.Email == applicationUser.Email && x.Id != applicationUser.Id))
                return OperationResult<object>.Fail(ErrorCatalog.Database.UniqueConstraintViolation.Message);

            applicationUser.Password = _encryptionService.Encrypt(applicationUser.Password);
            
            await _unitOfWork.ApplicationUsers.AddAsync(applicationUser);
            await _unitOfWork.SaveAsync();

            return OperationResult<object>.Ok(null);
        }

        public async Task<OperationResult<Pagination<ApplicationUser>>> GetAllAsync(int currentPage, int pageSize, string value)
        {
            Expression<Func<ApplicationUser, bool>> expression = x =>
                    (x.UserType == UserType.Employee) &&
                    (string.IsNullOrEmpty(value) || x.Name.Contains(value)) &&
                    (!x.IsDeleted);

            int skip = currentPage * pageSize;
            
            var totalCount = await _unitOfWork.ApplicationUsers.CountAsync(expression);
            
            var applicationUsers = await _unitOfWork.ApplicationUsers.FindAllAsync(expression, skip, pageSize);
            
            return OperationResult<Pagination<ApplicationUser>>.Ok(new Pagination<ApplicationUser>()
            {
                TotalCount = totalCount,
                Items = applicationUsers
            });
        }

        public async Task<OperationResult<object>> UpdateRangeAsync(IEnumerable<ApplicationUser> applicationUsers)
        {
            _unitOfWork.ApplicationUsers.UpdateRange(applicationUsers);
            await _unitOfWork.SaveAsync();

            return OperationResult<object>.Ok(null);
        }

        public async Task<OperationResult<object>> DeleteAsync(int id)
        {
            if (id < 0)
                return OperationResult<object>.Fail(ErrorCatalog.Validation.InvalidId.Message);

            var result = await GetByIdAsync(id);
            if (!result.State)
                return OperationResult<object>.Fail(result.Message);

            result.Data.IsDeleted = true;

            _unitOfWork.ApplicationUsers.Update(result.Data);
            await _unitOfWork.SaveAsync();

            return OperationResult<object>.Ok(null);
        }
    }
}
