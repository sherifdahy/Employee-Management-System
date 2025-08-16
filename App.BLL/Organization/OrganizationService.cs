using App.Entities;
using App.Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.BLL
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        public OrganizationService(IUnitOfWork unitOfWork,ILoggerService loggerService) 
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
        }
        public async Task<OperationResult<bool, string>> CreateAsync(Organization organization)
        {
            try
            {
                await _unitOfWork.Organizations.AddAsync(organization);
                _unitOfWork.Save();

                return OperationResult<bool, string>.Ok(true);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return OperationResult<bool,string>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }

        public async Task<OperationResult<bool, string>> DeleteAsync(Guid id)
        {
            try
            {
                var result = await GetByIdAsync(id);
                if (!result.State)
                    return OperationResult<bool, string>.Fail(result.Message);


                if (result.Data.Emails.Any())
                    return OperationResult<bool, string>.Fail(ErrorCatalog.Database.ForeignKeyViolation.Message);

                _unitOfWork.Organizations.Delete(result.Data);
                _unitOfWork.Save();
                return OperationResult<bool,string>.Ok(true);
            }
            catch (Exception ex)
            {
                _loggerService?.LogError(ex);
                return OperationResult<bool, string>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<Organization>,string>> GetAllAsync()
        {
            try
            {
                var organization = await _unitOfWork.Organizations.GetAllAsync();
                return OperationResult<IEnumerable<Organization>,string>.Ok(organization);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return OperationResult<IEnumerable<Organization>, string>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }

        public async Task<OperationResult<Organization, string>> GetByIdAsync(Guid id)
        {
            try
            {
                var organization = await _unitOfWork.Organizations.GetByIdAsync(id);
                if (organization == null)
                    return OperationResult<Organization, string>.Fail(ErrorCatalog.Database.RecordNotFound.Message);

                return OperationResult<Organization,string>.Ok(organization);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return OperationResult<Organization, string>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }

        public OperationResult<Organization, string> Update(Organization organization)
        {
            try
            {
                organization.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Organizations.Update(organization);
                _unitOfWork.Save();

                return OperationResult<Organization, string>.Ok(organization);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return OperationResult<Organization, string>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }
    }
}
