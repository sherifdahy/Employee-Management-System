using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.UserControls.Employee.Companies;
using MyApp.WPF.UserControls.Employee.Home;
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
using System.Windows.Shapes;

namespace MyApp.WPF.Windows.Employee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void CompanyPageBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainSection.Content = _serviceProvider.GetRequiredService<CompaniesControl>();
        }

        private void HomePageBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainSection.Content = _serviceProvider.GetRequiredService<HomeControl>();
        }
    }
}
