using InsuranceEngine.Caching;
using InsuranceEngine.Business.BusinessManagers.Questionnaire;
using InsuranceEngine.Business.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.DTO.Questionnaire.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InsuranceEngine.Business.Contexts
{
    public class QuoteContext : ContextBase<QuoteContext>
    {

        private const string SubSystemKey = "QuoteContext";

        #region properties

        private const string SchemeIDCacheKey = "SchemeID";
        public int SchemeID
        {
            get
            {
                return OutofProcessAccess.Instance.GetUserSpecificCacheItem<int>(SubSystemKey, SchemeIDCacheKey);
            }
        }
        private int SetSchemeID
        {
            set
            {
                OutofProcessAccess.Instance.InsertUserSpecificCacheItem<int>(SubSystemKey, SchemeIDCacheKey, value);
            }
        }

        public SchemeDTO Scheme
        {
            get
            {
                return QuestionnaireAdminBusinessManager.Instance.LoadScheme(SchemeID);
            }
        }

        public List<RenderedPageForDisplayDTO> Pages
        {
            get
            {
                return QuestionnaireBusinessManager.Instance.ListRenderedPagesForDisplayByScheme(SchemeID);
            }
        }

        private const string QuoteCacheKey = "Quote";
        public QuoteDTO Quote
        {
            get
            {
                return OutofProcessAccess.Instance.GetUserSpecificCacheItem<QuoteDTO>(SubSystemKey, QuoteCacheKey);
            }
        }
        private QuoteDTO SetQuote
        {
            set
            {
                OutofProcessAccess.Instance.InsertUserSpecificCacheItem<QuoteDTO>(SubSystemKey, QuoteCacheKey, value);
            }
        }

        #endregion

        #region methods

        public void NewQuote(int schemeId)
        {            
            QuoteDTO quote = new QuoteDTO()
            {
                QuoteDate = DateTime.Now,
                SchemeID = schemeId,
                Answers = new List<QuoteQuestionAnswerDTO>()
            };
            SetSchemeID = schemeId;
            SetQuote = quote;
            quote.Reference = ReferenceGenerator();
            quote = QuestionnaireBusinessManager.Instance.SaveQuote(quote);            
        }

        public void ExistingQuote(int quoteId)
        {            
            SetQuote = QuestionnaireBusinessManager.Instance.LoadQuote(quoteId);
            SetSchemeID = Quote.SchemeID;
            if (Quote.Answers == null)
            {
                Quote.Answers = new List<QuoteQuestionAnswerDTO>();
            }
        }

        public RenderedPageForDisplayDTO GetPage()
        {
            return GetCurrentPageFromRoute();
        }

        public RenderedPageForDisplayDTO NextPage(FormCollection form)
        {
            RenderedPageForDisplayDTO currentPage = GetCurrentPageFromRoute();
            if (currentPage != null)
            {                
                if (currentPage.NextPageID.HasValue && Pages.Any(p => p.PageID == currentPage.NextPageID.Value))
                {
                    return Pages.Where(p => p.PageID == currentPage.NextPageID.Value).FirstOrDefault();
                }
                else
                {
                    //no further pages
                    return null;
                }
            }
            else
            {
                throw new Exception("unable to find page");
            }
        }

        public RenderedPageForDisplayDTO PreviousPage()
        {
            RenderedPageForDisplayDTO currentPage = GetCurrentPageFromRoute();
            if (currentPage != null)
            {
                if (Pages.Any(p => p.NextPageID.HasValue && p.NextPageID == currentPage.PageID))
                {
                    return Pages.Where(p => p.NextPageID == currentPage.PageID).FirstOrDefault();
                }
                else
                {
                    //no previous pages
                    return null;
                }
            }
            else
            {
                throw new Exception("unable to find page");
            }
        }

        /// <summary>
        /// Updates the list of in memory answer for the quote
        /// Also updates the list of "removed" anwsers
        /// </summary>
        /// <param name="form">form submitted by the user</param>
        public void UpdateAnswers(FormCollection form)
        {
            RenderedPageForDisplayDTO currentPage = GetCurrentPageFromRoute();
            //parse the answers from the form
            List<AnswerDTO> parsedAnswerDTOs = AnswerParser.Instance.GetAnswersFromForm(form);
            //convert parsed answers into quote question answer dto's
            Quote.Answers = AnswerParser.Instance.ParseAnswers(Quote.QuoteID, parsedAnswerDTOs, Quote.Answers, currentPage.Questions);
            //get list of question visibility
            Quote.VisbilitySettings = ConditionalDisplay.Instance.GetPageQuestionVisbilitiesForPage(currentPage, Quote.Answers);   
            //update the list of removed answers
            Quote.RemovedAnswers = AnswerParser.Instance.FindRemovedAnswers(Quote.Answers, currentPage.Questions, Quote.VisbilitySettings);
            //update the answers list to exclude the removed answers
            Quote.RemovedAnswers.ForEach(a => Quote.Answers.Remove(a));
        }

        /// <summary>
        /// Runs server side copy of question validation, returns a list of failed validation rules
        /// </summary>
        /// <param name="validationErrorDTOs"></param>
        /// <returns></returns>
        public bool ValidationPassed(out List<ValidationErrorDTO> validationErrorDTOs)
        {
            bool result = true;
            RenderedPageForDisplayDTO currentPage = GetCurrentPageFromRoute();
            //run the validation
            result = Validation.Instance.ValidationPassed(currentPage.Questions, Quote.Answers, Quote.VisbilitySettings, out validationErrorDTOs);
            return result;
        }

        /// <summary>
        /// Saves answers current held agaisnt the Quote object
        /// </summary>
        public void SaveAnswers()
        {
            if (Quote.RemovedAnswers != null && Quote.RemovedAnswers.Count > 0)
            {
                //delete any removed answers
                QuestionnaireBusinessManager.Instance.DeleteAnswers(Quote.RemovedAnswers);
                //reset the removed answers
                Quote.RemovedAnswers = new List<QuoteQuestionAnswerDTO>();
            }            
            //save the answers
            Quote.Answers = QuestionnaireBusinessManager.Instance.SaveAnswers(Quote.Answers);
        }

        public void Clear()
        {
            OutofProcessAccess.Instance.ClearUserSpecificSubsystemCacheItems(SubSystemKey);
        }

        private string ReferenceGenerator()
        {
            string result = Scheme.Code;
            DateTime date = DateTime.Now;
            result += "-" + date.ToString("yyMMdd");
            result += "-" + date.ToString("HHmmss");
            
            return result;
        }

        private RenderedPageForDisplayDTO GetCurrentPageFromRoute()
        {
            RenderedPageForDisplayDTO result = null;
            RouteData routeData = HttpContext.Current.Request.RequestContext.RouteData;

            string pageName = routeData.Values["pageName"].ToString();

            if (!String.IsNullOrWhiteSpace(pageName))
            {
                result = Pages.Where(p => p.PageName.ToLower() == pageName.ToLower()).FirstOrDefault();
            }

            return result;
        }

        #endregion

    }
}
