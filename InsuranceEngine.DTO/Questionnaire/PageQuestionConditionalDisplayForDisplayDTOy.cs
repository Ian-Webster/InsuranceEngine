using System;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class PageQuestionConditionalDisplayForDisplayDTO
    {
        [DataMember]
        public int TargetPageQuestionID { get; set; }
        [DataMember]
        public bool Hide { get; set; }
    }
}
