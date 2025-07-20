using App.BLL;
using App.Entities.Enums;
using App.Entities.Models;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Services.State;
using MyApp.WPF.UserControls.Admin.Employees;
using MyApp.WPF.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MyApp.WPF.Windows.Identity
{
    public partial class LoginWindow : Window
    {
        public bool IsLoginSuccessful { get; private set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;
        private readonly IStateService _stateService;
        public LoginWindow(IStateService stateService,IServiceProvider serviceProvider, IAuthService authService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _stateService = stateService;
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EmailTxt.Focus();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userVm = this.DataContext as LoginViewModel;

                if(userVm.IsValid)
                {
                    var result = await _authService.LoginAsync(userVm.Email, userVm.Password);
                    if (result.State)
                    {
                        IsLoginSuccessful = true;
                        _stateService.UserId = result.Data.Id;
                        if (result.Data.UserType == UserType.Admin)
                        {
                            var mainWindow = _serviceProvider.GetRequiredService<Admin.MainWindow>();
                            mainWindow.Show();
                        }
                        else if (result.Data.UserType == UserType.Employee)
                        {
                            var mainWindow = _serviceProvider.GetRequiredService<Employee.MainWindow>();
                            mainWindow.Show();
                        }
                        this.Close();
                        return;
                    }
                    MessageBox.Show(result.Message, "فشل التحقق من الهوية", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PasswordTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as Telerik.Windows.Controls.RadPasswordBox;
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = passwordBox.Password;
            }
        }

        private void Input_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LoginBtn_Click(sender, e);
            }
        }

        
    }
}
