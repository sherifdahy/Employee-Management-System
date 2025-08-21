using App.BLL;
using App.Entities.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.Services.State;
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
using Telerik.Windows.Controls;

namespace MyApp.WPF.UserControls.Shared
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        private readonly IAuthService _authService;
        private readonly IStateService _stateService;
        public SettingsControl(IAuthService authService,IStateService stateService)
        {
            InitializeComponent();
            _authService = authService;
            _stateService = stateService;
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await _authService.GetByIdAsync(_stateService.UserId);
                if(result.State)
                {
                    
                    this.DataContext = result.Data.ToViewModel(new ApplicationUserViewModel());
                    return;
                }
                else
                {
                    DialogService.ShowError(result.Message);
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }

        private async void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = this.DataContext as ApplicationUserViewModel;
                if (vm != null)
                {
                    if (vm.IsValid)
                    {
                        var result = await _authService.GetByIdAsync(_stateService.UserId);
                        if (result.State)
                        {
                            vm.ToModel(result.Data);
                            await _authService.UpdateAsync(result.Data);
                            MessageBox.Show("تم الحفظ بنجاح", "عملية ناجة", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show(result.Message, "Ooops.", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("حدث خطأ ما.", "Ooops.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);

            }
        }

        private void PasswordTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as RadPasswordBox;
            var vm = this.DataContext as ApplicationUserViewModel;
            if (vm != null)
            {
                vm.Password = box.Password;
            }
        }
        private void ConfirmPasswordTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as RadPasswordBox;
            var vm = this.DataContext as ApplicationUserViewModel;
            if (vm != null)
            {
                vm.ConfirmPassword = box.Password;
            }
        }
    }
}
