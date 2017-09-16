using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class QuestionDTO
    {
        [DataMember]
        public int QuestionID { get; set; }
        [DataMember]
        [Display(Name = "Scheme")]
        [Required(ErrorMessage = "You must select a Scheme")]
        public int SchemeID { get; set; }
        [DataMember]
        [Display(Name = "Question Template")]
        [Required(ErrorMessage = "You must select a Question Template")]
        public int QuestionTemplateID { get; set; }
        [DataMember]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter the Name")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }
        [DataMember]
        [Display(Name = "Code")]
        [Required(ErrorMessage = "You must enter the Code")]
        [MaxLength(50, ErrorMessage = "Code cannot be longer than 50 characters")]
        public string Code { get; set; }

        public List<QuestionPossibleAnswerDTO> PossibleAnswers { get; set; }

    }
}
