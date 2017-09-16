using InsuranceEngine.DTO.Questionnaire;

namespace InsuranceEngine.Business.Questionnaire.Validators
{
    public interface IValidator
    {

        bool ValidationPassed(PageQuestionValidationDTO validationDTO, PageQuestionForDisplayDTO questionDTO, QuoteQuestionAnswerDTO answerDTO);

    }
}
