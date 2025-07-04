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
        public OwnerViewModel _owner;
        public NewOwnerWindow(IUnitOfWork unitOfWork,IMapper mapper)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private void AddOwnerBtn_Click(object sender, RoutedEventArgs e)
        {
            _owner = this.DataContext as OwnerViewModel;
            this.DialogResult = true;
            
        }
    }
}
