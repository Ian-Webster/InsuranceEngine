using InsuranceEngine.Data.Mapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceEngine.Data.Mapping
{
    internal class Mapper
    {

        private static object SyncLock = new object();

        private static Boolean _objectMapped = false;
        private static Boolean ObjectMapped
        {
            get
            {
                return _objectMapped;
            }
            set
            {
                _objectMapped = value;
            }
        }

        /// <summary>
        /// Sets up maping for each of the mapped objects
        /// </summary>
        public static void Configure()
        {
            if (!ObjectMapped)
            {
                /* only map if none exists */
                lock (SyncLock) /* thread safety */
                {
                    QuestionnaireMappings.Map();

                    ObjectMapped = true;
                }
            }
        }

    }
}
