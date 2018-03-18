using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaData;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    class AddEditCustomerViewModel : ViewModelBase
    {
        private bool _editMode;
        private Customer _editingCustomer = null;
        private ICustomersRepository _repo;

        public AddEditCustomerViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        public bool EditMode
        {
            get => _editMode;
            set => SetProperty(ref _editMode, value);
        }

        private SimpleEditableCustomer _customer = null;

        public SimpleEditableCustomer Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }


        public void SetCustomer(Customer obj)
        {
            _editingCustomer = obj;
            if (Customer != null) Customer.ErrorsChanged -= RaiseCanExecuteChanged;
            Customer = new SimpleEditableCustomer();
            Customer.ErrorsChanged += RaiseCanExecuteChanged;
            CopyCustomer(obj, Customer);
        }

        private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void CopyCustomer(Customer source, SimpleEditableCustomer target)
        {
            target.Id = source.Id;
            if (EditMode)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                target.Phone = source.Phone;
                target.Email = source.Email;
            }
        }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done = delegate { };

        private void OnCancel()
        {
            Done();
        }
        private async void OnSave()
        {
            UpdateCustomer(Customer, _editingCustomer);
            if (EditMode)
            {
                await _repo.UpdateCustomerAsync(_editingCustomer);
            }
            else
            {
                await _repo.AddCustomerAsync(_editingCustomer);
            }
            Done();
        }

        private void UpdateCustomer(SimpleEditableCustomer source, Customer target)
        {

            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.Email = source.Email;
        }

        private bool CanSave()
        {
            return !Customer.HasErrors;
        }
    }
}
