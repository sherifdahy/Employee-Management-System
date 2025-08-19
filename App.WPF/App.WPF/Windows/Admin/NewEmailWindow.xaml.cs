using App.BLL.Manager;
using App.Entities.Models;
using Interfaces;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace MyApp.WPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for NewEmailWindow.xaml
    /// </summary>
    public partial class NewEmailWindow : Window
    {
        private readonly IBLayerManager _manager;
        public NewEmailWindow(IBLayerManager manager,EmailViewModel emailViewModel)
        {
            InitializeComponent();
            _manager = manager;
            this.DataContext = emailViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrganizations();
        }

        private async void LoadOrganizations()
        {
            try
            {
                var organizationsResult = await _manager.OrganizationService.GetAllAsync();

                if(!organizationsResult.State)
                {
                    DialogService.ShowError(organizationsResult.Message);
                    return;
                }

                if(organizationsResult.Data is null || organizationsResult.Data.Count() == 0)
                {
                    DialogService.ShowWarning("لا يوجد منظمات لعرضها");
                    return;
                }

                OrgCombobox.ItemsSource = organizationsResult.Data;

                OrgCombobox.DisplayMemberPath = nameof(Organization.Name);
                OrgCombobox.SelectedValuePath = nameof(Organization.Id);

                OrgCombobox.SelectedIndex = 0;
                
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }

        private void AddEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is EmailViewModel emailViewModel)
            {
                if (emailViewModel.ValidateAll())
                {
                    this.DialogResult = true;
                }
            }
        }

        private void RadPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is RadPasswordBox passwordBox)
            {
                if (this.DataContext is EmailViewModel emailViewModel)
                {
                    emailViewModel.Password = passwordBox.Password;
                }
            }
        }
    }
}
