using App.BLL.Dependencies.Interfaces;
using App.Entities.Models;
using MediaFoundation;
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

namespace MyApp.WPF.UserControls.Shared
{
    /// <summary>
    /// Interaction logic for DisplayCompanyControl.xaml
    /// </summary>
    public partial class DisplayCompanyControl : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBrowserService _browserService;
        public DisplayCompanyControl(IBrowserService browserService,IServiceProvider serviceProvider,Company company)
        {
            InitializeComponent();
            this.DataContext = company;
            _serviceProvider = serviceProvider;
            _browserService = browserService;
        }
        private void EmailDataGrid_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            var email = e.Row.DataContext as Email;
            if(email != null)
            {
                _browserService.Open(email,1);

            }
        }
    }
}
