using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class QuoteDTO
    {
        [DataMember]
        public int QuoteID { get; set; }
        [DataMember]
        public int SchemeID { get; set; }
        [DataMember]
        [Display(Name = "Reference")]
        public string Reference { get; set; }
        [DataMember]
        [Display(Name = "Quote Date")]
        public DateTime QuoteDate { get; set; }

        public List<QuoteQuestionAnswerDTO> Answers { get; set; }

        public List<QuoteQuestionAnswerDTO> RemovedAnswers { get; set; }

        public List<PageQuestionVisibilityDTO> VisbilitySettings { get; set; }

    }
}
