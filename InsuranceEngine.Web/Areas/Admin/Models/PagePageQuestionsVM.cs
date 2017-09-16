using InsuranceEngine.DTO.Questionnaire.Admin;
using System.Collections.Generic;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class PagePageQuestionsVM
    {

        public int PageID { get; set; }
        public int SchemeID { get; set; }
        public IEnumerable<PageQuestionForGridDTO> PageQuestions { get; set; }

    }
}