using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class QuestionTemplateDTO
    {
        [DataMember]
        public int QuestionTemplateID { get; set; }
        [DataMember]
        [Required(ErrorMessage="You must select a Question Type")]
        [Range(1, int.MaxValue, ErrorMessage="You must select a Question Type")]
        public int QuestionTypeID { get; set; }
        [DataMember]
        [RegularExpression("^[a-zA-Z0-9_-]+$", ErrorMessage = "Name can only contain a-z, 0-9, - or _ characters")]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter the Name")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [DataMember]
        [AllowHtml]
        [Display(Name = "Template")]
        [Required(ErrorMessage = "You must enter the Template")]
        public string Template { get; set; }
        [DataMember]
        [Display(Name = "Last Render Date")]
        public DateTime? LastRenderDate { get; set; }

    }
}
