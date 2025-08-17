using App.BLL;
using App.BLL.DataSync;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.UserControls.Shared;
using MyApp.WPF.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MyApp.WPF.UserControls.Admin.Companies
{
    public partial class CompaniesControl : UserControl
    {
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDataSync _dataSync;
        public CompaniesControl(IDataSync dataSync,ICompanyService companyService, IMapper mapper, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _companyService = companyService;
            _dataSync = dataSync;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            Loaded += UserControl_Loaded;
            CompaniesDataPager.PageIndexChanged += CompaniesDataPager_PageIndexChanged;
            SearchBox.LostFocus += SearchBox_LostFocus;
            SearchBox.KeyUp += SearchBox_KeyUp;
            
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await UsePagination();
        }
        private void CompaniesDataGrid_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            try
            {
                var company = e.Row.DataContext as Company;
                if (company != null)
                {
                    this.Content = ActivatorUtilities.CreateInstance<DisplayCompanyControl>(_serviceProvider, company.ToDisplayViewModel());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("سيتم مسح الشركة للأبد.", "تأكيد عمليه المسح", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RightAlign);
                if (result == MessageBoxResult.Yes)
                {
                    var button = sender as Button;
                    var company = button?.DataContext as Company;
                    if (company != null)
                    {
                        await _companyService.DeleteAsync(company.Id);
                        UserControl_Loaded(this, new RoutedEventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                var company = button.DataContext as Company;
                if (company != null)
                {
                    var companyVM = _mapper.Map<CompanyViewModel>(company);
                    this.Content = ActivatorUtilities.CreateInstance<EditCompaniesControl>(_serviceProvider, companyVM);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void CompaniesDataPager_PageIndexChanged(object sender, Telerik.Windows.Controls.PageIndexChangedEventArgs e)
        {
            await UsePagination();
        }
        private async Task UsePagination(int userId = default,string value = null)
        {
            var pageSize = CompaniesDataPager.PageSize;
            var currentPage = CompaniesDataPager.PageIndex;
            var paginationResult = await _companyService.GetAllAsync(currentPage, pageSize,userId, value);
            CompaniesDataGrid.ItemsSource = paginationResult.Items;
            CompaniesDataPager.ItemCount = paginationResult.TotalCount;
        }
        private async void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var text = sender as Telerik.Windows.Controls.RadWatermarkTextBox;
                if (text == null) return;
                await UsePagination(value: text.Text);
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
                var text = sender as Telerik.Windows.Controls.RadWatermarkTextBox;
                if (text == null) return;

                if (e.RoutedEvent == KeyUpEvent && e.Key != Key.Enter) return;

                await UsePagination(value: text.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog()
            {
                Title = "اختر مكان حفظ الملف",
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FileName = $"{DateTime.UtcNow:yyyy-MM-dd_HH-mm-ss} - Export File"
            };

            if(saveDialog.ShowDialog() != true)
                return;

            string filePath = saveDialog.FileName;
            var result = await _dataSync.ExportToFileAsync(filePath);

            if (!result.State)
            {
                DialogService.ShowError(result.Message);
                return;
            }

            DialogService.ShowSuccess("تم حفظ البيانات في المجلد بنجاح.");
        }

        private async void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog()
            {
                Title = "اختر مكان الملف",
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
            };

            if(fileDialog.ShowDialog() != true)
                return;
            
            string filePath = fileDialog.FileName;
            var result = await _dataSync.ImportFromFileAsync(filePath);

            if(!result.State)
            {
                DialogService.ShowError(result.Message);
                return;
            }

            DialogService.ShowSuccess($"Importing file {filePath}");

        }
    }
}
