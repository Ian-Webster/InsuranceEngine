using System.ComponentModel.DataAnnotations;

namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    public class PageQuestionForAdminDisplayDTO
    {

        public int PageQuestionID { get; set; }
        [Display(Name = "Question Name")]
        public string Question { get; set; }
        [Display(Name = "Question Display Text")]
        public string QuestionText { get; set; }
        [Display(Name = "Question Name")]
        public string QuestionName { get; set; }
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
        [Display(Name = "Default Show")]
        public bool DefaultShow { get; set; }

    }
}
