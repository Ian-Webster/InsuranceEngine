using InsuranceEngine.Data.EF.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceEngine.Data.DataManagers.Questionnaire
{
    public class QuestionnaireDataManager : DataManagerBase<QuestionnaireDataManager>
    {

        #region quotes

        public List<QuoteListItemDTO> ListQuotesForDisplay()
        {
            List<QuoteListItemDTO> result = QuestionnaireCacheManager.Instance.ListQuotesForDisplayFromCache();
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    var dao = context.Quotes.OrderByDescending(q => q.QuoteDate);

                    if (dao != null)
                    {
                        result = AutoMapper.Mapper.Map<IEnumerable<Quote>, List<QuoteListItemDTO>>(dao);
                        QuestionnaireCacheManager.Instance.InsertListQuotesForDisplayIntoCache(result);
                    }
                }
            }

            return result;
        }

        public QuoteDTO LoadQuote(int quoteId)
        {
            QuoteDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Quotes.Find(quoteId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Quote, QuoteDTO>(dao);
                    result.Answers = ListAnswersByQuote(result.QuoteID);
                }
            }
            return result;
        }

        public QuoteDTO SaveQuote(QuoteDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Quote dao = null;
                if (dto.QuoteID != 0)
                {
                    dao = context.Quotes.Where(q => q.Quote_ID == dto.QuoteID).FirstOrDefault();
                }
                else
                {
                    dao = new Quote();
                    context.Quotes.Add(dao);
                }

                dao.Scheme_ID = dto.SchemeID;
                dao.Reference = dto.Reference;
                dao.QuoteDate = dto.QuoteDate;

                context.SaveChanges();
                QuestionnaireCacheManager.Instance.ClearListQuotesForDisplayFromCache();
                dto.QuoteID = dao.Quote_ID;
            }
            return dto;
        }

        #endregion

        #region questionnaire

        #region display

        public List<RenderedPageForDisplayDTO> ListRenderedPagesForDisplayByScheme(int schemeId, string dynamicViewsRootFolder, string questionControlRootPath)
        {
            List<RenderedPageForDisplayDTO> result = QuestionnaireCacheManager.Instance.ListRenderedPagesForDisplayBySchemeFromCache(schemeId);
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    var dao = (from r in context.Rendered_Page
                               join p in context.Pages on r.Page_ID equals p.Page_ID
                               where p.Scheme_ID == schemeId
                               orderby p.DisplayOrder
                               select r);

                    if (dao != null)
                    {
                        result = AutoMapper.Mapper.Map<IEnumerable<Rendered_Page>, List<RenderedPageForDisplayDTO>>(dao);
                        foreach (RenderedPageForDisplayDTO pageDTO in result)
                        {
                            pageDTO.Questions = ListPageQuestionsForDisplayByPage(pageDTO.PageID, questionControlRootPath);
                            pageDTO.DynamicViewsRootFolder = dynamicViewsRootFolder;
                        }
                        QuestionnaireCacheManager.Instance.InsertRenderedPagesForDisplayBySchemeIntoCache(schemeId, result);
                    }
                }
            }

            return result;
        }

        public List<QuoteQuestionAnswerDTO> SaveAnswers(List<QuoteQuestionAnswerDTO> dtos)
        {
            using (var context = new QuestionnaireEntities())
            {
                Quote_Question_Answer dao = null;
                foreach (QuoteQuestionAnswerDTO dto in dtos)
                {
                    if (dto.QuoteQuestionAnswerID > 0)
                    {
                        //existing answer - load it
                        dao = context.Quote_Question_Answer.Find(dto.QuoteQuestionAnswerID);
                    }
                    else
                    {
                        //new answer
                        dao = new Quote_Question_Answer()
                        {
                            Quote_ID = dto.QuoteID,
                            Question_ID = dto.QuestionID
                        };
                        context.Quote_Question_Answer.Add(dao);
                    }

                    dao.Question_Possible_Answer_ID = dto.QuestionPossibleAnswerID;
                    dao.Answer = dto.Answer;
                    dto.QuoteQuestionAnswerID = dao.Quote_Question_Answer_ID;
                }
                context.SaveChanges();
                //re-load questions to get question id's
                dtos = ListAnswersByQuote(dtos[0].QuoteID);
            }
            return dtos;
        }

        public void DeleteAnswers(List<QuoteQuestionAnswerDTO> dtos)
        {
            using (var context = new QuestionnaireEntities())
            {
                foreach (QuoteQuestionAnswerDTO dto in dtos)
                {
                    var dao = context.Quote_Question_Answer.Find(dto.QuoteQuestionAnswerID);
                    if (dao != null)
                    {
                        context.Quote_Question_Answer.Remove(dao);
                    }
                }
                context.SaveChanges();
            }
        }

        public List<QuoteQuestionAnswerDTO> ListAnswersByQuote(int quoteId)
        {
            List<QuoteQuestionAnswerDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Quote_Question_Answer.Where(qa => qa.Quote_ID == quoteId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Quote_Question_Answer>, List<QuoteQuestionAnswerDTO>>(dao);
                }
            }
            return result;
        }

        private List<PageQuestionForDisplayDTO> ListPageQuestionsForDisplayByPage(int pageId, string questionControlRootPath)
        {
            List<PageQuestionForDisplayDTO> result = QuestionnaireCacheManager.Instance.ListPageQuestionsForDisplayByPageFromCache(pageId);
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    var dao = (from pq in context.Page_Question
                               where pq.Page_ID == pageId
                               orderby pq.DisplayOrder
                               select pq);

                    if (dao != null)
                    {
                        result = AutoMapper.Mapper.Map<IEnumerable<Page_Question>, List<PageQuestionForDisplayDTO>>(dao);
                        foreach (PageQuestionForDisplayDTO pageQuestionDTO in result)
                        {
                            //load possible answers
                            pageQuestionDTO.PossibleAnswers = ListPossibleAnswersForQuestionForDisplay(pageQuestionDTO.QuestionID, context);
                            //set question path
                            pageQuestionDTO.QuestionTemplatePath = questionControlRootPath + pageQuestionDTO.QuestionTemplatePath + ".cshtml";     
                            //set validators for the question
                            pageQuestionDTO.Validators = ListValidationByPageQuestion(pageQuestionDTO.PageQuestionID, context);
                            pageQuestionDTO.Validators.ForEach(v => v.ErrorMessage = v.ErrorMessage.Replace("$$QuestionName$$", pageQuestionDTO.QuestionName));
                            pageQuestionDTO.IsRequired = (pageQuestionDTO.Validators != null && pageQuestionDTO.Validators.Any(v => v.ValidationType == ValidationTypes.Required));
                            pageQuestionDTO.DependantQuestions = ListDependantPageQuestionsBySourcePageQuestion(pageQuestionDTO.PageQuestionID, context);
                            pageQuestionDTO.HasDisplayConditions = (pageQuestionDTO.DependantQuestions != null && pageQuestionDTO.DependantQuestions.Count > 0);
                        }
                        QuestionnaireCacheManager.Instance.InsertPageQuestionsForDisplayByPageIntoCache(pageId, result);
                    }
                }
            }
            return result;
        }

        private List<PageQuestionConditionalDisplayForDisplayDTO> ListDependantPageQuestionsBySourcePageQuestion(int pageQuestionId, QuestionnaireEntities context)
        {
            List<PageQuestionConditionalDisplayForDisplayDTO> result = null;
            var dao = (from pq in context.Page_Question
                       join c in context.Page_Question_Conditional_Display on pq.Page_Question_ID equals c.Target_Page_Question_ID
                       where c.Source_Page_Question_ID == pageQuestionId
                       select new
                       {
                           PageQuestionID = pq.Page_Question_ID,
                           Hide = !pq.DefaultShow
                       }).Distinct();
            if (dao != null)
            {
                result = new List<PageQuestionConditionalDisplayForDisplayDTO>();
                foreach (var item in dao)
                {
                    if (!result.Any(c => c.TargetPageQuestionID == item.PageQuestionID))
                    {
                        result.Add(new PageQuestionConditionalDisplayForDisplayDTO() { Hide = item.Hide, TargetPageQuestionID = item.PageQuestionID });
                    }                    
                }
            }
            return result;
        }

        #endregion

        #region management

        #region questions

        public List<QuestionDTO> ListQuestionsForScheme(int schemeId)
        {
            List<QuestionDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Questions.Where(q => q.Scheme_ID == schemeId).OrderBy(q => q.Name);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Question>, List<QuestionDTO>>(dao);
                }
            }
            return result;
        }

        public QuestionDTO LoadQuestion(string code)
        {
            QuestionDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Questions.Where(q => q.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Question, QuestionDTO>(dao);
                }
            }
            return result;
        }

        #endregion

        #region possible answers

        public List<QuestionPossibleAnswerDTO> ListPossibleAnswersForQuestion(int questionId)
        {
            List<QuestionPossibleAnswerDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                result = ListPossibleAnswersForQuestion(questionId, context);
            }
            return result;
        }
        private List<QuestionPossibleAnswerDTO> ListPossibleAnswersForQuestion(int questionId, QuestionnaireEntities context)
        {
            List<QuestionPossibleAnswerDTO> result = QuestionnaireCacheManager.Instance.ListPossibleAnswersByQuestionFromCache(questionId);
            if (result == null)
            {
                var dao = context.Question_Possible_Answer.Where(qpa => qpa.Question_ID == questionId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Question_Possible_Answer>, List<QuestionPossibleAnswerDTO>>(dao);
                    QuestionnaireCacheManager.Instance.InsertPossibleAnswersByQuestionIntoCache(questionId, result);
                }
            }

            return result;
        }

        private List<QuestionPossibleAnswerForDisplayDTO> ListPossibleAnswersForQuestionForDisplay(int questionId, QuestionnaireEntities context)
        {
            List<QuestionPossibleAnswerForDisplayDTO> result = QuestionnaireCacheManager.Instance.ListPossibleAnswersByQuestionForDisplayFromCache(questionId);
            if (result == null)
            {
                var dao = context.Question_Possible_Answer.Where(qpa => qpa.Question_ID == questionId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Question_Possible_Answer>, List<QuestionPossibleAnswerForDisplayDTO>>(dao);
                    QuestionnaireCacheManager.Instance.InsertPossibleAnswersByQuestionForDisplayIntoCache(questionId, result);
                    foreach(QuestionPossibleAnswerForDisplayDTO dto in result)
                    {
                        dto.DisplayConditions = ListPageQuestionDisplayConditionsForDisplayByPossibleAnswer(context, dto.QuestionPossibleAnswerID);
                    }
                    QuestionnaireCacheManager.Instance.InsertPossibleAnswersByQuestionForDisplayIntoCache(questionId, result);
                }
            }

            return result;
        }        

        #endregion

        #region page templates

        public List<PageTemplateDTO> ListPageTemplates()
        {
            List<PageTemplateDTO> result = QuestionnaireCacheManager.Instance.ListPageTemplatesFromCache();
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    var dao = context.Page_Template.OrderBy(pt => pt.Name);
                    if (dao != null)
                    {
                        result = AutoMapper.Mapper.Map<IEnumerable<Page_Template>, List<PageTemplateDTO>>(dao);
                        QuestionnaireCacheManager.Instance.InsertListPageTemplatesIntoCache(result);
                    }
                }
            }
            return result;
        }

        public PageTemplateDTO LoadPageTemplate(int pageTemplateId)
        {
            PageTemplateDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Page_Template.Find(pageTemplateId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Page_Template, PageTemplateDTO>(dao);
                }
            }
            return result;
        }

        public PageTemplateDTO SavePageTemplate(PageTemplateDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Page_Template dao = null;
                if (dto.PageTemplateID > 0)
                {
                    dao = context.Page_Template.Find(dto.PageTemplateID);
                }
                else
                {
                    dao = new Page_Template();
                    context.Page_Template.Add(dao);
                }

                dao.Name = dto.Name;
                dao.TemplateContent = dto.TemplateContent;

                context.SaveChanges();
            }
            return dto;
        }

        #endregion

        #region rendered pages

        public List<RenderedPageDTO> ListRenderedPagesByScheme(int schemeId)
        {
            List<RenderedPageDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = (from rp in context.Rendered_Page
                           join p in context.Pages on rp.Page_ID equals p.Page_ID
                           where p.Scheme_ID == schemeId
                           orderby p.DisplayOrder
                           select rp);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Rendered_Page>, List<RenderedPageDTO>>(dao);
                }
            }
            return result;
        }

        public List<RenderedPageForRenderingDTO> ListPagesForRenderingByScheme(int schemeId)
        {
            List<RenderedPageForRenderingDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Pages.Where(p => p.Scheme_ID == schemeId).OrderBy(p => p.DisplayOrder);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Page>, List<RenderedPageForRenderingDTO>>(dao);
                }
            }
            return result;
        }

        public RenderedPageDTO SaveRenderedPage(RenderedPageDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Rendered_Page dao = null;
                if (dto.RenderedPageID > 0)
                {
                    //existing rendered page
                    dao = context.Rendered_Page.Find(dto.RenderedPageID);
                }
                else if (dto.PageID > 0 && context.Rendered_Page.Any(rp => rp.Page_ID == dto.PageID))
                {
                    dao = context.Rendered_Page.Where(rp => rp.Page_ID == dto.PageID).FirstOrDefault();
                }
                else
                {
                    dao = context.Rendered_Page.Where(rp => rp.Page_ID == dto.PageID).FirstOrDefault();
                    if (dao == null)
                    {
                        dao = new Rendered_Page()
                        {
                            Page_ID = dto.PageID
                        };
                    }
                    context.Rendered_Page.Add(dao);
                }

                dao.PageContent = dto.PageContent;
                dao.LastRenderDate = dto.LastRenderDate;

                context.SaveChanges();

                int schemeId = (from s in context.Schemes
                                join p in context.Pages on s.Scheme_ID equals p.Scheme_ID
                                where p.Page_ID == dto.PageID
                                select s.Scheme_ID).First();
                QuestionnaireCacheManager.Instance.ClearPage(dto.PageID, schemeId);

                dto.RenderedPageID = dao.Rendered_Page_ID;

            }
            return dto;
        }

        #endregion

        #region question templates

        public QuestionTemplateDTO SaveQuestionTemplate(QuestionTemplateDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Question_Template dao = null;
                if (dto.QuestionTemplateID > 0)
                {
                    dao = context.Question_Template.Find(dto.QuestionTemplateID);
                }
                else
                {
                    dao = new Question_Template()
                    {
                        LastRenderDate = DateTime.Now
                    };
                    context.Question_Template.Add(dao);
                }

                dao.Name = dto.Name;
                dao.Template = dto.Template;
                dao.Question_Type_ID = dto.QuestionTypeID;
                dao.LastRenderDate = dto.LastRenderDate;

                context.SaveChanges();
                dto.QuestionTemplateID = dao.Question_Template_ID;
                QuestionnaireCacheManager.Instance.ClearQuestion(dto.QuestionTemplateID);
            }
            return dto;
        }

        public List<QuestionTemplateDTO> ListQuestionTemplates()
        {
            List<QuestionTemplateDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Question_Template.OrderBy(qt => qt.Name);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Question_Template>, List<QuestionTemplateDTO>>(dao);
                }
            }
            return result;
        }

        public QuestionTemplateDTO LoadQuestionTemplate(int questionTemplateId)
        {
            QuestionTemplateDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Question_Template.Find(questionTemplateId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Question_Template, QuestionTemplateDTO>(dao);
                }
            }
            return result;
        }

        #endregion

        #region question types

        public List<QuestionTypeDTO> ListQuestionTypes()
        {
            List<QuestionTypeDTO> result = QuestionnaireCacheManager.Instance.ListQuestionTypesFromCache();
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    var dao = context.Question_Type;
                    if (dao != null)
                    {
                        result = AutoMapper.Mapper.Map<IEnumerable<Question_Type>, List<QuestionTypeDTO>>(dao);
                        QuestionnaireCacheManager.Instance.InsertQuestionTypesIntoCache(result);
                    }
                }
            }
            return result;
        }

        #endregion

        #region page questions

        public List<PageQuestionDTO> ListPageQuestionsByPage(int pageId)
        {
            List<PageQuestionDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Page_Question.Where(pq => pq.Page_ID == pageId).OrderBy(pq => pq.DisplayOrder);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Page_Question>, List<PageQuestionDTO>>(dao);
                }
            }
            return result;
        }

        /// <summary>
        /// Used for selecting a "source" page question for use in a conditional display
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="targetQuestionId">This question will be excluded from the list</param>
        /// <returns></returns>
        public List<PageQuestionDTO> ListPageQuestionsByPageWithPossibleAnswers(int pageId, int targetQuestionId)
        {
            List<PageQuestionDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = (from pq in context.Page_Question
                           join q in context.Questions on pq.Question_ID equals q.Question_ID
                           join qt in context.Question_Template on q.Question_Template_ID equals qt.Question_Template_ID
                           join qType in context.Question_Type on qt.Question_Type_ID equals qType.Question_Type_ID
                           where pq.Page_ID == pageId
                           && pq.Page_Question_ID != targetQuestionId
                           && qType.HasPossibleAnswers
                           select pq);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Page_Question>, List<PageQuestionDTO>>(dao);
                }
            }
            return result;
        }

        public int GetPageQuestionCountForPage(int pageId)
        {
            return ListPageQuestionsByPage(pageId).Count();
        }

        #endregion

        #region page question validation

        public List<PageQuestionValidationDTO> ListValidationByPageQuestion(int pageQuestionId)
        {
            List<PageQuestionValidationDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                result = ListValidationByPageQuestion(pageQuestionId, context);
            }
            return result;
        }
        private List<PageQuestionValidationDTO> ListValidationByPageQuestion(int pageQuestionId, QuestionnaireEntities context)
        {
            List<PageQuestionValidationDTO> result = QuestionnaireCacheManager.Instance.ListPageValidationsByPageQuestionFromCache(pageQuestionId);
            if (result == null)
            {
                var dao = context.Page_Question_Validation.Where(pqv => pqv.Page_Question_ID == pageQuestionId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Page_Question_Validation>, List<PageQuestionValidationDTO>>(dao);
                    QuestionnaireCacheManager.Instance.InsertPageValidationsByPageQuestionIntoCache(pageQuestionId, result);
                }
            }

            return result;
        }

        #endregion

        #region page question conditional display

        public List<PageQuestionConditionalDisplayDTO> ListPageQuestionConditionalDisplaysByPage(int pageId)
        {
            List<PageQuestionConditionalDisplayDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = (from d in context.Page_Question_Conditional_Display
                           join q in context.Page_Question on d.Target_Page_Question_ID equals q.Page_Question_ID
                           where q.Page_ID == pageId
                           select d);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Page_Question_Conditional_Display>, List<PageQuestionConditionalDisplayDTO>>(dao);
                }
            }
            return result;
        }

        public List<PageQuestionConditionalDisplayDTO> ListPageQuestionConditionalDisplaysByTargetPageQuestion(int targetPageQuestionId)
        {
            List<PageQuestionConditionalDisplayDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Page_Question_Conditional_Display.Where(d => d.Target_Page_Question_ID == targetPageQuestionId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Page_Question_Conditional_Display>, List<PageQuestionConditionalDisplayDTO>>(dao);
                }
            }
            return result;
        }

        private List<PageQuestionConditionalDisplayForDisplayDTO> ListPageQuestionDisplayConditionsForDisplayByPossibleAnswer(QuestionnaireEntities context, int possibleAnswerId)
        {
            List<PageQuestionConditionalDisplayForDisplayDTO> result = null;
            var dao = context.Page_Question_Conditional_Display.Where(c => c.Trigger_Question_Possible_Answer_ID == possibleAnswerId);
            if (dao != null)
            {
                result = AutoMapper.Mapper.Map<IEnumerable<Page_Question_Conditional_Display>, List<PageQuestionConditionalDisplayForDisplayDTO>>(dao);
            }
            return result;
        }

        #endregion

        #endregion

        #endregion

    }
}
