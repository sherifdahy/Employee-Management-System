using App.BLL;
using App.BLL.Dependencies.Interfaces;
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
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace MyApp.WPF.UserControls.Shared
{
    /// <summary>
    /// Interaction logic for DisplayCompanyControl.xaml
    /// </summary>
    public partial class DisplayCompanyControl : UserControl
    {
        private readonly IBrowserService _browserService;
        private readonly IEmailService _emailService;
        public DisplayCompanyControl(IBrowserService browserService,IEmailService emailService,DisplayCompanyViewModel companyViewModel)
        {
            InitializeComponent();

            _browserService = browserService;
            _emailService = emailService;
            this.DataContext = companyViewModel;
        }

        private  void EmailsGrid_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            
        }

        

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void EmailsDataGrid_RowActivated(object sender, RowEventArgs e)
        {
            try
            {
                var emailVM = e.Row.DataContext as EmailViewModel;
                if (emailVM == null)
                    throw new InvalidOperationException("الحساب غير موجود");

                var result = await _emailService.GetByIdAsync(emailVM.Id);

                if (!result.State)
                    throw new ApplicationException(result.Message);

                _browserService.Open(result.Data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OrganizationsDataGrid_RowActivated(object sender, RowEventArgs e)
        {
            var grid = sender as RadGridView;
            if (e.Row is GridViewRow row)
            {
                if (row.DetailsVisibility == Visibility.Visible)
                    grid.CollapseHierarchyItem(row.Item);
                else
                    grid.ExpandHierarchyItem(row.Item);
            }
        }


    }
}
