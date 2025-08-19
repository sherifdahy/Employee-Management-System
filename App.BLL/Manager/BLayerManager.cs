using App.BLL.DataSync;
using App.BLL.Dependencies.Interfaces;
using App.Entities.Helper;
using Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Manager
{
    public class BLayerManager : IBLayerManager
    {
        public BLayerManager(
            IDataSync dataSync,
            ICompanyService companyService,
            IAuthService authService,
            IEmailService emailService,
            IEmployeeService employeeService,
            IEncryptionService encryptionService,
            IOrganizationService organizationService,
            IBrowserService browserService)
        {
            DataSync = dataSync;
            BrowserService = browserService;
            CompanyService = companyService;
            AuthService = authService;
            EmailService = emailService;
            EmployeeService = employeeService;
            EncryptionService = encryptionService;
            OrganizationService = organizationService;
        }

        public IDataSync DataSync { get; }
        public ICompanyService CompanyService { get; }
        public IAuthService AuthService { get; }
        public IEmailService EmailService { get; }
        public IEmployeeService EmployeeService { get; }
        public IEncryptionService EncryptionService { get; }
        public IOrganizationService OrganizationService { get; }
        public IBrowserService BrowserService { get; }
    }
}
