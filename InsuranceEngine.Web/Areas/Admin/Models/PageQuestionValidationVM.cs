using InsuranceEngine.DTO.Questionnaire.Admin;
using System.Collections.Generic;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class PageQuestionValidationVM
    {
        public int SchemeID { get; set; }

        public int PageID { get; set; }

        public int PageQuestionID { get; set; }

        public IEnumerable<PageQuestionValidationForGridDTO> Validations { get; set; }

    }
}