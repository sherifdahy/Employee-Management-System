using App.Entities.Enums;
using App.Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var applicationUsers = await _unitOfWork.ApplicationUsers.FindAllAsync(x=>x.UserType == UserType.Employee);
            return applicationUsers;
        }
        public void UpdateRange(IEnumerable<ApplicationUser> applicationUsers)
        {
            _unitOfWork.ApplicationUsers.UpdateRange(applicationUsers);
            _unitOfWork.Save();
        }
    }
}
