using InsuranceEngine.DTO.Questionnaire;
using System;
using System.Linq;

namespace InsuranceEngine.Business.Questionnaire.Validators
{
    public class RequiredValidator : ValidatorBase<RequiredValidator>, IValidator
    {
        public bool ValidationPassed(PageQuestionValidationDTO validationDTO, PageQuestionForDisplayDTO questionDTO, QuoteQuestionAnswerDTO answerDTO)
        {
            bool result = false;            

            if (answerDTO != null)
            {
                //check if the question has possible answers
                if (questionDTO.HasPossibleAnswers)
                {
                    //check if the answer given is a valid possible for the question
                    if (questionDTO.PossibleAnswers.Any(pa => pa.QuestionPossibleAnswerID == answerDTO.QuestionPossibleAnswerID.GetValueOrDefault(0)))
                    {
                        //valid answer given validation has passed
                        result = true;
                    }
                }
                else if (!String.IsNullOrWhiteSpace(answerDTO.Answer))
                {
                    //answer given
                    result = true;
                }
            }


            return result;
        }
    }
}
