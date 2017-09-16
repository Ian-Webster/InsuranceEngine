using System;

namespace InsuranceEngine.Business.Questionnaire
{
    public class QuestionnaireBase<T>
    {

        private static T _instance = default(T);
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance<T>();
                }

                return _instance;
            }
        }

        internal QuestionnaireBase()
        {
        }

    }
}
