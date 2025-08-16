using App.BLL;
using App.Entities.Enums;
using App.Entities.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyApp.WPF.UserControls.Admin.Employees
{
    /// <summary>
    /// Interaction logic for FormEmployeeControl.xaml
    /// </summary>
    public partial class FormEmployeeControl : UserControl
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IServiceProvider _serviceProvider;
        public FormEmployeeControl(IEmployeeService employeeService, IMapper mapper, IServiceProvider serviceProvider,ApplicationUserViewModel applicationUserViewModel)
        {
            InitializeComponent();
            _employeeService = employeeService;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            this.DataContext = applicationUserViewModel;
        }


        

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var roles = Enum.GetValues(typeof(UserType));
                RoleComboBox.ItemsSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is Telerik.Windows.Controls.RadPasswordBox passwordBox)
            {
                if(this.DataContext is ApplicationUserViewModel applicationUserViewModel)
                {
                    applicationUserViewModel.Password = passwordBox.Password;
                }
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is Telerik.Windows.Controls.RadPasswordBox passwordBox)
            {
                if (this.DataContext is ApplicationUserViewModel applicationUserViewModel)
                {
                    applicationUserViewModel.ConfirmPassword = passwordBox.Password;
                }
            }
        }
    }
}
