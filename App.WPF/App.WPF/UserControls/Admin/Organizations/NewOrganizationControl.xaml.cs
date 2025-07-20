using App.BLL;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.ViewModels;
using MyApp.WPF.Windows.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Diagrams;

namespace MyApp.WPF.UserControls.Admin.Organizations
{
    public partial class NewOrganizationControl : UserControl
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOrganizationService _organizationService;
        public NewOrganizationControl(IOrganizationService organizationService,IMapper mapper,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _organizationService = organizationService;
        }

        private async void AddOrgBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var organizationVM = this.DataContext as OrganizationViewModel;
                if(organizationVM.IsValid)
                {
                    var organization = _mapper.Map<Organization>(organizationVM);
                    await _organizationService.CreateAsync(organization);
                    ResetUserControl();
                    MessageBox.Show(
                        "تم الإضافة بنجاح.",
                        "نجاح العملية",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddSelectorBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = _serviceProvider.GetRequiredService<AddSelectorWindow>();
            window.ShowDialog();
            if(window.DialogResult.GetValueOrDefault())
            {
                var SelectorVM = window.DataContext as SelectorViewModel;
                if(SelectorVM != null)
                {
                    var orgVM = this.DataContext as OrganizationViewModel;
                    if (orgVM != null)
                    {
                        orgVM.Selectors.Add(SelectorVM);
                    }
                }
            }
        }
        #region Helper
        private void ResetUserControl()
        {
            this.Content = _serviceProvider.GetRequiredService<NewOrganizationControl>();
        }
        #endregion

        
    }
}
