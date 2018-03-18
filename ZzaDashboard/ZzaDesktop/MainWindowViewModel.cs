using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaData;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;
using ZzaDesktop.Services;
using Unity;

namespace ZzaDesktop
{
    public class MainWindowViewModel : ViewModelBase
    {
        private CustomerListViewModel _customerListViewModel;
        private OrderViewModel _orderViewModel = new OrderViewModel();
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
        private AddEditCustomerViewModel _addEditViewModel;
       

        private ViewModelBase _currentViewModel;
        public MainWindowViewModel()
        {
            _customerListViewModel = ContainerHelper.Container.Resolve<CustomerListViewModel>();
            _addEditViewModel = ContainerHelper.Container.Resolve<AddEditCustomerViewModel>();

            NavCommand = new RelayCommand<string>(OnNav);
            _customerListViewModel.PlaceOrderRequested += NavToOrder;
            _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
            _addEditViewModel.Done += NavToCustomerList;
        }

        private void NavToEditCustomer(Customer obj)
        {
            _addEditViewModel.EditMode = true;
            _addEditViewModel.SetCustomer(obj);
            CurrentViewModel = _addEditViewModel;
        }

        private void NavToAddCustomer(Customer obj)
        {
            _addEditViewModel.EditMode = false ;
            _addEditViewModel.SetCustomer(obj);
            CurrentViewModel = _addEditViewModel;
        }

        public ViewModelBase CurrentViewModel {
            get => _currentViewModel;
            set => SetProperty(member: ref _currentViewModel, val: value);
        }

        public RelayCommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "orderPrep":
                    CurrentViewModel = _orderPrepViewModel;
                    break;
                case "customers":
                default:
                    CurrentViewModel = _customerListViewModel;
                    break;
            }
        }
        private void NavToOrder(Guid customerId)
        {
            _orderViewModel.CustomerId = customerId;
            CurrentViewModel = _orderViewModel;
        }

        private void NavToCustomerList()
        {
            CurrentViewModel = _customerListViewModel;
        }
    }
}
