using InsuranceEngine.DTO.Questionnaire;
using System;

namespace InsuranceEngine.Business.Questionnaire.Validators
{
    public class DateBeforeTodayValidator : ValidatorBase<DateBeforeTodayValidator>, IValidator
    {
        public bool ValidationPassed(PageQuestionValidationDTO validationDTO, PageQuestionForDisplayDTO questionDTO, QuoteQuestionAnswerDTO answerDTO)
        {
            bool result = true;
            if (answerDTO != null && !string.IsNullOrWhiteSpace(answerDTO.Answer))
            {
                DateTime givenDate = DateTime.MinValue;
                if (DateTime.TryParse(answerDTO.Answer, out givenDate))
                {

                    if (givenDate.Date >= DateTime.Now.Date)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
