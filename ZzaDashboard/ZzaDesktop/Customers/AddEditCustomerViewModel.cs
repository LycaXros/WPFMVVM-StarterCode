using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaData;

namespace ZzaDesktop.Customers
{
    class AddEditCustomerViewModel:ViewModelBase
    {
        private bool _editMode;
        private Customer _editingCustomer = null;
        public bool EditMode { get => _editMode;
            set => SetProperty(ref _editMode, value); }

        public void SetCustomer(Customer obj)
        {
            _editingCustomer = obj;
        }
    }
}
