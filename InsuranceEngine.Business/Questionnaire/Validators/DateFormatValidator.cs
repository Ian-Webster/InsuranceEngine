using InsuranceEngine.DTO.Questionnaire;
using System.Text.RegularExpressions;

namespace InsuranceEngine.Business.Questionnaire.Validators
{
    public class DateFormatValidator : ValidatorBase<DateFormatValidator>, IValidator
    {
        public bool ValidationPassed(PageQuestionValidationDTO validationDTO, PageQuestionForDisplayDTO questionDTO, QuoteQuestionAnswerDTO answerDTO)
        {
            bool result = true;
            if (answerDTO != null && !string.IsNullOrWhiteSpace(answerDTO.Answer))
            {
                //using a fixed regex pattern for date format (uk), found here http://stackoverflow.com/questions/3767614/does-anyone-know-of-a-reg-expression-for-uk-date-format
                var match = Regex.Match(answerDTO.Answer, @"^(?:(?:(?:(?:31\/(?:0?[13578]|1[02]))|(?:(?:29|30)\/(?:0?[13-9]|1[0-2])))\/(?:1[6-9]|[2-9]\d)\d{2})|(?:29\/0?2\/(?:(?:(1[6-9]|[2-9]\d)(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:0?[1-9]|1\d|2[0-8])\/(?:(?:0?[1-9])|(?:1[0-2]))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$");
                if (!match.Success)
                {
                    result = false;
                } 
            }           
            return result;
        }
    }
}
