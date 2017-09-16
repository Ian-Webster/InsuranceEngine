using InsuranceEngine.DTO.Questionnaire;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceEngine.Business.Questionnaire
{
    public class ConditionalDisplay : QuestionnaireBase<ConditionalDisplay>
    {

        public List<PageQuestionVisibilityDTO> GetPageQuestionVisbilitiesForPage(RenderedPageForDisplayDTO pageDTO, List<QuoteQuestionAnswerDTO> answerDTOs)
        {
            List<PageQuestionVisibilityDTO> result = new List<PageQuestionVisibilityDTO>();
            foreach (PageQuestionForDisplayDTO pageQuestionDTO in pageDTO.Questions)
            {
                result.Add(SetPageQuestionVisbility(pageQuestionDTO, pageDTO, answerDTOs));
            }
            return result;
        }

        public PageQuestionVisibilityDTO SetPageQuestionVisbility(PageQuestionForDisplayDTO pageQuestionDTO, RenderedPageForDisplayDTO pageDTO, List<QuoteQuestionAnswerDTO> answerDTOs)
        {
            PageQuestionVisibilityDTO result = new PageQuestionVisibilityDTO() { PageQuestionID = pageQuestionDTO.PageQuestionID };
            if (pageDTO.Questions.SelectMany(q => q.DependantQuestions).Any(dq => dq.TargetPageQuestionID == pageQuestionDTO.PageQuestionID))
            {
                //this question is dependant on another question for it's visbility
                
                //get a list of questions this question is dependant on for it's visbility
                List<PageQuestionForDisplayDTO> sourcePageQuestionDTOs = pageDTO.Questions.Where(q => q.HasPossibleAnswers
                                                                                                 && q.DependantQuestions != null 
                                                                                                 && q.DependantQuestions.Count > 0 
                                                                                                 && q.DependantQuestions.Any(dq => dq.TargetPageQuestionID == pageQuestionDTO.PageQuestionID)).ToList();
                //check if we have any answers for the question
                if (answerDTOs.Any(a => sourcePageQuestionDTOs.Select(s => s.QuestionID).Contains(a.QuestionID)))
                {
                    //we have an answer/s

                    //get the display conditions that apply to this question
                    List<PageQuestionConditionalDisplayForDisplayDTO> displayConditionDTOs = sourcePageQuestionDTOs.SelectMany(s => s.PossibleAnswers)
                                                                                                                   .Where(pa => answerDTOs.Select(a => a.QuestionPossibleAnswerID.GetValueOrDefault(0)).Contains(pa.QuestionPossibleAnswerID))
                                                                                                                   .SelectMany(a => a.DisplayConditions)
                                                                                                                   .Where(d => d.TargetPageQuestionID == pageQuestionDTO.PageQuestionID)
                                                                                                                   .ToList();
                    //are there any conditions that make this visible
                    if (displayConditionDTOs != null && displayConditionDTOs.Count > 0)
                    {
                        if (displayConditionDTOs.Any(d => !d.Hide))
                        {
                            //we've found a condition that makes the question visible
                            result.IsVisible = true;
                        }
                        else 
                        {
                            //no conditions that make the question visible - so we need to hide the question
                            result.IsVisible = false;
                        }
                    }
                    else
                    {
                        //no conditions with answers - use default show condition
                        result.IsVisible = pageQuestionDTO.DefaultShow;
                    }
                }
                else
                {
                    //set the default visbility
                    result.IsVisible = pageQuestionDTO.DefaultShow;
                }                
            }
            else
            {
                //not dependant on another questions for visibility - must be visible
                result.IsVisible = true;
            }
            return result;
        }

    }
}
