using System;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    [Serializable]
    public class SchemeForGridDTO
    {

        [DataMember]
        public int SchemeID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Code { get; set; }

    }
}
