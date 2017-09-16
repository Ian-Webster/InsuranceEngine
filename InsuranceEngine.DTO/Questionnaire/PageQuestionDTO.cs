using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class PageQuestionDTO
    {
        [DataMember]
        public int PageQuestionID { get; set; }
        [DataMember]
        public int PageID { get; set; }
        [DataMember]
        [Display(Name = "Question")]
        [Required(ErrorMessage = "You must select the Question")]
        [Range(1, int.MaxValue, ErrorMessage="You must select the Question")]
        public int QuestionID { get; set; }
        [DataMember]
        [Display(Name = "Display Order")]
        [Required(ErrorMessage = "You must enter the Display Order")]
        public int DisplayOrder { get; set; }
        [DataMember]
        [Display(Name = "Is Required")]
        [Required(ErrorMessage = "You must select an answer for Is Required")]
        public bool IsRequired { get; set; }
        [DataMember]
        [Display(Name = "Question Text")]
        [Required(ErrorMessage = "You must enter the Question Text")]
        [MaxLength(250, ErrorMessage = "Question Text cannot be longer than 250 characters")]
        public string QuestionText { get; set; }
        [DataMember]
        [Display(Name = "Question Name")]
        [Required(ErrorMessage = "You must enter the Question Name")]
        [MaxLength(50, ErrorMessage = "Question Text cannot be longer than 50 characters")]
        public string QuestionName { get; set; }
        [DataMember]
        [Display(Name = "Show Question By Default")]
        public bool DefaultShow { get; set; }
    }
}
