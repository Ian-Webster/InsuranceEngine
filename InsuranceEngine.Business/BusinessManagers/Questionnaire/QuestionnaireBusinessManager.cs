using InsuranceEngine.Data.DataManagers.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace InsuranceEngine.Business.BusinessManagers.Questionnaire
{
    public class QuestionnaireBusinessManager : BusinessManagerBase<QuestionnaireBusinessManager>
    {

        #region quotes

        public List<QuoteListItemDTO> ListQuotesForDisplay()
        {
            return QuestionnaireDataManager.Instance.ListQuotesForDisplay();
        }

        public QuoteDTO LoadQuote(int quoteId)
        {
            return QuestionnaireDataManager.Instance.LoadQuote(quoteId);
        }

        public QuoteDTO SaveQuote(QuoteDTO dto)
        {
            return QuestionnaireDataManager.Instance.SaveQuote(dto);
        }

        #endregion

        #region questionnaire

        #region display

        public List<QuoteQuestionAnswerDTO> ListAnswersByQuote(int quoteId)
        {
            return QuestionnaireDataManager.Instance.ListAnswersByQuote(quoteId);
        }
        
        public List<QuoteQuestionAnswerDTO> SaveAnswers(List<QuoteQuestionAnswerDTO> dtos)
        {
            return QuestionnaireDataManager.Instance.SaveAnswers(dtos);
        }

        public void DeleteAnswers(List<QuoteQuestionAnswerDTO> dtos)
        {
            QuestionnaireDataManager.Instance.DeleteAnswers(dtos);
        }

        public List<RenderedPageForDisplayDTO> ListRenderedPagesForDisplayByScheme(int schemeId)
        {
            return QuestionnaireDataManager.Instance.ListRenderedPagesForDisplayByScheme(schemeId, 
                                                                                         WebConfigurationManager.AppSettings["Urls.DynamicViewsFolder"], 
                                                                                         WebConfigurationManager.AppSettings["Urls.QuestionControlFolder"]);
        }

        #endregion

        #region management

        #region question

        public List<QuestionDTO> ListQuestionsForScheme(int schemeId)
        {
            return QuestionnaireDataManager.Instance.ListQuestionsForScheme(schemeId);
        }

        public QuestionDTO LoadQuestion(string code)
        {
            return QuestionnaireDataManager.Instance.LoadQuestion(code);
        }

        #endregion

        #region possible answers

        public List<QuestionPossibleAnswerDTO> ListPossibleAnswersForQuestion(int questionId)
        {
            return QuestionnaireDataManager.Instance.ListPossibleAnswersForQuestion(questionId);
        }



        #endregion

        #region page templates

        public PageTemplateDTO LoadPageTemplate(int pageTemplateId)
        {
            return QuestionnaireDataManager.Instance.LoadPageTemplate(pageTemplateId);
        }

        public List<PageTemplateDTO> ListPageTemplates()
        {
            return QuestionnaireDataManager.Instance.ListPageTemplates();
        }

        public PageTemplateDTO SavePageTemplate(PageTemplateDTO dto)
        {
            return QuestionnaireDataManager.Instance.SavePageTemplate(dto);
        }

        public PageTemplateDTO NewPageTemplate()
        {
            return new PageTemplateDTO();
        }

        #endregion

        #region rendered pages

        public List<RenderedPageDTO> ListRenderedPagesByScheme(int schemeId)
        {
            return QuestionnaireDataManager.Instance.ListRenderedPagesByScheme(schemeId);
        }

        public List<RenderedPageForRenderingDTO> ListPagesForRenderingByScheme(int schemeId)
        {
            return QuestionnaireDataManager.Instance.ListPagesForRenderingByScheme(schemeId);
        }

        public void SaveRenderedPage(RenderedPageForRenderingDTO dto)
        {
            RenderedPageDTO pageDTO = new RenderedPageDTO()
            {
                LastRenderDate = DateTime.Now,
                PageContent = dto.PageTemplateContent,
                PageID = dto.PageID,
            };
            SaveRenderedPage(pageDTO);
        }
        public RenderedPageDTO SaveRenderedPage(RenderedPageDTO dto)
        {
            return QuestionnaireDataManager.Instance.SaveRenderedPage(dto);
        }

        #endregion

        #region question templates

        public QuestionTemplateDTO SaveQuestionTemplate(QuestionTemplateDTO dto)
        {
            return QuestionnaireDataManager.Instance.SaveQuestionTemplate(dto);
        }

        public List<QuestionTemplateDTO> ListQuestionTemplates()
        {
            return QuestionnaireDataManager.Instance.ListQuestionTemplates();
        }

        public QuestionTemplateDTO LoadQuestionTemplate(int questionTemplateId)
        {
            return QuestionnaireDataManager.Instance.LoadQuestionTemplate(questionTemplateId);
        }

        public QuestionTemplateDTO NewQuestionTemplate()
        {
            return new QuestionTemplateDTO() { LastRenderDate = null };
        }

        #endregion

        #region question types

        public List<QuestionTypeDTO> ListQuestionTypes()
        {
            return QuestionnaireDataManager.Instance.ListQuestionTypes();
        }

        #endregion

        #region page questions

        public List<PageQuestionDTO> ListPageQuestionsByPage(int pageId)
        {
            return QuestionnaireDataManager.Instance.ListPageQuestionsByPage(pageId);
        }


        public List<PageQuestionDTO> ListPageQuestionsByPageWithPossibleAnswers(int pageId, int targetQuestionId)
        {
            return QuestionnaireDataManager.Instance.ListPageQuestionsByPageWithPossibleAnswers(pageId, targetQuestionId);
        }

        #endregion

        #region page question validation

        public List<PageQuestionValidationDTO> ListValidationByPageQuestion(int pageQuestionId)
        {
            return QuestionnaireDataManager.Instance.ListValidationByPageQuestion(pageQuestionId);
        }

        #endregion

        #region page question conditional display

        public List<PageQuestionConditionalDisplayDTO> ListPageQuestionConditionalDisplaysByPage(int pageId)
        {
            return QuestionnaireDataManager.Instance.ListPageQuestionConditionalDisplaysByPage(pageId);
        }

        public List<PageQuestionConditionalDisplayDTO> ListPageQuestionConditionalDisplaysByTargetPageQuestion(int targetPageQuestionId)
        {
            return QuestionnaireDataManager.Instance.ListPageQuestionConditionalDisplaysByTargetPageQuestion(targetPageQuestionId);
        }

        public PageQuestionConditionalDisplayDTO NewPageQuestionConditionalDisplay(int targetPageQuestionId)
        {
            return new PageQuestionConditionalDisplayDTO() { TargetPageQuestionID = targetPageQuestionId };
        }

        #endregion

        #endregion

        #endregion

    }
}
