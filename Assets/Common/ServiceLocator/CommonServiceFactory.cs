using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ServiceLocator
{
    public class CommonServiceFactory<TServiceLocator, TServiceInterface, TService> : IServiceFactory<TServiceInterface>
        where TServiceLocator : Component
        where TService : Component, TServiceInterface
    {
        protected TServiceLocator serviceLocator;
        protected string serviceName;

        public CommonServiceFactory(TServiceLocator serviceLocator)
            : this(serviceLocator, typeof(TService).Name)
        {
        }

        public CommonServiceFactory(TServiceLocator serviceLocator, string serviceName)
        {
            this.serviceLocator = serviceLocator;
            this.serviceName = serviceName;
        }

        public TServiceInterface GetService()
        {
            var service = serviceLocator.transform.GetComponentInChildren<TServiceInterface>() as TService;
            if (service != null)
                return service;
            else
                return Create();
        }

        TService Create()
        {
            var service = new GameObject(serviceName).AddComponent<TService>();
            service.transform.SetParent(serviceLocator.transform);
            Initialize(service);
            return service;
        }

        protected virtual void Initialize(TService service)
        {
        }
    }
}
