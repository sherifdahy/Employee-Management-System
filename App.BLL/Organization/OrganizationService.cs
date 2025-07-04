using App.Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrganizationService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Organization organization)
        {
            await _unitOfWork.Organizations.AddAsync(organization);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            var organization = await _unitOfWork.Organizations.GetAllAsync();
            return organization;
        }
    }
}
