using App.Entities.Models;
using AutoMapper;
using Interfaces;
using MyApp.WPF.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace MyApp.WPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for NewOwnerWindow.xaml
    /// </summary>
    public partial class NewOwnerWindow : Window
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NewOwnerWindow(IUnitOfWork unitOfWork,IMapper mapper)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private void AddOwnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is OwnerViewModel owner)
            {
                if(owner.ValidateAll())
                {
                    this.DialogResult = true;
                }
            }
        }
    }
}
