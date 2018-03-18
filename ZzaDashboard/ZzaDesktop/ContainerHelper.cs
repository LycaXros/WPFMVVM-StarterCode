﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using ZzaDesktop.Services;

namespace ZzaDesktop
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;

        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<ICustomersRepository, CustomersRepository>(
                    new Unity.Lifetime.ContainerControlledLifetimeManager());
        }
        public static IUnityContainer Container => _container; 
    }
}
