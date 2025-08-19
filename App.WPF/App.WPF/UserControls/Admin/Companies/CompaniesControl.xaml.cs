using App.BLL.Manager;
using App.Entities.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.UserControls.Shared;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MyApp.WPF.UserControls.Admin.Companies
{
    public partial class CompaniesControl : UserControl
    {
        private readonly IBLayerManager _manager;
        private readonly IServiceProvider _serviceProvider;
        
        public CompaniesControl(IBLayerManager manager,IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _manager = manager;
            _serviceProvider = serviceProvider;

            Loaded += UserControl_Loaded;
            CompaniesDataPager.PageIndexChanged += CompaniesDataPager_PageIndexChanged;
            
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await UsePagination();
        }
        private async void CompaniesDataGrid_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            try
            {
                if (e.Row.DataContext is not Company tempCompany)
                    return;

                var result = await _manager.CompanyService.GetByIdAsync(tempCompany.Id);

                if (!result.State)
                    return;

                this.Content = ActivatorUtilities.CreateInstance<DisplayCompanyControl>(_serviceProvider, result.Data.ToDisplayViewModel());
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }
        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var confirm = DialogService.Confirm("سيتم مسح الشركة للأبد.", "تأكيد عمليه المسح");
                if (!confirm)
                    return;

                if (sender is not Button { DataContext: Company company })
                    return;

                var deleteResult = await _manager.CompanyService.DeleteAsync(company.Id);
                if (!deleteResult.State)
                {
                    DialogService.ShowError(deleteResult.Message);
                    return;
                }

                UserControl_Loaded(this, new RoutedEventArgs());
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not Button { DataContext: Company company })
                    return;

                var result = await _manager.CompanyService.GetByIdAsync(company.Id);

                if (!result.State)
                    return;

                this.Content = ActivatorUtilities.CreateInstance<EditCompaniesControl>(_serviceProvider, result.Data.ToViewModel(new()));
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }
        private async void CompaniesDataPager_PageIndexChanged(object sender, Telerik.Windows.Controls.PageIndexChangedEventArgs e)
        {
            await UsePagination();
        }
        private async Task UsePagination(int userId = default,string value = null)
        {
            try
            {
                CompaniesDataGrid.IsBusy = true;

                var pageSize = CompaniesDataPager.PageSize;
                var currentPage = CompaniesDataPager.PageIndex;

                var result = await _manager.CompanyService.GetAllAsync(currentPage, pageSize, userId, value);

                if (!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }

                CompaniesDataGrid.ItemsSource = result.Data.Items;
                CompaniesDataPager.ItemCount = result.Data.TotalCount;
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
            finally
            {
                CompaniesDataGrid.IsBusy = false;
            }
        }
        private async void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is not Telerik.Windows.Controls.RadWatermarkTextBox radWatermarkTextBox)
                return;

            await UsePagination(value: radWatermarkTextBox.Text);
        }

        private async void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog()
                {
                    Title = "اختر مكان حفظ الملف",
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                    FileName = $"{DateTime.UtcNow:yyyy-MM-dd_HH-mm-ss} - Export File"
                };

                if (saveDialog.ShowDialog() != true)
                    return;

                string filePath = saveDialog.FileName;
                var result = await _manager.DataSync.ExportToFileAsync(filePath);

                if (!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }

                DialogService.ShowSuccess("تم حفظ البيانات في المجلد بنجاح.");
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }

        private async void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileDialog = new OpenFileDialog()
                {
                    Title = "اختر مكان الملف",
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                };

                if (fileDialog.ShowDialog() != true)
                    return;

                string filePath = fileDialog.FileName;
                var result = await _manager.DataSync.ImportFromFileAsync(filePath);

                if (!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }

                DialogService.ShowSuccess($"Importing file {filePath}");
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }

        }
    }
}
