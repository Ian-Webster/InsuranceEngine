using InsuranceEngine.DTO.Questionnaire.Admin;
using System.Collections.Generic;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class QuestionPossibleAnswersVM
    {

        public int SchemeID { get; set; }
        public int QuestionID { get; set; }
        public IEnumerable<PossibleAnswerForGridDTO> PossibleAnswers { get; set; }

    }
}