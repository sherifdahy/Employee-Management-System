using App.BLL.DTOs;
using App.BLL.Mappers;
using App.DAL.Data;
using App.Entities;
using App.Entities.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DataSync
{
    public class DataSync : IDataSync
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext applicationDbContext;
        public DataSync(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext)
        {   
            _unitOfWork = unitOfWork;
            this.applicationDbContext = applicationDbContext;
        }

        private async Task<OperationResult<object, string>> GetAllAsync()
        {
            try
            {
                var obj = new DataSyncDTO()
                {
                    ApplicationUsers = (await _unitOfWork.ApplicationUsers.GetAllAsync()).ToDTO().ToList(),
                    Companies = (await _unitOfWork.Companies.GetAllAsync()).ToDTO().ToList(),
                    Organizations = (await _unitOfWork.Organizations.GetAllAsync()).ToDTO().ToList(),
                };
                return OperationResult<object, string>.Ok(obj);
            }
            catch (Exception ex)
            {
                return OperationResult<object, string>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<object, string>> ExportToFileAsync(string filePath)
        {
            try
            {
                var companiesResult = await GetAllAsync();

                if (!companiesResult.State)
                    throw new Exception(companiesResult.Message);

                var jsonData = JsonConvert.SerializeObject(companiesResult.Data, Formatting.Indented);

                await File.WriteAllTextAsync(filePath, jsonData);

                return OperationResult<object, string>.Ok(null);
            }
            catch (Exception ex)
            {
                return OperationResult<object, string>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<object, string>> ImportFromFileAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new Exception(ErrorCatalog.FilesRules.FileNotExist.Message);

                var fileContent = await File.ReadAllTextAsync(filePath);

                if (string.IsNullOrWhiteSpace(fileContent))
                    throw new Exception(ErrorCatalog.FilesRules.FileIsEmpty.Message);

                var obj = JsonConvert.DeserializeObject<DataSyncDTO>(fileContent);

                if (obj == null)
                    throw new Exception(ErrorCatalog.FilesRules.FileIsEmpty.Message);

                //await UpdateOrganizations(obj.Organizations);
                //await UpdateApplicationUsers(obj.ApplicationUsers);
                await UpdateCompanies(obj.Companies);


                return OperationResult<object, string>.Ok(filePath);
            }
            catch (Exception ex)
            {
                return OperationResult<object, string>.Fail(ex.Message);
            }
        }

        private async Task UpdateOrganizations(List<OrganizationDTO> organizationsDTO)
        {
            var organizations = (await _unitOfWork.Organizations.GetAllAsync()).ToDictionary(x => x.Id);

            foreach (var organizationDTO in organizationsDTO)
            {
                if (!organizations.TryGetValue(organizationDTO.Id, out var organization))
                {
                    _unitOfWork.Organizations.Add(organizationDTO.ToModel());
                }
                else // if (organizationDTO.UpdatedAt > organization.UpdatedAt)
                {
                    organizationDTO.ToModel(organization);
                    _unitOfWork.Organizations.Update(organization);
                }
            }
        }

        private async Task UpdateApplicationUsers(List<ApplicationUserDTO> applicationUsersDTO)
        {
            var applicationUsers = (await _unitOfWork.ApplicationUsers.GetAllAsync()).ToDictionary(x => x.Id);

            foreach (var applicationUserDTO in applicationUsersDTO)
            {
                if (!applicationUsers.TryGetValue(applicationUserDTO.Id, out var applicationUser))
                {
                    _unitOfWork.ApplicationUsers.Add(applicationUserDTO.ToModel());
                }
                else // if (applicationUserDTO.UpdatedAt > applicationUser.UpdatedAt)
                {
                    applicationUserDTO.ToModel(applicationUser);
                    _unitOfWork.ApplicationUsers.Update(applicationUser);
                }
            }
        }

        private async Task UpdateCompanies(List<CompanyDTO> companiesDTO)
        {
            var data = await _unitOfWork.Companies.GetAllAsync();
            
            if (data is null)
                return;

            var companies = data.ToDictionary(x => x.TaxRegistrationNumber);

            foreach(var companyDTO in companiesDTO)
            {
                if(!companies.TryGetValue(companyDTO.TaxRegistrationNumber,out Company foundedCompany))
                {
                    // new 
                    var newCompany = new Company();
                    companyDTO.ToModel(newCompany);
                    _unitOfWork.Companies.Add(newCompany);
                }
                else
                {
                    companyDTO.ToModel(foundedCompany);
                }

            }
            await _unitOfWork.SaveAsync();
        }
    }

}
