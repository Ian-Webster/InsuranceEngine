using InsuranceEngine.DTO.Questionnaire;
using System.Collections.Generic;

namespace InsuranceEngine.Web.Models.Questionnaire
{
    public class PageVM
    {

        public RenderedPageForDisplayDTO Page { get; set; }

        public List<QuestionVM> Questions { get; set; }

    }
}