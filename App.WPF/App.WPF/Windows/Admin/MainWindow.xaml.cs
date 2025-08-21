using App.BLL;
using App.Entities.Models;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.Services.State;
using MyApp.WPF.UserControls;
using MyApp.WPF.UserControls.Admin;
using MyApp.WPF.UserControls.Admin.Companies;
using MyApp.WPF.UserControls.Admin.DailyTransactions;
using MyApp.WPF.UserControls.Admin.Employees;
using MyApp.WPF.UserControls.Admin.Organizations;
using MyApp.WPF.UserControls.Shared;
using MyApp.WPF.ViewModels;
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
using System.Windows.Threading;

namespace MyApp.WPF.Windows.Admin
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStateService _stateService;
        private readonly IAuthService _authService;
        private readonly DispatcherTimer timer;

        public MainWindow(IServiceProvider serviceProvider,IStateService stateService,IAuthService authService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _stateService = stateService;
            _authService = authService;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await _authService.GetByIdAsync(_stateService.UserId);
            
            if(!result.State)
            {
                DialogService.ShowError(result.Message);
                return;
            }

            NameOfUserTxt.Text = $"أهلاً، {result?.Data?.Name}  👋";
            RoleOfUser.Text = $"الصلاحية : {result?.Data?.UserType}";

            HomePageBtn_Click(HomePageBtn, e);
        }
       
        private void HomePageBtn_Click(object sender, RoutedEventArgs e)
        {

            MainSection.Content = _serviceProvider.GetRequiredService<HomeControl>();
            CurrentPageName.Text = "الصفحة الرئيسية";
            SetActiveButton((Button)sender);
        }

        private void CompanyPageBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<CompaniesControl>();
            CurrentPageName.Text = "عرض الشركات";
            SetActiveButton((Button)sender);

        }

        private void NewCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<NewCompanyControl>();
            CurrentPageName.Text = "شركة جديدة";
            SetActiveButton((Button)sender);

        }

        private void OrganizationsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<OrganizationsControl>();
            CurrentPageName.Text = "عرض المنصات";
            SetActiveButton((Button)sender);

        }

        private void AddOrganizationBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<NewOrganizationControl>();
            CurrentPageName.Text = "منصة جديدة";
            SetActiveButton((Button)sender);

        }

        private void EmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<EmployeesControl>();
            CurrentPageName.Text = "عرض الموظفين";
            SetActiveButton((Button)sender);

        }
        private void CreateEmplyee_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<NewEmployeeControl>();
            CurrentPageName.Text = "موظف جديد";
            SetActiveButton((Button)sender);

        }

        private void UserAccessBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<EmployeeAccessControl>();
            CurrentPageName.Text = "صلاحيات الموظف";
            SetActiveButton((Button)sender);

        }
        private void TransactionsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<TransactionsControl>();
            CurrentPageName.Text = "العهد";
            SetActiveButton((Button)sender);

        }
        private void NewTransactionBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = ActivatorUtilities.CreateInstance<NewTransactionControl>(_serviceProvider);
            CurrentPageName.Text = "انشاء عهدة";
            SetActiveButton((Button)sender);
        }
        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainSection.Content = _serviceProvider.GetRequiredService<SettingsControl>();
            CurrentPageName.Text = "الاعدادات";
            SetActiveButton((Button)sender);

        }
        private void SetActiveButton(Button activeBtn)
        {
            ClearTagsFromChildren(SidebarContent);
            activeBtn.Tag = "Selected";
        }

        private void ClearTagsFromChildren(Panel parent)
        {
            foreach (var child in parent.Children)
            {
                if (child is Button btn)
                {
                    btn.Tag = null;
                }
                else if (child is Panel panel) // لو جوه StackPanel أو Grid أو غيره
                {
                    ClearTagsFromChildren(panel);
                }
                else if (child is ContentControl cc && cc.Content is Panel innerPanel)
                {
                    // علشان نمسك الـ StackPanel اللي جوه RadExpander
                    ClearTagsFromChildren(innerPanel);
                }
            }
        }

    }
}
