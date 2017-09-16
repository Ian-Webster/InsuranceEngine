using InsuranceEngine.DTO.Questionnaire;
using System.Text.RegularExpressions;

namespace InsuranceEngine.Business.Questionnaire.Validators
{
    public class RegExValidator : ValidatorBase<RegExValidator>, IValidator
    {
        public bool ValidationPassed(PageQuestionValidationDTO validationDTO, PageQuestionForDisplayDTO questionDTO, QuoteQuestionAnswerDTO answerDTO)
        {
            bool result = true;
            if (answerDTO != null && !string.IsNullOrWhiteSpace(answerDTO.Answer))
            {
                var match = Regex.Match(answerDTO.Answer, validationDTO.ValidationExpression);
                if (!match.Success)
                {
                    result = false;
                } 
            }           
            return result;
        }
    }
}
