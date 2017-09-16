using InsuranceEngine.DTO.Questionnaire;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceEngine.Web.Models.Questionnaire
{
    public class QuestionVM
    {

        public PageQuestionForDisplayDTO Question { get; set; }

        public List<QuoteQuestionAnswerDTO> Answers { get; set; }

        public PageQuestionVisibilityDTO Visibility { get; set; }

        public string GetAnswerText(int questionId)
        {
            if (Answers.Any(a => a.QuestionID == questionId))
            {
                return Answers.Where(a => a.QuestionID == questionId).First().Answer;
            }
            else
            {
                return string.Empty;
            }
        }

        public int? GetPossibleAnswerID(int questionId)
        {
            if (Answers.Any(a => a.QuestionID == questionId))
            {
                return Answers.Where(a => a.QuestionID == questionId).First().QuestionPossibleAnswerID;
            }
            else
            {
                return null;
            }
        }

    }
}