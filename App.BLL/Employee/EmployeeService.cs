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
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(ApplicationUser  applicationUser)
        {
            await _unitOfWork.ApplicationUsers.AddAsync(applicationUser);
            _unitOfWork.Save();
        }

        public async Task<Pagination<ApplicationUser>> GetAllAsync(int currentPage, int pageSize, string value)
        {
            Expression<Func<ApplicationUser, bool>> expression = x =>
                    x.UserType == UserType.Employee &&
                    (string.IsNullOrEmpty(value) || x.Name.Contains(value));
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
    }
}
