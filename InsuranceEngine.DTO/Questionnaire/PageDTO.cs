using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class PageDTO
    {
        [DataMember]
        public int PageID { get; set; }
        [DataMember]
        public int PageTemplateID { get; set; }
        [DataMember]
        [Display(Name = "Scheme")]
        [Required(ErrorMessage = "You must select a Scheme")]
        public int SchemeID { get; set; }
        [DataMember]
        [Display(Name = "Title")]
        [Required(ErrorMessage = "You must enter the Title")]
        [MaxLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string Title { get; set; }
        [DataMember]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter the Name")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [DataMember]
        [Display(Name = "Display Order")]
        [Required(ErrorMessage = "You must enter the Display Order")]
        [Range(1, int.MaxValue, ErrorMessage="Display Order must be greater than zero")]
        public int DisplayOrder { get; set; }
        [DataMember]
        [Display(Name = "Next Page")]
        public int? NextPageID { get; set; }

        public List<PageQuestionDTO> PageQuestions { get; set; }

    }
}
