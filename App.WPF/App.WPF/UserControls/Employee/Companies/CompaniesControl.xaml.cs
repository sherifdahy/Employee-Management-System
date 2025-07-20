using App.BLL;
using App.Entities.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Services.State;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyApp.WPF.UserControls.Employee.Companies
{
    /// <summary>
    /// Interaction logic for CompaniesControl.xaml
    /// </summary>
    public partial class CompaniesControl : UserControl
    {
        private readonly ICompanyService _companyService;
        private readonly IStateService _stateService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public CompaniesControl(IMapper mapper,ICompanyService companyService,IStateService stateService,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _companyService = companyService;
            _stateService = stateService;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CompaniesDataGrid.ItemsSource = await _companyService.GetRelatedCompaniesAsync(_stateService.UserId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textbox = sender as  Telerik.Windows.Controls.RadWatermarkTextBox;
                CompaniesDataGrid.ItemsSource = await _companyService.SearchAsync(textbox.Text);
            }
            catch (Exception ex) 
            { 

                MessageBox.Show(ex.Message,"حدث خطأ ما",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void CompaniesDataGrid_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            try
            {
                var company = e.Row.DataContext as Company;
                if (company != null)
                {
                    this.Content = ActivatorUtilities.CreateInstance<DisplayCompanyControl>(_serviceProvider, company);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
