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
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool, string>> CreateAsync(Company company)
        {
            try
            {
                if (!_unitOfWork.Companies.IsExist(x => x.TaxRegistrationNumber == company.TaxRegistrationNumber))
                {
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

        public async Task<OperationResult<Company, string>> GetByIdAsync(int id)
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
        public async Task<OperationResult<string, string>> DeleteAsync(int id)
        {
            var companyResult = await GetByIdAsync(id);
            if(companyResult.State)
            {
                _unitOfWork.Companies.Delete(companyResult?.Data);
                _unitOfWork.Save();
                return OperationResult<string,string>.Ok(string.Empty);
            }
            else
            {
                return OperationResult<string,string>.Fail(companyResult?.Message);
            }
        }

        public async Task<Pagination<Company>> GetAllAsync(int currentPage,int displayCount = 10,int userId = 0,string? value = null)
        {
            Expression<Func<Company, bool>> query = x =>
                (userId == 0 || x.ApplicationUsers.Any(u => u.Id == userId)) &&
                (string.IsNullOrEmpty(value) || x.Name.Contains(value) || x.TaxRegistrationNumber.Contains(value));

            var totalCount = await _unitOfWork.Companies.CountAsync(query);
            var skip = currentPage * displayCount;
            var companies = await _unitOfWork.Companies.FindAllAsync(query,displayCount,skip);
            return new Pagination<Company>()
            {
                TotalCount = totalCount,
                Items = companies,
            };

        }

        public async Task<IEnumerable<Company>> GetRelatedCompaniesAsync(int userId)
        {
            var appUser = await _unitOfWork.ApplicationUsers.FindAsync(x=>x.Id == userId);
            return appUser.Companies;
        }

        public async Task<IEnumerable<Company>> SearchAsync(string value)
        {
            var companies = await _unitOfWork.Companies.FindAllAsync(x => string.IsNullOrEmpty(value) ? true : x.Name.Contains(value) || x.TaxRegistrationNumber.Contains(value));
            return companies;
        }

        public OperationResult<string, string> Update(Company company)
        {
            try
            {
                _unitOfWork.Companies.Update(company);
                _unitOfWork.Save();
                return OperationResult<string, string>.Ok("تم الحفظ بنجاح");
            }
            catch (Exception ex) {
                return OperationResult<string, string>.Fail(ex.Message);
            }
        }
    }
}
