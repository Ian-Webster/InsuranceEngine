using System;

namespace InsuranceEngine.Data.DataManagers
{
    public class DataManagerBase<T>
    {

        private static T _instance = default(T);
        /// <summary>
        /// Provides Singleton access to the data manager
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    //create the manager
                    _instance = Activator.CreateInstance<T>();

                    //assert the auto mapper configuration.
                    Mapping.Mapper.Configure();
                }

                return _instance;
            }
        }

    }
}
