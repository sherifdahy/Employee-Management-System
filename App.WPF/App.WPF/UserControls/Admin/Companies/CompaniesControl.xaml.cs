using Interfaces;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyApp.WPF.UserControls.Admin.Companies
{
    public partial class CompaniesControl : UserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompaniesControl(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CompaniesDataGrid.IsBusy = true;
            CompaniesDataGrid.ItemsSource = await _unitOfWork.Companies.GetAllAsync();
            CompaniesDataGrid.IsBusy = false;

        }
    }
}
