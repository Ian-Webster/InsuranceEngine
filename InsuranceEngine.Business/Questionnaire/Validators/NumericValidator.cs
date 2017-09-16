using InsuranceEngine.DTO.Questionnaire;
using System;
using System.Globalization;

namespace InsuranceEngine.Business.Questionnaire.Validators
{
    public class NumericValidator : ValidatorBase<NumericValidator>, IValidator
    {
        public bool ValidationPassed(PageQuestionValidationDTO validationDTO, PageQuestionForDisplayDTO questionDTO, QuoteQuestionAnswerDTO answerDTO)
        {
            bool result = true;
            if (answerDTO != null && !string.IsNullOrWhiteSpace(answerDTO.Answer))
            {
                Double temp;
                result = Double.TryParse(answerDTO.Answer, NumberStyles.Any, CultureInfo.InvariantCulture, out temp);
            }
            return result;
        }
    }
}
