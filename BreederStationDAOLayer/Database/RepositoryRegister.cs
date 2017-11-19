using BreederStationDataLayer.Orm.Dao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BreederStationDAOLayer.Database
{
    public class RepositoryRegister
    {
        private static RepositoryRegister instance;
        private Dictionary<Type, Object> repositoryRegister = new Dictionary<Type, object>();

        private RepositoryRegister()
        {

        }

        public static RepositoryRegister getInstance()
        {
            if(instance == null)
            {
                instance = new RepositoryRegister();
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
