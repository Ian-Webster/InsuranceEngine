using System;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class QuoteQuestionAnswerDTO
    {
        [DataMember]
        public int QuoteQuestionAnswerID { get; set; }
        [DataMember]
        public int QuoteID { get; set; }
        [DataMember]
        public int QuestionID { get; set; }
        public int? QuestionPossibleAnswerID { get; set; }
        public string Answer { get; set; }

    }
}
