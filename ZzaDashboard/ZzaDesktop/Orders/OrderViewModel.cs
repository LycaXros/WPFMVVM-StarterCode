
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZzaDesktop.Orders
{
    class OrderViewModel : ViewModelBase
    {
        private Guid _customerId;

        public Guid CustomerId
        {
            get => _customerId;
            set => SetProperty(ref _customerId, value);
        }
    }
}
