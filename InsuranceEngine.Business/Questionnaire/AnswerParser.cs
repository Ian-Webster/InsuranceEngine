using InsuranceEngine.DTO.Questionnaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InsuranceEngine.Business.Questionnaire
{
    public class AnswerParser : QuestionnaireBase<AnswerParser>
    {

        /// <summary>
        /// Combines given answers with the existing set of answers.
        /// </summary>
        /// <param name="quoteId">id of the quote the answers belong to</param>
        /// <param name="formAnswers">List of AnswerDTO parsed from the form</param>
        /// <param name="existingAnswers">Answers given already for the quote</param>
        /// <param name="questions">list of expected questions (this will be for a particular page)</param>
        /// <returns>update list of QuoteQuestionAnswerDTO objects</returns>
        public List<QuoteQuestionAnswerDTO> ParseAnswers(int quoteId, List<AnswerDTO> formAnswers, List<QuoteQuestionAnswerDTO> existingAnswers, List<PageQuestionForDisplayDTO> questions)
        {
            string textAnswer = string.Empty;
            int? possibleAnswerId = null;
            AnswerDTO formAnswer;
            QuoteQuestionAnswerDTO quoteAnswer;
            //itterate through the list of page questions
            foreach (PageQuestionForDisplayDTO question in questions)
            {
                //have we got an answer for this question
                if (formAnswers.Any(a => a.PageQuestionID == question.PageQuestionID))
                {
                    //we have an answer for the question
                    formAnswer = formAnswers.Where(a => a.PageQuestionID == question.PageQuestionID).First();

                    textAnswer = string.Empty;
                    possibleAnswerId = null;

                    //need to figure out whether we have a "text" (e.g. from a textbox) answer or a possible answer (e.g. from a drop down list)
                    if (question.PossibleAnswers != null && question.PossibleAnswers.Count > 0)
                    {
                        //must be a possible answer
                        //check that the answer given matches one in the list
                        if (question.PossibleAnswers.Any(pa => pa.AnswerValue == formAnswer.AnswerText && pa.QuestionID == question.QuestionID))
                        {
                            //got a match
                            possibleAnswerId = question.PossibleAnswers.Where(pa => pa.AnswerValue == formAnswer.AnswerText && pa.QuestionID == question.QuestionID).First().QuestionPossibleAnswerID;
                        }
                        else
                        {
                            //no match, someones selected an invalid answer!
                            throw new Exception("invalid answer selected for question id" + question.QuestionID + " answer given is " + formAnswer.AnswerText);
                        }
                    }
                    else
                    {
                        //must be a text answer
                        textAnswer = formAnswer.AnswerText;
                    }

                    //is the answer already in the list of exsiting answers?
                    if (existingAnswers.Any(a => a.QuestionID == question.QuestionID))
                    {
                        //yes - we need to update the answer
                        quoteAnswer = existingAnswers.Where(a => a.QuestionID == question.QuestionID).First();
                        quoteAnswer.Answer = textAnswer;
                        quoteAnswer.QuestionPossibleAnswerID = possibleAnswerId;
                    }
                    else
                    {
                        //no - we need to insert a new answer
                        quoteAnswer = new QuoteQuestionAnswerDTO()
                        {
                            Answer = textAnswer,
                            QuestionID = question.QuestionID,
                            QuestionPossibleAnswerID = possibleAnswerId,
                            QuoteID = quoteId
                        };
                        existingAnswers.Add(quoteAnswer);
                    }
                }                
            }

            return existingAnswers;
        }

        /// <summary>
        /// Checks the given answers to find any that should be removed.
        /// An answer should be remove if it is 1. an answer to a hidden question or 2. if it's answer (either possible answer or free text answer) is blank
        /// </summary>
        /// <param name="answers"></param>
        /// <param name="questions"></param>
        /// <param name="questionVisbilityDTOs"></param>
        /// <returns></returns>
        public List<QuoteQuestionAnswerDTO> FindRemovedAnswers(List<QuoteQuestionAnswerDTO> answers, List<PageQuestionForDisplayDTO> questions, List<PageQuestionVisibilityDTO> questionVisbilityDTOs)
        {
            List<QuoteQuestionAnswerDTO> removedAnswers = new List<QuoteQuestionAnswerDTO>();
            //itterate through the list of page questions
            foreach (PageQuestionForDisplayDTO question in questions)
            {
                //do we have an answer for this question?
                if (answers.Any(a => a.QuestionID == question.QuestionID))
                {
                    //is the question visible?
                    if (!questionVisbilityDTOs.Any(v => v.PageQuestionID == question.PageQuestionID && v.IsVisible))
                    {
                        //question isn't visible, remove it from the existing answers
                        removedAnswers.Add(answers.Where(a => a.QuestionID == question.QuestionID).First());
                    }
                    else 
                    {
                        //question is visible, check if we have an answer for it
                        if (answers.Any(a => a.QuestionID == question.QuestionID && (a.QuestionPossibleAnswerID.GetValueOrDefault(0) == 0 && string.IsNullOrWhiteSpace(a.Answer))))
                        {
                            //we've got an answer but it's blank - add it to the removed list
                            removedAnswers.Add(answers.Where(a => a.QuestionID == question.QuestionID).First());
                        }
                    }
                }

            }

            return removedAnswers;
        }

        /// <summary>
        /// Takes a form collection and parses all elements starting with "Question_"
        /// </summary>
        /// <param name="form"></param>
        /// <returns>List of AnswerDTO objects</returns>
        public List<AnswerDTO> GetAnswersFromForm(FormCollection form)
        {
            List<AnswerDTO> result = new List<AnswerDTO>();           
            foreach (string key in form.AllKeys.Where(k => k.StartsWith("Question_")))
            {
                if (!string.IsNullOrWhiteSpace(form[key]))
                {
                    result.Add(new AnswerDTO() { AnswerText = form[key], PageQuestionID = int.Parse(key.Replace("Question_", string.Empty)) });
                }                
            }
            return result;
        }

    }
}
