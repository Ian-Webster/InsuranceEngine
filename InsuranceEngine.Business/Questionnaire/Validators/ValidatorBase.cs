using System;

namespace InsuranceEngine.Business.Questionnaire.Validators
{
    public class ValidatorBase<T>
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
                }

                return _instance;
            }
        }

    }
}
