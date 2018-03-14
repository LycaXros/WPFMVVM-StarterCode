using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaData;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    class CustomerListViewModel: ViewModelBase
    {
        private ICustomersRepository _repo = new CustomersRepository();

        public CustomerListViewModel()
        {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlacerOrder);
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        public async void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(
                await _repo.GetCustomersAsync());
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }

        public event Action<Guid> PlaceOrderRequested = delegate { };

        private void OnPlacerOrder(Customer customer)
        {
            PlaceOrderRequested(customer.Id);
        }
    }
}
