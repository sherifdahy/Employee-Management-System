using App.Entities.Models;
using Interfaces;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace MyApp.WPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for NewEmailWindow.xaml
    /// </summary>
    public partial class NewEmailWindow : Window
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewEmailWindow(IUnitOfWork unitOfWork,EmailViewModel emailViewModel)
        {
            InitializeComponent();
            this._unitOfWork = unitOfWork;
            this.DataContext = emailViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrgCombobox.ItemsSource = await _unitOfWork.Organizations.GetAllAsync();
            OrgCombobox.DisplayMemberPath = nameof(Organization.Name);
            OrgCombobox.SelectedValuePath = nameof(Organization.Id);
        }

        private void AddEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is EmailViewModel emailViewModel)
            {
                if (emailViewModel.ValidateAll())
                {
                    this.DialogResult = true;
                }
            }
        }

        private void RadPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is RadPasswordBox passwordBox)
            {
                if (this.DataContext is EmailViewModel emailViewModel)
                {
                    emailViewModel.Password = passwordBox.Password;
                }
            }
        }
    }
}
