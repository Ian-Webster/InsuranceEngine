using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    [Serializable]
    public class SchemeDTO
    {
        [DataMember]
        public int SchemeID { get; set; }
        [DataMember]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter the Name")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [DataMember]
        [Display(Name = "Code")]
        [Required(ErrorMessage = "You must enter the Code")]
        [MaxLength(10, ErrorMessage = "Code cannot be longer than 10 characters")]
        public string Code { get; set; }

        public List<PageDTO> Pages { get; set; }

        public List<RenderedPageDTO> RenderedPages { get; set; }

    }
}
