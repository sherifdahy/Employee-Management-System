using App.BLL.Mappers;
using App.Entities;
using App.Entities.Models;
using Interfaces;
using System.Linq.Expressions;

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
        public async Task<OperationResult<bool>> CreateAsync(Company company)
        {
            if (_unitOfWork.Companies.IsExist(x => x.TaxRegistrationNumber == company.TaxRegistrationNumber))
                return OperationResult<bool>.Fail($"{ErrorCatalog.Database.UniqueConstraintViolation.Message} - {company.TaxRegistrationNumber}");

            foreach (var email in company.Emails)
            {
                email.Password = _encryptionService.Encrypt(email.Password);
            }

            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.SaveAsync();

            return OperationResult<bool>.Ok(true);
        }

        public async Task<OperationResult<Company>> GetByIdAsync(int id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company != null)
            {
                foreach (var email in company.Emails)
                {
                    email.Password = _encryptionService.Decrypt(email.Password);
                }

                return OperationResult<Company>.Ok(company);
            }
            else
            {
                return OperationResult<Company>.Fail(ErrorCatalog.Database.RecordNotFound.Message);
            }
        }
        public async Task<OperationResult<string>> DeleteAsync(int id)
        {
            var companyResult = await GetByIdAsync(id);
            if (!companyResult.State)
                return OperationResult<string>.Fail(companyResult.Message);

            companyResult.Data.IsDeleted = true;

            _unitOfWork.Companies.Update(companyResult.Data);
            await _unitOfWork.SaveAsync();

            return OperationResult<string>.Ok(string.Empty);
        }

        public async Task<OperationResult<Pagination<Company>>> GetAllAsync(int currentPage,int displayCount = 10,int userId = 0,string? value = null)
        {
            Expression<Func<Company, bool>> query = x =>
            (userId == default || x.ApplicationUsers.Any(u => u.Id == userId)) &&
            (string.IsNullOrEmpty(value) || x.Name.Contains(value) || x.TaxRegistrationNumber.Contains(value)) &&
            (!x.IsDeleted);

            var totalCount = await _unitOfWork.Companies.CountAsync(query);
            var skip = currentPage * displayCount;

            var companies = await _unitOfWork.Companies.FindAllAsync(query, skip, displayCount);

            return OperationResult<Pagination<Company>>.Ok(new Pagination<Company>()
            {
                TotalCount = totalCount,
                Items = companies,
            });

        }

        public async Task<OperationResult<IEnumerable<Company>>> GetRelatedCompaniesAsync(int userId)
        {
            var appUser = await _unitOfWork.ApplicationUsers.FindAsync(x => x.Id == userId && !x.IsDeleted);
            return OperationResult<IEnumerable<Company>>.Ok(appUser.Companies);
        }

        public async Task<OperationResult<IEnumerable<Company>>> SearchAsync(string value)
        {
            Expression<Func<Company, bool>> query = x =>
            (string.IsNullOrEmpty(value) ? true : x.Name.Contains(value) || x.TaxRegistrationNumber.Contains(value)) && !x.IsDeleted;

            var companies = await _unitOfWork.Companies.FindAllAsync(query);
                
            return OperationResult<IEnumerable<Company>>.Ok(companies);
        }

        public async Task<OperationResult<string>> UpdateAsync(Company company)
        {
            foreach(var email in company.Emails)
            {
                email.Password = _encryptionService.Encrypt(email.Password);
            }
            company.UpdatedAt = DateTime.UtcNow;
                
            _unitOfWork.Companies.Update(company);
            await _unitOfWork.SaveAsync();

            return OperationResult<string>.Ok(string.Empty);
        }

        public async Task<OperationResult<IEnumerable<Company>>> GetAllAsync()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();
            return OperationResult<IEnumerable<Company>>.Ok(companies);
        }

        
    }
}
