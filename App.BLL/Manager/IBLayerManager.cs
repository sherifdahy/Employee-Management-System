using App.BLL.DataSync;
using App.BLL.Dependencies.Interfaces;
using App.BLL.TransactionItemCategoryService;
using App.BLL.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Manager
{
    public interface IBLayerManager
    {
        IDataSync DataSync { get; }
        ICompanyService CompanyService { get; }
        IAuthService AuthService { get; }
        IEmailService EmailService { get; }
        IEmployeeService EmployeeService { get; }
        IEncryptionService EncryptionService { get; }
        IOrganizationService OrganizationService { get; }
        IBrowserService BrowserService { get; }
        ITransactionService TransactionService { get; }
        ITransactionItemCategoryService TransactionItemCategoryService { get; }

    }
}
