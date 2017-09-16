using InsuranceEngine.Business.Questionnaire.Validators;
using InsuranceEngine.DTO.Questionnaire;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceEngine.Business.Questionnaire
{
    public class Validation : QuestionnaireBase<Validation>
    {

        public bool ValidationPassed(List<PageQuestionForDisplayDTO> questionDTOs, List<QuoteQuestionAnswerDTO> answerDTOs, List<PageQuestionVisibilityDTO> questionVisbilityDTOs, out List<ValidationErrorDTO> validationErrorDTOs)
        {
            bool result = true;
            validationErrorDTOs = new List<ValidationErrorDTO>();
            QuoteQuestionAnswerDTO answerDTO = null;
            //itterate the questions with validation
            foreach (PageQuestionForDisplayDTO questionDTO in questionDTOs.Where(q => q.Validators != null && q.Validators.Count > 0))
            {
                //check if the question is visible
                if (questionVisbilityDTOs.Any(v => v.PageQuestionID == questionDTO.PageQuestionID && v.IsVisible))
                {
                    //question is visible - need to check validators for this question

                    //itterate the validators for this question
                    foreach (PageQuestionValidationDTO validatorDTO in questionDTO.Validators)
                    {

                        answerDTO = answerDTOs.Where(a => a.QuestionID == questionDTO.QuestionID).FirstOrDefault();
                        switch (validatorDTO.ValidationType)
                        {
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Required:
                                if (!RequiredValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Regex:
                                if (!RegExValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Date_Valid_Format:
                                if (!DateFormatValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Date_After_Today:
                                if (!DateAfterTodayValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Date_Before_Today:
                                if (!DateBeforeTodayValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Date_Max_Fixed_Number_of_Days:
                                if (!FixedOffsetFromTodayValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO, true, false))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Date_Max_Fixed_Number_of_Years:
                                if (!FixedOffsetFromTodayValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO, true, true))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Date_Min_Fixed_Number_of_Days:
                                if (!FixedOffsetFromTodayValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO, false, false))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Date_Min_Fixed_Number_of_Years:
                                if (!FixedOffsetFromTodayValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO, false, true))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            case InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums.ValidationTypes.Numeric:
                                if (!NumericValidator.Instance.ValidationPassed(validatorDTO, questionDTO, answerDTO))
                                {
                                    result = false;
                                    validationErrorDTOs.Add(new ValidationErrorDTO() { ErrorMessage = validatorDTO.ErrorMessage, PageQuestionID = questionDTO.PageQuestionID });
                                }
                                break;
                            default:
                                throw new Exception("Unknow validator type " + Convert.ToInt16(validatorDTO.ValidationType).ToString());
                        }
                    }
                }
            }

            return result;
        }

    }
}
