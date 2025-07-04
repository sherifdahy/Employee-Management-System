using App.BLL;
using App.Entities.Enums;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
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
    public partial class NewEmployeeControl : UserControl
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IServiceProvider _serviceProvider;
        public NewEmployeeControl(IEmployeeService employeeService,IMapper mapper,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _employeeService = employeeService;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
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

        private async void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var registerVM = this.DataContext as RegisterViewModel;
                if (registerVM.IsValid)
                {
                    var appUser = _mapper.Map<ApplicationUser>(registerVM);
                    await _employeeService.CreateAsync(appUser);
                    this.Content = _serviceProvider.GetRequiredService<NewEmployeeControl>();
                    MessageBox.Show(
                        "✅ تم حفظ بيانات الموظف بنجاح.",
                        "نجاح العملية",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.OK,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
    }
}
