﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel
    {
        private ICustomersRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;

        public CustomerListViewModel()
        {
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }

        public async void LoadCustomers()
        {

            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            Customers = new ObservableCollection<Customer>(
                await _repo.GetCustomersAsync());
        }

        private bool CanDelete()
        {
            return SelectedCustomer != null;
        }

        private void OnDelete()
        {
            Customers.Remove(SelectedCustomer);
        }

        public RelayCommand DeleteCommand { get; private set; }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => _customers = value;
        }
        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }


    }
}
