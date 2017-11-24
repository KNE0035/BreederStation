using BreederStationDataLayer.Orm.Dao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BreederStationBussinessLayer.Service
{
    public class ServiceRegister
    {
        private static ServiceRegister instance;
        private Dictionary<Type, Object> repositoryRegister = new Dictionary<Type, object>();

        private ServiceRegister()
        {

        }

        public static ServiceRegister getInstance()
        {
            if(instance == null)
            {
                instance = new ServiceRegister();
            }
            return instance;
        }

        public void Register(Type type, Object repository)
        {
            repositoryRegister.Add(type, repository);
        }

        public T Get<T> ()
        {
            Object obj;
            repositoryRegister.TryGetValue(typeof(T), out obj);
            return (T)obj;
        }
    }
}
