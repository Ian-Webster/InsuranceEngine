using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class QuestionPossibleAnswerForDisplayDTO
    {
        [DataMember]
        public int QuestionID { get; set; }
        [DataMember]
        public int QuestionPossibleAnswerID { get; set; }
        [DataMember]
        public string AnswerText { get; set; }
        [DataMember]
        public string AnswerValue { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }

        public List<PageQuestionConditionalDisplayForDisplayDTO> DisplayConditions { get; set; }

    }
}
