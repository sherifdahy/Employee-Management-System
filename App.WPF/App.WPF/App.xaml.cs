using App.BLL;
using App.BLL.DataSync;
using App.BLL.Dependencies.Implementations;
using App.BLL.Dependencies.Interfaces;
using App.BLL.Manager;
using App.BLL.TransactionItemCategoryService;
using App.BLL.Transactions;
using App.DAL.Data;
using App.Entities.Helper;
using App.Entities.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApp.WPF.ApplicationConfiguration;
using MyApp.WPF.Services.State;
using MyApp.WPF.UserControls.Admin.Companies;
using MyApp.WPF.UserControls.Admin.DailyTransactions;
using MyApp.WPF.UserControls.Admin.Employees;
using MyApp.WPF.UserControls.Admin.Organizations;
using MyApp.WPF.UserControls.Shared;
using MyApp.WPF.Windows.Admin;
using MyApp.WPF.Windows.Identity;
using Repository;
using Serilog;
using System.Windows;

namespace MyApp.WPF
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("defaultConnection");

                    #region Databases
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseLazyLoadingProxies(true);
                        options.UseSqlServer(connectionString);
                    }, ServiceLifetime.Transient);

                    #endregion
                    #region Logger
                    LoggerConfigurator.ConfigureLogger(connectionString);
                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddSerilog(Log.Logger, dispose: true);
                    });

                    #endregion

                    #region AppSettings Mapping
                    services.Configure<EncryptionSettings>(context.Configuration.GetSection("EncryptionSettings"));
                    #endregion

                    #region Services Registration
                    // managers
                    services.AddTransient<IBLayerManager,BLayerManager>();
                    
                    // Business Layer
                    services.AddTransient<IAuthService, AuthService>();
                    services.AddTransient<ICompanyService, CompanyService>();
                    services.AddTransient<IOrganizationService, OrganizationService>();
                    services.AddTransient<IEmployeeService, EmployeeService>();
                    services.AddTransient<IDataSync, DataSync>();
                    services.AddTransient<ITransactionService, TransactionService>();
                    services.AddTransient<ITransactionItemCategoryService, TransactionItemCategoryService>();

                    // Application Layer
                    services.AddSingleton<IStateService, StateService>();
                    services.AddSingleton<IBrowserService, BrowserService>();
                    services.AddSingleton<IWebDriverFactory, WebDriverFactory>();
                    services.AddSingleton<IEmailService, EmailService>();
                    services.AddSingleton<IEncryptionService, EncryptionService>();

                    // UnitOfWork
                    services.AddTransient<IUnitOfWork, UnitOfWork>();

                    // Logger
                    services.AddScoped<ILoggerService, LoggerService>();
                    #endregion

                    #region Windows
                    services.AddTransient<LoginWindow>();
                    services.AddTransient<Windows.Admin.MainWindow>();
                    services.AddTransient<Windows.Employee.MainWindow>();
                    services.AddTransient<NewOwnerWindow>();
                    services.AddTransient<NewEmailWindow>();
                    services.AddTransient<AddSelectorWindow>();
                    #endregion

                    #region UserControls
                    services.AddTransient<UserControls.Admin.HomeControl>();
                    services.AddTransient<UserControls.Admin.Companies.CompaniesControl>();
                    services.AddTransient<UserControls.Employee.Home.HomeControl>();
                    services.AddTransient<UserControls.Employee.Companies.CompaniesControl>();
                    services.AddTransient<NewCompanyControl>();
                    services.AddTransient<NewOrganizationControl>();
                    services.AddTransient<OrganizationsControl>();
                    services.AddTransient<NewEmployeeControl>();
                    services.AddTransient<EmployeesControl>();
                    services.AddTransient<EmployeeAccessControl>();
                    services.AddTransient<DisplayCompanyControl>();
                    services.AddTransient<SettingsControl>();
                    services.AddTransient<FormOrganizationControl>();
                    services.AddTransient<TransactionsControl>();
                    services.AddTransient<NewTransactionControl>();
                    services.AddTransient<EditTransactionControl>();
                    #endregion

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
            loginWindow.ShowDialog();
            if (loginWindow.IsLoginSuccessful)
            {
                return;
            }
            else
            {
                Shutdown();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }
}
