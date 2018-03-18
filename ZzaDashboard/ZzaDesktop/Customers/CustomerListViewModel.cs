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
    class CustomerListViewModel : ViewModelBase
    {
        private ICustomersRepository _repo;

        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlacerOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        private List<Customer> _allCustomers;

        public async void LoadCustomers()
        {

            _allCustomers= await _repo.GetCustomersAsync();
            Customers = new ObservableCollection<Customer>(_allCustomers);
        }

        private string _searchInput;

        public string SearchInput
        {
            get { return _searchInput; }
            set
            {
                SetProperty(ref _searchInput, value);
                FilterCustomers(_searchInput);
            }
        }

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
                return;
            }
            else
            {
                Customers = new ObservableCollection<Customer>(
                    _allCustomers.Where(c => c.FullName.ToLower().Contains(searchInput.ToLower())));
            }

        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        public RelayCommand AddCustomerCommand { get; private set; }
        public RelayCommand<Customer> EditCustomerCommand { get; private set; }
        public RelayCommand ClearSearchCommand { get; private set; }

        public event Action<Guid> PlaceOrderRequested = delegate { };
        public event Action<Customer> AddCustomerRequested = delegate { };
        public event Action<Customer> EditCustomerRequested = delegate { };

        private void OnPlacerOrder(Customer customer)
        {
            PlaceOrderRequested(customer.Id);
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested(new Customer() { Id = Guid.NewGuid() });
        }

        private void OnEditCustomer(Customer cust)
        {
            EditCustomerRequested(cust);
        }

        private void OnClearSearch()
        {
            SearchInput = null;
        }
    }
}
