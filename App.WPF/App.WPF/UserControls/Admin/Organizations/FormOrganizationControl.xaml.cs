using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.ViewModels;
using MyApp.WPF.Windows.Admin;
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
    /// <summary>
    /// Interaction logic for FormOrganizationControl.xaml
    /// </summary>
    public partial class FormOrganizationControl : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        public FormOrganizationControl(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        private void AddSelectorBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = _serviceProvider.GetRequiredService<AddSelectorWindow>();
            window.ShowDialog();
            if (window.DialogResult.GetValueOrDefault())
            {
                var SelectorVM = window.DataContext as SelectorViewModel;
                if (SelectorVM != null)
                {
                    var orgVM = this.DataContext as OrganizationViewModel;
                    if (orgVM != null)
                    {
                        orgVM.Selectors.Add(SelectorVM);
                    }
                }
            }
        }
    }
}
