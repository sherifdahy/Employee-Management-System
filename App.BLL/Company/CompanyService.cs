using App.BLL.Mappers;
using App.Entities;
using App.Entities.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        public CompanyService(IUnitOfWork unitOfWork,IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
        }
        public async Task<OperationResult<bool, string>> CreateAsync(Company company)
        {
            try
            {
                if (!_unitOfWork.Companies.IsExist(x => x.TaxRegistrationNumber == company.TaxRegistrationNumber))
                {
                    foreach(var email in company.Emails)
                    {
                        email.Password = _encryptionService.Encrypt(email.Password);
                    }

                    await _unitOfWork.Companies.AddAsync(company);
                    _unitOfWork.Save();
                    return OperationResult<bool, string>.Ok(true);
                }
                return OperationResult<bool, string>.Fail("رقم التسجيل الضريبي مسجل من قبل");
            }
            catch (Exception ex)
            {
                return OperationResult<bool, string>.Fail("حدث خطأ أثناء الحفظ: " + ex.Message);
            }
        }

        public async Task<OperationResult<Company, string>> GetByIdAsync(Guid id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if(company != null)
            {
                return OperationResult<Company, string>.Ok(company);
            }
            else
            {
                return OperationResult<Company, string>.Fail("Not Found");
            }
        }
        public async Task<OperationResult<string, string>> DeleteAsync(Guid id)
        {
            var companyResult = await GetByIdAsync(id);
            if(companyResult.State)
            {
                companyResult.Data.IsDeleted = true;
                _unitOfWork.Companies.Update(companyResult?.Data);
                _unitOfWork.Save();
                return OperationResult<string,string>.Ok(string.Empty);
            }
            else
            {
                return OperationResult<string,string>.Fail(companyResult?.Message);
            }
        }

        public async Task<Pagination<Company>> GetAllAsync(int currentPage,int displayCount = 10,Guid userId = default,string? value = null)
        {
            Expression<Func<Company, bool>> query = x =>
                (userId == default || x.ApplicationUsers.Any(u => u.Id == userId)) &&
                (string.IsNullOrEmpty(value) || x.Name.Contains(value) || x.TaxRegistrationNumber.Contains(value)) &&
                (!x.IsDeleted);

            var totalCount = await _unitOfWork.Companies.CountAsync(query);
            var skip = currentPage * displayCount;
            var companies = await _unitOfWork.Companies.FindAllAsync(query,displayCount,skip);
            return new Pagination<Company>()
            {
                TotalCount = totalCount,
                Items = companies,
            };

        }

        public async Task<IEnumerable<Company>> GetRelatedCompaniesAsync(Guid userId)
        {
            var appUser = await _unitOfWork.ApplicationUsers.FindAsync(x=>x.Id == userId && !x.IsDeleted);
            return appUser.Companies;
        }

        public async Task<IEnumerable<Company>> SearchAsync(string value)
        {
            Expression<Func<Company, bool>> query = x =>
                (string.IsNullOrEmpty(value) ? true : x.Name.Contains(value) || x.TaxRegistrationNumber.Contains(value)) && !x.IsDeleted;

            var companies = await _unitOfWork.Companies.FindAllAsync(query);
            return companies;
        }

        public OperationResult<string, string> Update(Company company)
        {
            try
            {
                foreach(var email in company.Emails)
                {
                    email.Password = _encryptionService.Encrypt(email.Password);
                }
                company.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Companies.Update(company);
                _unitOfWork.Save();
                return OperationResult<string, string>.Ok("تم الحفظ بنجاح");
            }
            catch (Exception ex) {
                return OperationResult<string, string>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<Company>,string>> GetAllAsync()
        {
            try
            {
                var companies = await _unitOfWork.Companies.GetAllAsync();
                return OperationResult<IEnumerable<Company>,string>.Ok(companies);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Company>,string>.Fail(ex.Message);
            }
        }

        
    }
}
