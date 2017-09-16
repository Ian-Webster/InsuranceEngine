using InsuranceEngine.DTO.Questionnaire;
using System;

namespace InsuranceEngine.Business.Questionnaire.Validators
{
    public class FixedOffsetFromTodayValidator : ValidatorBase<FixedOffsetFromTodayValidator>
    {
        public bool ValidationPassed(PageQuestionValidationDTO validationDTO, PageQuestionForDisplayDTO questionDTO, QuoteQuestionAnswerDTO answerDTO, bool isMax, bool offSetInYears)
        {
            bool result = true;
            if (answerDTO != null && !string.IsNullOrWhiteSpace(answerDTO.Answer))
            {
                DateTime givenDate = DateTime.MinValue;
                int offset = 0;
                if (DateTime.TryParse(answerDTO.Answer, out givenDate))
                {
                    //add offset to current date
                    offset = Convert.ToInt16(validationDTO.ValidationExpression);
                    DateTime currentDate = DateTime.Now;
                    if (offSetInYears)
                    {
                        currentDate = currentDate.AddYears(offset);
                    }
                    else
                    {
                        currentDate = currentDate.AddDays(offset);
                    }   
                       
                    //figure out if we need to check into the future or the past
                    if (offset > 0)
                    {
                        //check in to the future
                        if (isMax)
                        {
                            //maximum date                            
                            //date must be before or on the target date
                            if (DateTime.Compare(givenDate.Date, currentDate.Date) > 0)
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            //minimum date
                            //date must be after or on the target date
                            if (DateTime.Compare(givenDate.Date, currentDate.Date) < 0)
                            {
                                result = false;
                            }
                        }

                    }
                    else
                    {
                        //check in to the past
                        if (isMax)
                        {
                            //maximum date
                            //date must be after or on the target date
                            if (DateTime.Compare(givenDate.Date, currentDate.Date) < 0)
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            //minimum date
                            //date must be before or on the target date
                            if (DateTime.Compare(givenDate.Date, currentDate.Date) > 0)
                            {
                                result = false;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
