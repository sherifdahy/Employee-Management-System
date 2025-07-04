using App.Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Company company)
        {
            await _unitOfWork.Companies.AddAsync(company);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();
            return companies;
        }

        public async Task<IEnumerable<Company>> SearchAsync(string value)
        {
            var companies = await _unitOfWork.Companies.FindAllAsync(x => string.IsNullOrEmpty(value) ? true : x.Name.Contains(value) || x.TaxRegistrationNumber.Contains(value));
            return companies;
        }
    }
}
