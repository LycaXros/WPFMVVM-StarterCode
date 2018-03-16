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

        private SimpleEditableCustomer _customer =null;

        public SimpleEditableCustomer Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer , value); }
        }


        public void SetCustomer(Customer obj)
        {
            _editingCustomer = obj;
            Customer = new SimpleEditableCustomer();
            CopyCustomer(obj, Customer);
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
    }
}
