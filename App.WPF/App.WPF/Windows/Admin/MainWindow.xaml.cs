using App.Entities.Models;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.UserControls;
using MyApp.WPF.UserControls.Admin;
using MyApp.WPF.UserControls.Admin.Companies;
using MyApp.WPF.UserControls.Admin.Employees;
using MyApp.WPF.UserControls.Admin.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyApp.WPF.Windows.Admin
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        private void HomePageBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<HomeControl>();

        }

        private void CompanyPageBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<CompaniesControl>();
        }

        private void NewCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<NewCompanyControl>();
        }

        private void OrganizationsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<OrganizationsControl>();
        }

        private void AddOrganizationBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<NewOrganizationControl>();
        }

        private void EmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<EmployeesControl>();
        }
        private void CreateEmplyee_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<NewEmployeeControl>();
        }

        private void UserAccessBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<EmployeeAccessControl>();
        }
    }
}
