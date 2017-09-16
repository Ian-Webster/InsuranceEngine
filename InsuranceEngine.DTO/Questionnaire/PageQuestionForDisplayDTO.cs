using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class PageQuestionForDisplayDTO
    {
        [DataMember]
        public int PageQuestionID { get; set; }

        [DataMember]
        public string QuestionTemplatePath { get; set; }

        [DataMember]
        public int QuestionID { get; set; }

        [DataMember]
        public int DisplayOrder { get; set; }

        [DataMember]
        public string QuestionText { get; set; }

        [DataMember]
        public List<QuestionPossibleAnswerForDisplayDTO> PossibleAnswers { get; set; }

        [DataMember]
        public string QuestionName { get; set; }

        [DataMember]
        public bool DefaultShow { get; set; }

        [DataMember]
        public bool IsRequired { get; set; }

        [DataMember]
        public List<PageQuestionValidationDTO> Validators { get; set; }

        [DataMember]
        public bool HasPossibleAnswers { get; set; }

        [DataMember]
        public List<PageQuestionConditionalDisplayForDisplayDTO> DependantQuestions { get; set; }

        [DataMember]
        public bool HasDisplayConditions { get; set; }

    }
}
