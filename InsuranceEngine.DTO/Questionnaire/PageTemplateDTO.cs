using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class PageTemplateDTO
    {
        [DataMember]
        public int PageTemplateID { get; set; }
        [DataMember]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter the Name")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }
        [DataMember]
        [AllowHtml]
        [Display(Name = "Template Content")]
        [Required(ErrorMessage = "You must enter the Template Content")]
        public string TemplateContent { get; set; }

    }
}
