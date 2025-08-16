using App.BLL;
using App.Entities;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using Repository;
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

namespace MyApp.WPF.UserControls.Admin.Organizations
{
    public partial class OrganizationsControl : UserControl
    {
        private readonly IOrganizationService _organizationService;
        private readonly IServiceProvider _serviceProvider;
        public OrganizationsControl(IOrganizationService organizationService,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _organizationService = organizationService;
            _serviceProvider = serviceProvider;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await _organizationService.GetAllAsync();
            if(result is null)
            {
                DialogService.ShowError(result.Message);
                return;
            }

            OrganizationDataGrid.ItemsSource = result.Data;
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn || btn.DataContext is not Organization org)
            {
                DialogService.ShowError(ErrorCatalog.Server.Unexpected.Message);
                return;
            }

            var result = await _organizationService.DeleteAsync(org.Id);

            if (result.State)
            {
                UserControl_Loaded(sender, e);
            }
            else
            {
                DialogService.ShowError(result.Message);
            }
        }


        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var org = btn.DataContext as Organization;
            if (org is null)
            {
                DialogService.ShowError(ErrorCatalog.Server.Unexpected.Message);
                return;
            }

            this.Content = ActivatorUtilities.CreateInstance<EditOrganizationControl>(_serviceProvider,org.ToViewModel());
        }
    }
}
