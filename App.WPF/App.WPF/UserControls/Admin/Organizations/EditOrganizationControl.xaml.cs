using App.BLL;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyApp.WPF.UserControls.Admin.Organizations
{
    /// <summary>
    /// Interaction logic for EditOrganizationControl.xaml
    /// </summary>
    public partial class EditOrganizationControl : UserControl
    {
        private readonly IOrganizationService _organizationService;
        private readonly IServiceProvider _serviceProvider;
        public EditOrganizationControl(IServiceProvider serviceProvider,IOrganizationService organizationService,OrganizationViewModel organizationViewModel)
        {
            InitializeComponent();
            _organizationService = organizationService;
            _serviceProvider = serviceProvider;
            this.FormControl.DataContext = organizationViewModel;
        }

        private void SaveChangesBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.FormControl.Content = ActivatorUtilities.CreateInstance<FormOrganizationControl>(_serviceProvider);
        }
    }
}
