using App.BLL;
using App.DAL.Data;
using AutoMapper;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.WPF.ApplicationConfiguration;
using MyApp.WPF.UserControls;
using MyApp.WPF.UserControls.Admin;
using MyApp.WPF.UserControls.Admin.Companies;
using MyApp.WPF.UserControls.Admin.Employees;
using MyApp.WPF.UserControls.Admin.Organizations;
using MyApp.WPF.Windows;
using MyApp.WPF.Windows.Admin;
using MyApp.WPF.Windows.Employee;
using Repository;
using System.Windows;

namespace MyApp.WPF
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context , services) =>
                {
                    string connectionString = context.Configuration.GetSection("ConnectionStrings")["defaultConnection"];
                    #region Service Registration
                    services.AddTransient<IAuthService,AuthService>();
                    services.AddTransient<ICompanyService,CompanyService>();
                    services.AddTransient<IOrganizationService,OrganizationService>();
                    services.AddTransient<IEmployeeService,EmployeeService>();
                    #endregion
                    #region IUnitOfWork Registration
                    services.AddTransient<IUnitOfWork, UnitOfWork>();
                    #endregion
                    #region AutoMapper Registration
                    services.AddAutoMapper(typeof(AutoMapperProfile));
                    #endregion
                    #region Window Registration
                    services.AddTransient<LoginWindow>();
                    services.AddTransient<Windows.Admin.MainWindow>();
                    services.AddTransient<Windows.Employee.MainWindow>();
                    services.AddTransient<NewOwnerWindow>();
                    services.AddTransient<NewEmailWindow>();
                    #endregion
                    #region UserControl Registration
                    services.AddTransient<HomeControl>();
                    services.AddTransient<CompaniesControl>();
                    services.AddTransient<NewCompanyControl>();
                    services.AddTransient<NewOrganizationControl>();
                    services.AddTransient<OrganizationsControl>();
                    services.AddTransient<NewEmployeeControl>();
                    services.AddTransient<EmployeesControl>();
                    services.AddTransient<EmployeeAccessControl>();
                    #endregion

                    services.AddDbContext<ApplicationDbContext>(option =>
                    {
                        option.UseLazyLoadingProxies().UseSqlServer(connectionString);
                    });

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
