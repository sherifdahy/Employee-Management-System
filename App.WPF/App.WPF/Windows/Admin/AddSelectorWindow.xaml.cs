using App.Entities.Enums;
using MyApp.WPF.ViewModels;
using System;
using System.Windows;
using Telerik.Windows.Controls;


namespace MyApp.WPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for AddSelectorWindow.xaml
    /// </summary>
    public partial class AddSelectorWindow : Window
    {
        public AddSelectorWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TypeComboBox.Items.AddRange(Enum.GetValues<SelectorType>());
            ContentTypeComboBox.Items.AddRange(Enum.GetValues<ContentType>());
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as SelectorViewModel;
            if(vm != null)
            {
                if(vm.IsValid)
                {
                    this.DialogResult = true;
                    this.Close();
                }
            }
        }

        
    }
}
