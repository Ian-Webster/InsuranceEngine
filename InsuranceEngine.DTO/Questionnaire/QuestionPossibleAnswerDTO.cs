using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class QuestionPossibleAnswerDTO
    {
        [DataMember]
        public int QuestionPossibleAnswerID { get; set; }
        [DataMember]
        [Display(Name = "Question")]
        [Required(ErrorMessage = "You must select a Question")]
        public int QuestionID { get; set; }
        [DataMember]
        [Display(Name = "Answer Text")]
        [Required(ErrorMessage = "You must enter the Answer Text")]
        [MaxLength(50, ErrorMessage = "Answer Text cannot be longer than 50 characters")]
        public string AnswerText { get; set; }
        [DataMember]
        [Display(Name = "Answer Value")]
        [Required(ErrorMessage = "You must enter the Answer Value")]
        [MaxLength(20, ErrorMessage = "Answer Value cannot be longer than 20 characters")]
        public string AnswerValue { get; set; }
        [DataMember]
        [Display(Name = "Display Order")]
        [Required(ErrorMessage = "You must enter the Display Order")]
        public int DisplayOrder { get; set; }

    }
}
