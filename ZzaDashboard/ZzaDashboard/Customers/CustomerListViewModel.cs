using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel : INotifyPropertyChanged
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
            get { return _customers; }
            set
            {
                if(_customers != value)
                {
                    _customers = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Customers"));
                }
            }
        }
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { 
                if(_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    DeleteCommand.RaiseCanExecuteChanged();
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCustomer"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
