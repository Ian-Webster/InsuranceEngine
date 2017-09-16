using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class QuestionTypeDTO
    {
        [DataMember]
        [Display(Name = "Question Type")]
        public int QuestionTypeID { get; set; }
        [DataMember]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter the Name")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [DataMember]
        [Display(Name = "Has Possible Answers")]
        public bool HasPossibleAnswers { get; set; }

    }
}
