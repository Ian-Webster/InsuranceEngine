using InsuranceEngine.Data.DTO.GridData;
using InsuranceEngine.Data.EF.Questionnaire;
using InsuranceEngine.DTO.AttributeNames.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums;
using InsuranceEngine.DTO.Utility.GridData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace InsuranceEngine.Data.DataManagers.Questionnaire
{
    public class QuestionnaireAdminDataManager : DataManagerBase<QuestionnaireAdminDataManager>
    {

        #region delete

        public void DeleteScheme(int schemeId)
        {
            using (var context = new QuestionnaireEntities())
            {
                //delete the quotes for this scheme
                DeleteQuotesForScheme(schemeId, context);
                 
                //delete pages
                List<int> pageIds = (from p in context.Pages
                                     where p.Scheme_ID == schemeId
                                     select p.Page_ID).ToList();

                if (pageIds != null)
                {
                    pageIds.ForEach(p => DeletePage(p, context));
                }

                //delete questions
                List<int> questionIds = (from q in context.Questions
                                         where q.Scheme_ID == schemeId
                                         select q.Question_ID).ToList();

                if (questionIds != null)
                {
                    questionIds.ForEach(q => DeleteQuestion(q, context));
                }

                //delete the scheme
                var scheme = context.Schemes.Find(schemeId);

                if (scheme != null)
                {
                    context.Schemes.Remove(scheme);
                }

                context.SaveChanges();
                QuestionnaireCacheManager.Instance.ClearScheme(schemeId);
            }
        }

        public void DeleteQuestion(int questionId)
        {
            using (var context = new QuestionnaireEntities())
            {
                DeleteQuestion(questionId, context);
                context.SaveChanges();
                QuestionnaireCacheManager.Instance.ClearQuestion(questionId);
            }
        }
        private void DeleteQuestion(int questionId, QuestionnaireEntities context)
        {
            //delete any answers for this question
            List<int> answerIds = (from a in context.Quote_Question_Answer
                                   where a.Question_ID == questionId
                                   select a.Question_ID).ToList();

            if (answerIds != null)
            {
                answerIds.ForEach(a => DeleteQuoteQuestionAnswer(a, context));
            }

            //delete any page question for this question
            List<int> pageQuestionIds = (from pq in context.Page_Question
                                         where pq.Question_ID == questionId
                                         select pq.Page_Question_ID).ToList();

            if (pageQuestionIds != null)
            {
                pageQuestionIds.ForEach(pq => DeletePageQuestion(pq, context));
            }

            //delete possible answers for the question
            List<int> possibleAnswerIds = (from pa in context.Question_Possible_Answer
                                           where pa.Question_ID == questionId
                                           select pa.Question_Possible_Answer_ID).ToList();

            if (possibleAnswerIds != null)
            {
                possibleAnswerIds.ForEach(pa => DeletePossibleAnswer(pa, context));
            }

            //delete the question
            var question = (from q in context.Questions
                            where q.Question_ID == questionId
                            select q).FirstOrDefault();

            if (question != null)
            {
                context.Questions.Remove(question);
            }
        }

        public void DeletePossibleAnswer(int possibleAnswerId)
        {
            using (var context = new QuestionnaireEntities())
            {
                int questionId = context.Question_Possible_Answer.Find(possibleAnswerId).Question_ID;
                DeletePossibleAnswer(possibleAnswerId, context);
                context.SaveChanges();
                QuestionnaireCacheManager.Instance.ClearQuestion(questionId);
            }
        }
        private void DeletePossibleAnswer(int possibleAnswerId, QuestionnaireEntities context)
        {
            //delete any answers that use this possible answer
            List<int> answerIds = (from a in context.Quote_Question_Answer
                                   where a.Question_Possible_Answer_ID == possibleAnswerId
                                   && a.Question_Possible_Answer_ID != null
                                   select a.Question_Possible_Answer_ID.Value).ToList();

            if (answerIds != null)
            {
                answerIds.ForEach(a => DeleteQuoteQuestionAnswer(a, context));
            }

            //delete the possible answer
            var possibleAnswer = context.Question_Possible_Answer.Find(possibleAnswerId);

            if (possibleAnswer != null)
            {
                context.Question_Possible_Answer.Remove(possibleAnswer);
            }
        }

        public void DeletePage(int pageId)
        {
            using (var context = new QuestionnaireEntities())
            {
                //get scheme id
                int schemeId = context.Pages.Find(pageId).Scheme_ID;
                //check if we have pages using this page as it's next page
                if (context.Pages.Any(p => p.Next_Page_ID == pageId))
                {
                    //need to update the pages to use this pages next page for their next page id
                    int? nextPageId = (from p in context.Pages
                                       where p.Page_ID == pageId
                                       select p.Next_Page_ID).FirstOrDefault();

                    var pageDAOs = (from p in context.Pages
                                    where p.Next_Page_ID == pageId
                                    select p).ToList();

                    if  (pageDAOs != null)
                    {
                        pageDAOs.ForEach(p =>
                            {
                                p.Next_Page_ID = nextPageId;
                            });
                    }
                }
                DeletePage(pageId, context);
                context.SaveChanges();
                QuestionnaireCacheManager.Instance.ClearPage(pageId, schemeId);
            }
        }
        private void DeletePage(int pageId, QuestionnaireEntities context)
        {
            //delete page questions
            List<int> pageQuestionIds = (from pq in context.Page_Question
                                         where pq.Page_ID == pageId
                                         select pq.Page_Question_ID).ToList();

            if (pageQuestionIds != null)
            {
                pageQuestionIds.ForEach(pq => DeletePageQuestion(pq, context));
            }

            //delete the rendered page
            var renderedPage = (from rp in context.Rendered_Page
                                where rp.Page_ID == pageId
                                select rp).FirstOrDefault();
            if (renderedPage != null)
            {
                context.Rendered_Page.Remove(renderedPage);
            }

            //delete the page
            var page = context.Pages.Find(pageId);
            if (page != null)
            {
                context.Pages.Remove(page);
            }
        }

        public void DeleteRenderedPage(int renderedPageId)
        {
            using (var context = new QuestionnaireEntities())
            {
                DeleteRenderedPage(renderedPageId);
                context.SaveChanges();
            }
        }
        private void DeleteRenderedPage(int renderedPageId, QuestionnaireEntities context)
        {
            var renderedPage = context.Rendered_Page.Find(renderedPageId);
            if (renderedPage != null)
            {
                context.Rendered_Page.Remove(renderedPage);
            }
        }

        public void DeletePageQuestion(int pageQuestionId)
        {
            using (var context = new QuestionnaireEntities())
            {
                //get page and scheme id for the page question
                var ids = (from pq in context.Page_Question
                           join p in context.Pages on pq.Page_ID equals p.Page_ID
                           where pq.Page_Question_ID == pageQuestionId
                           select new  { PageId = p.Page_ID, SchemeId = p.Scheme_ID}).First();
                DeletePageQuestion(pageQuestionId, context);
                context.SaveChanges();
                QuestionnaireCacheManager.Instance.ClearPageQuestion(pageQuestionId, ids.PageId, ids.SchemeId);
            }
        }
        private void DeletePageQuestion(int pageQuestionId, QuestionnaireEntities context)
        {
            //delete display conditions
            List<int> displayConditionIds = (from dc in context.Page_Question_Conditional_Display
                                             where dc.Source_Page_Question_ID == pageQuestionId
                                             || dc.Target_Page_Question_ID == pageQuestionId
                                             select (dc.Page_Question_Conditional_Display_ID)).ToList();

            if (displayConditionIds != null)
            {
                displayConditionIds.ForEach(dc => DeletePageQuestionDisplayCondition(dc, context));
            }

            //delete validations
            List<int> validationIds = (from v in context.Page_Question_Validation
                                       where v.Page_Question_ID == pageQuestionId
                                       select v.Page_Question_Validation_ID).ToList();

            if (validationIds != null)
            {
                validationIds.ForEach(v => DeletePageQuestionValidation(v, context));
            }

            //delete any answers for this page question
            List<int> answerIdList = (from a in context.Quote_Question_Answer
                                      join pq in context.Page_Question on a.Question_ID equals pq.Question_ID
                                      where pq.Page_Question_ID == pageQuestionId
                                      select a.Quote_Question_Answer_ID).ToList();
            if (answerIdList != null)
            {
                answerIdList.ForEach(a => DeleteQuoteQuestionAnswer(a, context));
            }

            //delete the page question
            var pageQuestion = context.Page_Question.Find(pageQuestionId);
            if (pageQuestion != null)
            {
                context.Page_Question.Remove(pageQuestion);
            }
        }

        public void DeletePageQuestionDisplayCondition(int pageQuestionDisplayConditionId)
        {
            using (var context = new QuestionnaireEntities())
            {
                var ids = (from pqd in context.Page_Question_Conditional_Display
                           join pq in context.Page_Question on pqd.Target_Page_Question_ID equals pq.Page_Question_ID
                           join p in context.Pages on pq.Page_ID equals p.Page_ID
                           where pqd.Page_Question_Conditional_Display_ID == pageQuestionDisplayConditionId
                           select new { PageQuestionId = pq.Page_Question_ID, PageId = p.Page_ID, SchemeId = p.Scheme_ID }).First();
                
                DeletePageQuestionDisplayCondition(pageQuestionDisplayConditionId, context);
                context.SaveChanges();
                QuestionnaireCacheManager.Instance.ClearPageQuestion(ids.PageQuestionId, ids.PageId, ids.SchemeId);
            }
        }
        private void DeletePageQuestionDisplayCondition(int pageQuestionDisplayConditionId, QuestionnaireEntities context)
        {
            var dao = context.Page_Question_Conditional_Display.Find(pageQuestionDisplayConditionId);
            if (dao != null)
            {
                context.Page_Question_Conditional_Display.Remove(dao);
            }
        }

        public void DeletePageQuestionValidation(int pageQuestionValidationId)
        {
            using (var context = new QuestionnaireEntities())
            {
                DeletePageQuestionValidation(pageQuestionValidationId, context);
                context.SaveChanges();
            }
        }
        private void DeletePageQuestionValidation(int pageQuestionValidationId, QuestionnaireEntities context)
        {
            var dao = context.Page_Question_Validation.Find(pageQuestionValidationId);
            if (dao != null)
            {
                context.Page_Question_Validation.Remove(dao);
            }
        }

        private void DeleteQuotesForScheme(int schemeId, QuestionnaireEntities context)
        {
            List<int> answerIdList = (from a in context.Quote_Question_Answer
                                     join q in context.Quotes on a.Quote_ID equals q.Quote_ID
                                     where q.Scheme_ID == schemeId
                                     select a.Quote_Question_Answer_ID).ToList();
            if (answerIdList != null)
            {
                answerIdList.ForEach(a => DeleteQuoteQuestionAnswer(a, context));
            }

            var quotes = (from q in context.Quotes
                          where q.Scheme_ID == schemeId
                          select q);

            if (quotes != null)
            {
                context.Quotes.RemoveRange(quotes);
            }
        }
        private void DeleteQuoteQuestionAnswer(int quoteQuestionAnswerId, QuestionnaireEntities context)
        {
            var answer = context.Quote_Question_Answer.Find(quoteQuestionAnswerId);
            if (answer != null)
            {
                context.Quote_Question_Answer.Remove(answer);
            }
        }

        #endregion

        #region page

        public GridDataResponseDTO<PageForGridDTO> ListPagesBySchemeForGrid(GridDataRequestDTO filterDTO, int schemeId)
        {
            GridDataResponseDTO<PageForGridDTO> result = new GridDataResponseDTO<PageForGridDTO>()
            {
                GridRows = null,
                ItemCount = 0
            };
            IEnumerable<PageForGridDTO> pages = null;
            using (var dbContext = new QuestionnaireEntities())
            {
                string whereClause = null;
                if (filterDTO.Filters != null && filterDTO.Filters.Count > 0)
                {
                    whereClause = GridDataHelper.GetWhereClauseForRequest(filterDTO, false);
                }

                string orderByClause = null;
                if (filterDTO.Sort != null)
                {
                    orderByClause = GridDataHelper.GetSortClauseForRequest(filterDTO);
                }
                IEnumerable<Pages_ListPagesForGrid_Result> pageDAO = dbContext.Pages_ListPagesForGrid(schemeId,
                                                                                                              whereClause,
                                                                                                              orderByClause,
                                                                                                              filterDTO.PageSize,
                                                                                                              filterDTO.PageNumber);
                if (pageDAO != null)
                {
                    pages = AutoMapper.Mapper.Map<IEnumerable<Pages_ListPagesForGrid_Result>, IEnumerable<PageForGridDTO>>(pageDAO);
                    result.GridRows = pages;
                    if (pages.Count() > 0)
                    {
                        result.ItemCount = pages.First().TotalRows;
                    }
                    else
                    {
                        result.ItemCount = 0;
                    }
                }

            }
            return result;
        }

        public List<PageDTO> ListPagesByScheme(int schemeId)
        {
            List<PageDTO> result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Pages.Where(p => p.Scheme_ID == schemeId).OrderBy(p => p.DisplayOrder);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<IEnumerable<Page>, List<PageDTO>>(dao);
                }
            }
            return result;
        }

        public ManagePageStatsDTO GetStatsForPage(int pageId)
        {
            ManagePageStatsDTO result = QuestionnaireCacheManager.Instance.GetStatsForPageFromCache(pageId);
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    result = new ManagePageStatsDTO();

                    result.PageQuestionsCount = (from pq in context.Page_Question
                                                 where pq.Page_ID == pageId
                                                 select pq.Page_Question_ID).Count();

                    QuestionnaireCacheManager.Instance.InsertStatsForPageIntoCache(pageId, result);

                }
            }
            return result;
        }

        public PageDTO LoadPage(int pageId)
        {
            PageDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Pages.Find(pageId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Page, PageDTO>(dao);
                }
            }
            return result;
        }

        public PageDTO SavePage(PageDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Page dao = null;
                if (dto.PageID > 0)
                {
                    dao = context.Pages.Find(dto.PageID);
                }
                else
                {
                    dao = new Page()
                    {
                        Scheme_ID = dto.SchemeID
                    };
                    context.Pages.Add(dao);
                }

                dao.Page_Template_ID = dto.PageTemplateID;
                dao.Title = dto.Title;
                dao.Name = dto.Name;
                dao.DisplayOrder = dto.DisplayOrder;
                dao.Next_Page_ID = dto.NextPageID;

                context.SaveChanges();
                dto.PageID = dao.Page_ID;
                QuestionnaireCacheManager.Instance.ClearPage(dto.PageID, dto.SchemeID);
            }
            return dto;
        }

        public int GetPageCountForScheme(int schemeId)
        {
            return ListPagesByScheme(schemeId).Count();
        }

        #endregion

        #region page question

        public GridDataResponseDTO<PageQuestionForGridDTO> ListPageQuestionsByPageForGrid(GridDataRequestDTO filterDTO, int pageId)
        {
            GridDataResponseDTO<PageQuestionForGridDTO> result = new GridDataResponseDTO<PageQuestionForGridDTO>()
            {
                GridRows = null,
                ItemCount = 0
            };
            IEnumerable<PageQuestionForGridDTO> pageQuestions = null;
            using (var dbContext = new QuestionnaireEntities())
            {
                string whereClause = null;
                if (filterDTO.Filters != null && filterDTO.Filters.Count > 0)
                {
                    whereClause = GridDataHelper.GetWhereClauseForRequest(filterDTO, false);
                }

                string orderByClause = null;
                if (filterDTO.Sort != null)
                {
                    orderByClause = GridDataHelper.GetSortClauseForRequest(filterDTO);
                }
                IEnumerable<PageQuestions_ListPageQuestionsForGrid_Result> pageQuestionDAO = dbContext.PageQuestions_ListPageQuestionsForGrid(pageId,
                                                                                                                                              whereClause,
                                                                                                                                              orderByClause,
                                                                                                                                              filterDTO.PageSize,
                                                                                                                                              filterDTO.PageNumber);
                if (pageQuestionDAO != null)
                {
                    pageQuestions = AutoMapper.Mapper.Map<IEnumerable<PageQuestions_ListPageQuestionsForGrid_Result>, IEnumerable<PageQuestionForGridDTO>>(pageQuestionDAO);
                    result.GridRows = pageQuestions;
                    if (pageQuestions.Count() > 0)
                    {
                        result.ItemCount = pageQuestions.First().TotalRows;
                    }
                    else
                    {
                        result.ItemCount = 0;
                    }
                }

            }
            return result;
        }

        public PageQuestionDTO LoadPageQuestion(int pageQuestionId)
        {
            PageQuestionDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Page_Question.Find(pageQuestionId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Page_Question, PageQuestionDTO>(dao);
                }
            }
            return result;
        }

        public PageQuestionForAdminDisplayDTO LoadPageQuestionForAdminDisplay(int pageQuestionId)
        {
            PageQuestionForAdminDisplayDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = (from pq in context.Page_Question
                           join q in context.Questions on pq.Question_ID equals q.Question_ID
                           where pq.Page_Question_ID == pageQuestionId
                           select new
                           {
                               PageQuestionID = pq.Page_Question_ID,
                               Question = q.Name,
                               QuestionText = pq.QuestionText,
                               QuestionName = pq.QuestionName,
                               DisplayOrder = pq.DisplayOrder,
                               DefaultShow = pq.DefaultShow
                           }).FirstOrDefault();

                if (dao != null)
                {
                    result = new PageQuestionForAdminDisplayDTO()
                    {
                        DefaultShow = dao.DefaultShow,
                        DisplayOrder = dao.DisplayOrder,
                        PageQuestionID = dao.PageQuestionID,
                        Question = dao.Question,
                        QuestionName = dao.QuestionName,
                        QuestionText = dao.QuestionText
                    };
                }

            }
            return result;
        }

        public ManagePageQuestionStats GetStatsForPageQuestion(int pageQuestionId)
        {
            ManagePageQuestionStats result = QuestionnaireCacheManager.Instance.GetStatsForPageQuestionFromCache(pageQuestionId);
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    result = new ManagePageQuestionStats();

                    result.DisplayConditionCount = (from dc in context.Page_Question_Conditional_Display
                                                    where dc.Target_Page_Question_ID == pageQuestionId
                                                    select dc.Page_Question_Conditional_Display_ID).Count();

                    result.ValidationCount = (from qv in context.Page_Question_Validation
                                              where qv.Page_Question_ID == pageQuestionId
                                              select qv.Page_Question_Validation_ID).Count();

                    QuestionnaireCacheManager.Instance.InsertStatsForPageQuestionIntoCache(pageQuestionId, result);

                }
            }
            return result;
        }

        public PageQuestionDTO SavePageQuestion(PageQuestionDTO dto)
        {
            Page_Question dao = null;
            using (var context = new QuestionnaireEntities())
            {
                if (dto.PageQuestionID > 0)
                {
                    dao = context.Page_Question.Find(dto.PageQuestionID);
                }
                else
                {
                    dao = new Page_Question()
                    {
                        Page_ID = dto.PageID
                    };
                    context.Page_Question.Add(dao);
                }

                dao.Question_ID = dto.QuestionID;
                dao.DisplayOrder = dto.DisplayOrder;
                dao.QuestionText = dto.QuestionText;
                dao.QuestionName = dto.QuestionName;
                dao.DefaultShow = dto.DefaultShow;

                context.SaveChanges();
                dto.PageQuestionID = dao.Page_Question_ID;

                var ids = (from pq in context.Page_Question
                           join p in context.Pages on pq.Page_ID equals p.Page_ID
                           where pq.Page_Question_ID == dto.PageQuestionID
                           select new { PageId = p.Page_ID, SchemeId = p.Scheme_ID }).First();

                QuestionnaireCacheManager.Instance.ClearPageQuestion(dto.PageQuestionID, ids.PageId, ids.SchemeId);
            }
            return dto;
        }

        #endregion

        #region page question validation

        public GridDataResponseDTO<PageQuestionValidationForGridDTO> ListPageQuestionValidationsByPageQuestionForGrid(GridDataRequestDTO filterDTO, int pageQuestionId)
        {
            GridDataResponseDTO<PageQuestionValidationForGridDTO> result = new GridDataResponseDTO<PageQuestionValidationForGridDTO>()
            {
                GridRows = null,
                ItemCount = 0
            };
            IEnumerable<PageQuestionValidationForGridDTO> pageQuestionValidations = null;
            using (var dbContext = new QuestionnaireEntities())
            {
                string whereClause = null;
                if (filterDTO.Filters != null && filterDTO.Filters.Count > 0)
                {
                    whereClause = GridDataHelper.GetWhereClauseForRequest(filterDTO, false);
                }

                string orderByClause = null;
                if (filterDTO.Sort != null)
                {
                    orderByClause = GridDataHelper.GetSortClauseForRequest(filterDTO);
                }
                IEnumerable<PageQuestions_ListPageQuestionValidationForGrid_Result> pageQuestionValidationsDAO = dbContext.PageQuestions_ListPageQuestionValidationForGrid(pageQuestionId,
                                                                                                                                                                          whereClause,
                                                                                                                                                                          orderByClause,
                                                                                                                                                                          filterDTO.PageSize,
                                                                                                                                                                          filterDTO.PageNumber);
                if (pageQuestionValidationsDAO != null)
                {
                    pageQuestionValidations = AutoMapper.Mapper.Map<IEnumerable<PageQuestions_ListPageQuestionValidationForGrid_Result>, IEnumerable<PageQuestionValidationForGridDTO>>(pageQuestionValidationsDAO);
                    result.GridRows = pageQuestionValidations;
                    if (pageQuestionValidations.Count() > 0)
                    {
                        result.ItemCount = pageQuestionValidations.First().TotalRows;
                    }
                    else
                    {
                        result.ItemCount = 0;
                    }
                }

            }
            return result;
        }

        public PageQuestionValidationDTO LoadPageQuestionValidation(int pageQuestionValidationId)
        {
            PageQuestionValidationDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Page_Question_Validation.Find(pageQuestionValidationId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Page_Question_Validation, PageQuestionValidationDTO>(dao);
                }
            }
            return result;
        }

        public PageQuestionValidationDTO SavePageQuestionValidation(PageQuestionValidationDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Page_Question_Validation dao = null;
                if (dto.PageQuestionValidationID > 0)
                {
                    dao = context.Page_Question_Validation.Find(dto.PageQuestionValidationID);
                }
                else
                {
                    dao = new Page_Question_Validation()
                    {
                        Page_Question_ID = dto.PageQuestionID
                    };
                    context.Page_Question_Validation.Add(dao);
                }

                dao.Validation_Type_ID = Convert.ToInt16(dto.ValidationType);
                dao.ErrorMessage = dto.ErrorMessage;
                dao.ValidationExpression = dto.ValidationExpression;

                context.SaveChanges();
                dto.PageQuestionValidationID = dao.Page_Question_Validation_ID;

                var ids = (from pq in context.Page_Question
                           join p in context.Pages on pq.Page_ID equals p.Page_ID
                           where pq.Page_Question_ID == dto.PageQuestionID
                           select new { PageId = p.Page_ID, SchemeId = p.Scheme_ID }).First();


                QuestionnaireCacheManager.Instance.ClearPageQuestion(dto.PageQuestionID, ids.PageId, ids.SchemeId);

                dto.PageQuestionValidationID = dao.Page_Question_Validation_ID;
            }
            return dto;
        }

        #endregion

        #region page question conditional display

        public GridDataResponseDTO<PageQuestionConditionalDisplayForGridDTO> ListPageQuestionConditionalDisplaysByPageQuestionForGrid(GridDataRequestDTO filterDTO, int pageQuestionId)
        {
            GridDataResponseDTO<PageQuestionConditionalDisplayForGridDTO> result = new GridDataResponseDTO<PageQuestionConditionalDisplayForGridDTO>()
            {
                GridRows = null,
                ItemCount = 0
            };
            IEnumerable<PageQuestionConditionalDisplayForGridDTO> pageQuestionConditionalDisplays = null;
            using (var dbContext = new QuestionnaireEntities())
            {
                string whereClause = null;
                if (filterDTO.Filters != null && filterDTO.Filters.Count > 0)
                {
                    whereClause = GridDataHelper.GetWhereClauseForRequest(filterDTO, false);
                }

                string orderByClause = null;
                if (filterDTO.Sort != null)
                {
                    orderByClause = GridDataHelper.GetSortClauseForRequest(filterDTO);
                }
                IEnumerable<PageQuestions_ListPageQuestionDisplayConditionsForGrid_Result> pageQuestionConditionalDisplaysDAO = dbContext.PageQuestions_ListPageQuestionDisplayConditionsForGrid(pageQuestionId,
                                                                                                                                                                          whereClause,
                                                                                                                                                                          orderByClause,
                                                                                                                                                                          filterDTO.PageSize,
                                                                                                                                                                          filterDTO.PageNumber);
                if (pageQuestionConditionalDisplaysDAO != null)
                {
                    pageQuestionConditionalDisplays = AutoMapper.Mapper.Map<IEnumerable<PageQuestions_ListPageQuestionDisplayConditionsForGrid_Result>, IEnumerable<PageQuestionConditionalDisplayForGridDTO>>(pageQuestionConditionalDisplaysDAO);
                    result.GridRows = pageQuestionConditionalDisplays;
                    if (pageQuestionConditionalDisplays.Count() > 0)
                    {
                        result.ItemCount = pageQuestionConditionalDisplays.First().TotalRows;
                    }
                    else
                    {
                        result.ItemCount = 0;
                    }
                }

            }
            return result;
        }

        public PageQuestionConditionalDisplayDTO LoadPageQuestionConditionalDisplay(int pageQuestionConditionalDisplayId)
        {
            PageQuestionConditionalDisplayDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Page_Question_Conditional_Display.Find(pageQuestionConditionalDisplayId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Page_Question_Conditional_Display, PageQuestionConditionalDisplayDTO>(dao);
                }
            }
            return result;
        }

        public PageQuestionConditionalDisplayDTO SavePageQuestionConditionalDisplay(PageQuestionConditionalDisplayDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Page_Question_Conditional_Display dao = null;
                if (dto.PageQuestionConditionalDisplayID > 0)
                {
                    dao = context.Page_Question_Conditional_Display.Find(dto.PageQuestionConditionalDisplayID);
                }
                else
                {
                    dao = new Page_Question_Conditional_Display()
                    {
                        Target_Page_Question_ID = dto.TargetPageQuestionID
                    };
                    context.Page_Question_Conditional_Display.Add(dao);
                }

                dao.Source_Page_Question_ID = dto.SourcePageQuestionID;
                dao.Trigger_Question_Possible_Answer_ID = dto.TriggerQuestionPossibleAnswerID;
                dao.Hide = dto.Hide;

                context.SaveChanges();

                dto.PageQuestionConditionalDisplayID = dao.Page_Question_Conditional_Display_ID;

                var ids = (from pqd in context.Page_Question_Conditional_Display
                           join pq in context.Page_Question on pqd.Target_Page_Question_ID equals pq.Page_Question_ID
                           join p in context.Pages on pq.Page_ID equals p.Page_ID
                           where pqd.Page_Question_Conditional_Display_ID == dto.PageQuestionConditionalDisplayID
                           select new { PageQuestionId = pq.Page_Question_ID, PageId = p.Page_ID, SchemeId = p.Scheme_ID }).First();

                QuestionnaireCacheManager.Instance.ClearPageQuestion(ids.PageQuestionId, ids.PageId, ids.SchemeId);

                dto.PageQuestionConditionalDisplayID = dao.Page_Question_Conditional_Display_ID;
            }
            return dto;
        }

        #endregion

        #region possible answer

        public GridDataResponseDTO<PossibleAnswerForGridDTO> ListPossibleAnswersByQuestionForGrid(GridDataRequestDTO filterDTO, int questionId)
        {
            GridDataResponseDTO<PossibleAnswerForGridDTO> result = new GridDataResponseDTO<PossibleAnswerForGridDTO>()
            {
                GridRows = null,
                ItemCount = 0
            };
            IEnumerable<PossibleAnswerForGridDTO> possibleAnswers = null;
            using (var dbContext = new QuestionnaireEntities())
            {
                string whereClause = null;
                if (filterDTO.Filters != null && filterDTO.Filters.Count > 0)
                {
                    whereClause = GridDataHelper.GetWhereClauseForRequest(filterDTO, false);
                }

                string orderByClause = null;
                if (filterDTO.Sort != null)
                {
                    orderByClause = GridDataHelper.GetSortClauseForRequest(filterDTO);
                }
                IEnumerable<Questions_ListPossibleAnswerForGrid_Result> possibleAnswersDOA = dbContext.Questions_ListPossibleAnswerForGrid(questionId,
                                                                                                                                            whereClause,
                                                                                                                                            orderByClause,
                                                                                                                                            filterDTO.PageSize,
                                                                                                                                            filterDTO.PageNumber);
                if (possibleAnswersDOA != null)
                {
                    possibleAnswers = AutoMapper.Mapper.Map<IEnumerable<Questions_ListPossibleAnswerForGrid_Result>, IEnumerable<PossibleAnswerForGridDTO>>(possibleAnswersDOA);
                    result.GridRows = possibleAnswers;
                    if (possibleAnswers.Count() > 0)
                    {
                        result.ItemCount = possibleAnswers.First().TotalRows;
                    }
                    else
                    {
                        result.ItemCount = 0;
                    }
                }

            }
            return result;
        }


        public QuestionPossibleAnswerDTO LoadPossibleAnswer(int possibleAnswerId)
        {
            QuestionPossibleAnswerDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Question_Possible_Answer.Find(possibleAnswerId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Question_Possible_Answer, QuestionPossibleAnswerDTO>(dao);
                }
            }
            return result;
        }

        public QuestionPossibleAnswerDTO SavePossibleAnswer(QuestionPossibleAnswerDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Question_Possible_Answer dao = null;
                if (dto.QuestionPossibleAnswerID > 0)
                {
                    dao = context.Question_Possible_Answer.Find(dto.QuestionPossibleAnswerID);
                }
                else
                {
                    dao = new Question_Possible_Answer()
                    {
                        Question_ID = dto.QuestionID
                    };
                    context.Question_Possible_Answer.Add(dao);
                }

                dao.AnswerText = dto.AnswerText;
                dao.AnswerValue = dto.AnswerValue;
                dao.DisplayOrder = dto.DisplayOrder;

                context.SaveChanges();
                QuestionnaireCacheManager.Instance.ClearQuestion(dto.QuestionID);
                dto.QuestionPossibleAnswerID = dao.Question_Possible_Answer_ID;
            }
            return dto;
        }

        public int GetQuestionPossibleAnswerCountForQuestion(int questionId)
        {
            return QuestionnaireDataManager.Instance.ListPossibleAnswersForQuestion(questionId).Count();
        }

        public bool PossibleAnswerValueInUseForQuestion(int questionId, string answerValue, int possibleQuestionId)
        {
            bool result = false;
            using (var context = new QuestionnaireEntities())
            {
                result = context.Question_Possible_Answer.Any(qpa => qpa.AnswerValue == answerValue
                                                              && qpa.Question_ID == questionId
                                                              && qpa.Question_Possible_Answer_ID != possibleQuestionId);
            }
            return result;
        }

        #endregion

        #region question

        public GridDataResponseDTO<QuestionForGridDTO> ListQuestionsBySchemeForGrid(GridDataRequestDTO filterDTO, int schemeId)
        {
            GridDataResponseDTO<QuestionForGridDTO> result = new GridDataResponseDTO<QuestionForGridDTO>()
            {
                GridRows = null,
                ItemCount = 0
            };
            IEnumerable<QuestionForGridDTO> questions = null;
            using (var dbContext = new QuestionnaireEntities())
            {
                string whereClause = null;
                if (filterDTO.Filters != null && filterDTO.Filters.Count > 0)
                {
                    whereClause = GridDataHelper.GetWhereClauseForRequest(filterDTO, false);
                }

                string orderByClause = null;
                if (filterDTO.Sort != null)
                {
                    orderByClause = GridDataHelper.GetSortClauseForRequest(filterDTO);
                }
                IEnumerable<Questions_ListQuestionsForGrid_Result> questionDAO = dbContext.Questions_ListQuestionsForGrid(schemeId,
                                                                                                              whereClause,
                                                                                                              orderByClause,
                                                                                                              filterDTO.PageSize,
                                                                                                              filterDTO.PageNumber);
                if (questionDAO != null)
                {
                    questions = AutoMapper.Mapper.Map<IEnumerable<Questions_ListQuestionsForGrid_Result>, IEnumerable<QuestionForGridDTO>>(questionDAO);
                    result.GridRows = questions;
                    if (questions.Count() > 0)
                    {
                        result.ItemCount = questions.First().TotalRows;
                    }
                    else
                    {
                        result.ItemCount = 0;
                    }
                }

            }
            return result;
        }

        public QuestionDTO LoadQuestion(int questionId)
        {
            QuestionDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Questions.Find(questionId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Question, QuestionDTO>(dao);
                }
            }
            return result;
        }

        public QuestionForAdminDisplayDTO LoadQuestionForAdminDisplay(int questionId)
        {
            QuestionForAdminDisplayDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = (from q in context.Questions
                           join qt in context.Question_Template on q.Question_Template_ID equals qt.Question_Template_ID
                           join t in context.Question_Type on qt.Question_Type_ID equals t.Question_Type_ID
                           where q.Question_ID == questionId
                           select new
                           {
                               QuestionID = q.Question_ID,
                               QuestionTemplate = qt.Name,
                               Name = q.Name,
                               Code = q.Code,
                               HasPossibleAnswers = t.HasPossibleAnswers
                           }).FirstOrDefault();

                if (dao != null)
                {
                    result = new QuestionForAdminDisplayDTO()
                    {
                        Code = dao.Code,
                        HasPossibleAnswers = dao.HasPossibleAnswers,
                        Name = dao.Name,
                        QuestionID = dao.QuestionID,
                        QuestionTemplate = dao.QuestionTemplate
                    };
                }
            }
            return result;
        }

        public ManageQuestionStatsDTO GetStatsForQuestion(int questionId)
        {
            ManageQuestionStatsDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                int? possibleAnswerCount = (from pa in context.Question_Possible_Answer
                                            where pa.Question_ID == questionId
                                            select pa.Question_Possible_Answer_ID
                                           ).Count();
                if (possibleAnswerCount.HasValue)
                {
                    result = new ManageQuestionStatsDTO()
                    {
                        PossibleAnswersCount = possibleAnswerCount.Value
                    };
                }
                else
                {
                    result = new ManageQuestionStatsDTO()
                    {
                        PossibleAnswersCount = 0
                    };
                }
            }
            return result;
        }

        public QuestionDTO SaveQuestion(QuestionDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Question dao = null;
                if (dto.QuestionID > 0)
                {
                    dao = context.Questions.Find(dto.QuestionID);
                }
                else
                {
                    dao = new Question()
                    {
                        Scheme_ID = dto.SchemeID
                    };
                    context.Questions.Add(dao);
                }

                dao.Question_Template_ID = dto.QuestionTemplateID;
                dao.Name = dto.Name;
                dao.Code = dto.Code;

                context.SaveChanges();

                dto.QuestionID = dao.Question_ID;

                QuestionnaireCacheManager.Instance.ClearQuestion(dto.QuestionID);
            }
            return dto;
        }    

        #endregion

        #region rendered pages

        public GridDataResponseDTO<RenderedPageForGridDTO> ListRenderedPagesBySchemeForGrid(GridDataRequestDTO filterDTO, int schemeId)
        {
            GridDataResponseDTO<RenderedPageForGridDTO> result = new GridDataResponseDTO<RenderedPageForGridDTO>()
            {
                GridRows = null,
                ItemCount = 0
            };
            IEnumerable<RenderedPageForGridDTO> renderedPages = null;
            using (var dbContext = new QuestionnaireEntities())
            {
                string whereClause = null;
                if (filterDTO.Filters != null && filterDTO.Filters.Count > 0)
                {
                    whereClause = GridDataHelper.GetWhereClauseForRequest(filterDTO, false);
                }

                string orderByClause = null;
                if (filterDTO.Sort != null)
                {
                    orderByClause = GridDataHelper.GetSortClauseForRequest(filterDTO);
                }
                IEnumerable<RenderedPages_ListRenderedPagesForGrid_Result> renderedPagesDAO = dbContext.RenderedPages_ListRenderedPagesForGrid(schemeId,
                                                                                                              whereClause,
                                                                                                              orderByClause,
                                                                                                              filterDTO.PageSize,
                                                                                                              filterDTO.PageNumber);
                if (renderedPagesDAO != null)
                {
                    renderedPages = AutoMapper.Mapper.Map<IEnumerable<RenderedPages_ListRenderedPagesForGrid_Result>, IEnumerable<RenderedPageForGridDTO>>(renderedPagesDAO);
                    result.GridRows = renderedPages;
                    if (renderedPages.Count() > 0)
                    {
                        result.ItemCount = renderedPages.First().TotalRows;
                    }
                    else
                    {
                        result.ItemCount = 0;
                    }
                }

            }
            return result;
        }

        #endregion

        #region schemes

        public List<SchemeDTO> ListSchemes()
        {
            List<SchemeDTO> result = QuestionnaireCacheManager.Instance.ListSchemesFromCache();
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    var dao = context.Schemes.OrderBy(s => s.Code);

                    if (dao != null)
                    {
                        result = AutoMapper.Mapper.Map<IEnumerable<Scheme>, List<SchemeDTO>>(dao);
                        QuestionnaireCacheManager.Instance.InsertlistSchemesIntoCache(result);
                    }
                }
            }
            return result;
        }

        public GridDataResponseDTO<SchemeForGridDTO> ListSchemesForGrid(GridDataRequestDTO filterDTO)
        {
            GridDataResponseDTO<SchemeForGridDTO> result = new GridDataResponseDTO<SchemeForGridDTO>()
            {
                GridRows = null,
                ItemCount = 0
            };
            IEnumerable<SchemeForGridDTO> schemes = null;
            using (var dbContext = new QuestionnaireEntities())
            {
                IQueryable<Scheme> schemesDAO = dbContext.Schemes.OrderBy(n => n.Name);

                if (filterDTO.Filters != null)
                {
                    //apply filters
                    schemesDAO = schemesDAO.Where(GridDataHelper.GetWhereClauseForRequest(filterDTO, true));
                }

                //set total items count
                result.ItemCount = schemesDAO.Count();

                if (filterDTO.Sort != null)
                {
                    //apply sort
                    schemesDAO = schemesDAO.OrderBy(GridDataHelper.GetSortClauseForRequest(filterDTO));
                }

                //sort out paging
                if (filterDTO.PageNumber > 0)
                {
                    schemesDAO = schemesDAO.Skip((filterDTO.PageNumber - 1) * filterDTO.PageSize);
                }
                schemesDAO = schemesDAO.Take(filterDTO.PageSize);

                if (schemesDAO != null)
                {
                    schemes = AutoMapper.Mapper.Map<IQueryable<Scheme>, IEnumerable<SchemeForGridDTO>>(schemesDAO);
                    result.GridRows = schemes;
                }

            }
            return result;
        }

        public ManageSchemeStatsDTO GetStatsForScheme(int schemeId)
        {
            ManageSchemeStatsDTO result = QuestionnaireCacheManager.Instance.GetStatsForSchemeFromCache(schemeId);
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    result = new ManageSchemeStatsDTO();

                    result.PageCount = (from page in context.Pages
                                        where page.Scheme_ID == schemeId
                                        select page.Page_ID).Count();
                    result.QuestionCount = (from question in context.Questions
                                            where question.Scheme_ID == schemeId
                                            select question.Question_ID).Count();
                    result.RenderedPageCount = (from renderedPage in context.Rendered_Page
                                                join page in context.Pages on renderedPage.Page_ID equals page.Page_ID
                                                where page.Scheme_ID == schemeId
                                                select renderedPage.Rendered_Page_ID).Distinct().Count();

                    QuestionnaireCacheManager.Instance.InsertStatsForSchemeIntoCache(schemeId, result);
                }
            }
            return result;
        }

        public SchemeDTO LoadScheme(int schemeId)
        {
            SchemeDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Schemes.Find(schemeId);
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Scheme, SchemeDTO>(dao);
                }
            }
            return result;
        }
        public SchemeDTO LoadScheme(string schemeCode)
        {
            SchemeDTO result = null;
            using (var context = new QuestionnaireEntities())
            {
                var dao = context.Schemes.Where(s => s.Code.Equals(schemeCode, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (dao != null)
                {
                    result = AutoMapper.Mapper.Map<Scheme, SchemeDTO>(dao);
                }
            }
            return result;
        }

        public SchemeDTO SaveScheme(SchemeDTO dto)
        {
            using (var context = new QuestionnaireEntities())
            {
                Scheme dao = null;
                if (dto.SchemeID > 0)
                {
                    dao = context.Schemes.Find(dto.SchemeID);
                }
                else
                {
                    dao = new Scheme();
                    context.Schemes.Add(dao);
                }

                dao.Name = dto.Name;
                dao.Code = dto.Code;

                context.SaveChanges();
                dto.SchemeID = dao.Scheme_ID;
                QuestionnaireCacheManager.Instance.ClearScheme(dto.SchemeID);
            }
            return dto;
        }

        #endregion

        #region tree structure scheme

        private const string TreeStructureIcon_Scheme = "fa fa-building-o";
        private const string TreeStructureIcon_Page = "fa fa-file-text";
        private const string TreeStructureIcon_PageQuestion = "fa fa-question-circle";
        private const string TreeStructureIcon_Question = "fa fa-question";
        private const string TreeStructureIcon_RenderedPage = "fa fa-file-text-o";

        public TreeNodeDTO GetTreeStructureForScheme(int schemeId)
        {
            TreeNodeDTO result = QuestionnaireCacheManager.Instance.GetTreeStructureBySchemeFromCache(schemeId);
            if (result == null)
            {
                using (var context = new QuestionnaireEntities())
                {
                    var scheme = context.Schemes.Where(s => s.Scheme_ID == schemeId).FirstOrDefault();

                    if (scheme != null)
                    {
                        result = new TreeNodeDTO()
                        {
                            children = new List<TreeNodeDTO>(),
                            data = new Dictionary<string, string>(),
                            NodeType = NodeTypes.Scheme,
                            id = SchemeTreeNodeAttributes.ID_Scheme + scheme.Scheme_ID.ToString(),
                            icon = TreeStructureIcon_Scheme,
                            text = scheme.Name
                        };
                        //set the node type
                        result.data.Add(SchemeTreeNodeAttributes.NodeType, Convert.ToInt16(result.NodeType).ToString());

                        //create pages folder
                        TreeNodeDTO pageFolder = new TreeNodeDTO()
                        {
                            children = new List<TreeNodeDTO>(),
                            data = new Dictionary<string, string>(),
                            id = string.Empty,
                            NodeType = NodeTypes.PageFolder,                            
                            text = "Pages"
                        };
                        //set the node type
                        pageFolder.data.Add(SchemeTreeNodeAttributes.NodeType, Convert.ToInt16(pageFolder.NodeType).ToString());

                        //load the pages for this scheme
                        TreeNodeDTO pageNode = null;
                        TreeNodeDTO pageQuestionNode = null;
                        foreach (var page in scheme.Pages.OrderBy(s => s.DisplayOrder))
                        {
                            pageNode = new TreeNodeDTO()
                            {
                                children = new List<TreeNodeDTO>(),
                                data = new Dictionary<string, string>(),
                                icon = TreeStructureIcon_Page,
                                id = SchemeTreeNodeAttributes.ID_Page + page.Page_ID.ToString(),                                
                                NodeType = NodeTypes.Page,
                                text = page.Name
                            };
                            //set node type
                            pageNode.data.Add(SchemeTreeNodeAttributes.NodeType, Convert.ToInt16(pageNode.NodeType).ToString());

                            foreach (var pageQuestion in page.Page_Question.OrderBy(pq => pq.DisplayOrder))
                            {
                                pageQuestionNode = new TreeNodeDTO()
                                {
                                    data = new Dictionary<string, string>(),
                                    icon = TreeStructureIcon_PageQuestion,
                                    id = SchemeTreeNodeAttributes.ID_PageQuestion + pageQuestion.Page_Question_ID.ToString(),
                                    NodeType = NodeTypes.PageQuestion,
                                    text = pageQuestion.QuestionName
                                };
                                //set node type
                                pageQuestionNode.data.Add(SchemeTreeNodeAttributes.NodeType, Convert.ToInt16(pageQuestionNode.NodeType).ToString());
                                //set page id
                                pageQuestionNode.data.Add(SchemeTreeNodeAttributes.PageID, page.Page_ID.ToString());
                                pageNode.children.Add(pageQuestionNode);
                            }

                            //add the node to the folder
                            pageFolder.children.Add(pageNode);
                        }

                        //add the page folder to the scheme node
                        result.children.Add(pageFolder);

                        //create question folder
                        TreeNodeDTO questionFolder = new TreeNodeDTO()
                        {
                            children = new List<TreeNodeDTO>(),
                            data = new Dictionary<string, string>(),
                            id = string.Empty,
                            NodeType = NodeTypes.QuestionFolder,
                            text = "Questions"
                        };
                        //set node type
                        questionFolder.data.Add(SchemeTreeNodeAttributes.NodeType, Convert.ToInt16(questionFolder.NodeType).ToString());

                        //load questions for this scheme
                        TreeNodeDTO questionNode = null;
                        foreach (var question in scheme.Questions.OrderBy(q => q.Name))
                        {
                            questionNode = new TreeNodeDTO()
                            {
                                data = new Dictionary<string, string>(),
                                icon = TreeStructureIcon_Question,
                                id = SchemeTreeNodeAttributes.ID_Question + question.Question_ID.ToString(),
                                NodeType = NodeTypes.Question,
                                text = question.Name
                            };
                            //set node type
                            questionNode.data.Add(SchemeTreeNodeAttributes.NodeType, Convert.ToInt16(questionNode.NodeType).ToString());
                            //set the "has possible answers" data
                            if (question.Question_Template.Question_Type.HasPossibleAnswers)
                            {
                                questionNode.data.Add(SchemeTreeNodeAttributes.HasPossibleAnswers, true.ToString());
                            }
                            else
                            {
                                questionNode.data.Add(SchemeTreeNodeAttributes.HasPossibleAnswers, false.ToString());
                            }
                            //add the node to the folder
                            questionFolder.children.Add(questionNode);
                        }

                        //add the question folder to the scheme node
                        result.children.Add(questionFolder);

                        //create rendered pages folder
                        TreeNodeDTO renderedPageFolder = new TreeNodeDTO()
                        {
                            children = new List<TreeNodeDTO>(),
                            data = new Dictionary<string, string>(),
                            id = string.Empty,
                            NodeType = NodeTypes.RenderedPageFolder,
                            text = "Rendered Pages"
                        };
                        //set node type
                        renderedPageFolder.data.Add(SchemeTreeNodeAttributes.NodeType, Convert.ToInt16(renderedPageFolder.NodeType).ToString());

                        //load the rendered pages for this scheme
                        var renderedPages = (from rp in context.Rendered_Page
                                             join p in context.Pages on rp.Page_ID equals p.Page_ID
                                             where p.Scheme_ID == schemeId
                                             orderby p.DisplayOrder
                                             select new
                                             {
                                                 Rendered_Page_ID = rp.Rendered_Page_ID,
                                                 Name = p.Name,
                                                 LastRenderDate = rp.LastRenderDate
                                             });
                        if (renderedPages != null)
                        {
                            TreeNodeDTO renderedPageNode = null;
                            foreach (var renderedPage in renderedPages)
                            {
                                renderedPageNode = new TreeNodeDTO()
                                {
                                    data = new Dictionary<string, string>(),
                                    icon = TreeStructureIcon_RenderedPage,
                                    id = "RenderedPageID-" + renderedPage.Rendered_Page_ID.ToString(),
                                    NodeType = NodeTypes.RenderedPage,
                                    text = renderedPage.Name
                                };
                                //set node type
                                renderedPageNode.data.Add(SchemeTreeNodeAttributes.NodeType, Convert.ToInt16(renderedPageNode.NodeType).ToString());
                                //set last render date
                                renderedPageNode.data.Add(SchemeTreeNodeAttributes.LastRenderDate, renderedPage.LastRenderDate.ToString());
                                //add the node to the folder
                                renderedPageFolder.children.Add(renderedPageNode);
                            }
                        }

                        result.children.Add(renderedPageFolder);

                        //insert into the cache
                        QuestionnaireCacheManager.Instance.InsertTreeStructureBySchemeIntoCache(schemeId, result);
                    }
                }
            }
            return result;
        }

        #endregion

    }
}
