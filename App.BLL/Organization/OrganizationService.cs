using App.Entities;
using App.Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<OperationResult<bool>> CreateAsync(Organization organization)
        {
            try
            {
                await _unitOfWork.Organizations.AddAsync(organization);
                await _unitOfWork.SaveAsync();

                return OperationResult<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return OperationResult<bool>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var result = await GetByIdAsync(id);
                if (!result.State)
                    return OperationResult<bool>.Fail(result.Message);


                if (result.Data.Emails.Any())
                    return OperationResult<bool>.Fail(ErrorCatalog.Database.ForeignKeyViolation.Message);

                _unitOfWork.Organizations.Delete(result.Data);
                await _unitOfWork.SaveAsync();
                return OperationResult<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                _loggerService?.LogError(ex);
                return OperationResult<bool>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<Organization>>> GetAllAsync()
        {
            try
            {
                var organization = await _unitOfWork.Organizations.GetAllAsync();
                return OperationResult<IEnumerable<Organization>>.Ok(organization);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return OperationResult<IEnumerable<Organization>>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }

        public async Task<OperationResult<Organization>> GetByIdAsync(int id)
        {
            try
            {
                var organization = await _unitOfWork.Organizations.GetByIdAsync(id);
                if (organization == null)
                    return OperationResult<Organization>.Fail(ErrorCatalog.Database.RecordNotFound.Message);

                return OperationResult<Organization>.Ok(organization);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return OperationResult<Organization>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }

        public async Task<OperationResult<Organization>> UpdateAsync(Organization organization)
        {
            try
            {
                organization.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Organizations.Update(organization);
                await _unitOfWork.SaveAsync();

                return OperationResult<Organization>.Ok(organization);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return OperationResult<Organization>.Fail(ErrorCatalog.Server.Unexpected.Message);
            }
        }
    }
}
