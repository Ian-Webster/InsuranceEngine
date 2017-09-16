using InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class PageQuestionValidationDTO
    {
        [DataMember]
        public int PageQuestionValidationID { get; set; }
        [DataMember]
        [Display(Name = "Page Question")]
        [Required(ErrorMessage = "You must select a Page Question")]
        [Range(1, int.MaxValue, ErrorMessage="You must select a Page Question")]
        public int PageQuestionID { get; set; }
        [DataMember]
        [Display(Name = "Validation")]
        [Required(ErrorMessage = "You must select a Validation Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Validation Type")]
        public ValidationTypes ValidationType { get; set; }
        [DataMember]
        [Display(Name = "Error Message")]
        [Required(ErrorMessage = "You must enter the Error Message")]
        [MaxLength(1024, ErrorMessage = "Error Message cannot be longer than 1024 characters")]
        [DataType(DataType.MultilineText)]
        public string ErrorMessage { get; set; }
        [DataMember]
        [Display(Name = "Validation Expression")]
        [DataType(DataType.MultilineText)]
        public string ValidationExpression { get; set; }

    }
}
