using InsuranceEngine.Business.Contexts;
using InsuranceEngine.Business.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.Web.Models.Questionnaire;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InsuranceEngine.Web.Areas.Customer.Controllers
{
    [RouteArea("Customer")]
    public class QuestionnaireController : Controller
    {

        [Route("Questionnaire/StartNewQuestionnaire/{schemeId:int}", Name="StartingNewQuestionnaire", Order=1)]
        public ActionResult StartNewQuestionnaire(int schemeId)
        {
            QuoteContext.Instance.NewQuote(schemeId);
            return RedirectToAction("Quote", new { schemeCode = QuoteContext.Instance.Scheme.Code, pageName = QuoteContext.Instance.Pages.First().PageName });
        }

        [Route("Questionnaire/OpenExistingQuestionnaire/{quoteId:int}", Name = "OpeningExistingQuestionnaire", Order = 1)]
        public ActionResult OpenExistingQuestionnaire(int quoteId)
        {
            QuoteContext.Instance.ExistingQuote(quoteId);
            RenderedPageForDisplayDTO page = QuoteContext.Instance.Pages.FirstOrDefault();
            return RedirectToAction("Quote", new { schemeCode = QuoteContext.Instance.Scheme.Code, pageName = page.PageName });
        }

        [Route("Questionnaire/Quote/{schemeCode}/{pageName}", Name="QuestionnairePage", Order=1)]
        public ActionResult Quote(string schemeCode, string pageName)
        {

            return View(GetPageVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Questionnaire/Quote/{schemeCode}/{pageName}")]
        public ActionResult Quote(FormCollection form, string schemeCode, string pageName)
        {
            //update the in memory answers on the quote object
            QuoteContext.Instance.UpdateAnswers(form);
            if (ModelState.IsValid)
            {
                //check if server validation passes
                List<ValidationErrorDTO> validationErrorDTOs = new List<ValidationErrorDTO>();
                if (QuoteContext.Instance.ValidationPassed(out validationErrorDTOs))
                {
                    //validation passed
                    QuoteContext.Instance.SaveAnswers();
                    return RedirectToAction("NextPage");
                }
                else
                {
                    //validation failed;                    
                    foreach (ValidationErrorDTO validationErrorDTO in validationErrorDTOs)
                    {
                        ModelState.AddModelError("Question_" + validationErrorDTO.PageQuestionID.ToString(), validationErrorDTO.ErrorMessage);
                    }
                    return View(GetPageVM());
                }

            }
            else
            {   
                //client side validation failed
                return View(GetPageVM());
            }
        }
        
        [Route("Questionnaire/Quote/{schemeCode}/{pageName}/NextPage", Name = "NextQuestionnairePage", Order = 1)]
        public ActionResult NextPage(string schemeCode, string pageName)
        {
            RenderedPageForDisplayDTO nextPage = QuoteContext.Instance.NextPage(null);
            if (nextPage != null)
            {
                return RedirectToAction("Quote", new { schemeCode = QuoteContext.Instance.Scheme.Code, pageName = nextPage.PageName });
            }
            else
            {
                return RedirectToAction("QuoteSummary", new {schemeCode = QuoteContext.Instance.Scheme.Code});
            }
        }

        [Route("Questionnaire/Quote/{schemeCode}/{pageName}/PreviousPage", Name = "PreviousQuestionnairePage", Order = 1)]
        public ActionResult PreviousPage(string schemeCode, string pageName)
        {
            RenderedPageForDisplayDTO previousPage = QuoteContext.Instance.PreviousPage();
            if (previousPage != null)
            {
                return RedirectToAction("Quote", new { schemeCode = QuoteContext.Instance.Scheme.Code, pageName = previousPage.PageName });
            }
            else
            {
                QuoteContext.Instance.Clear();
                return RedirectToAction("ExistingQuotes", "Quotes");
            }
        }

        [Route("Questionnaire/{schemeCode}/QuoteSummary", Name = "QuoteSummaryPage", Order = 1)]
        public ActionResult QuoteSummary(string schemeCode)
        {
            return View(QuoteContext.Instance.Quote);
        }

        private PageVM GetPageVM()
        {
            QuestionVM questionVM = null;
            PageVM page = new PageVM()
            {
                Page = QuoteContext.Instance.GetPage(),
                Questions = new List<QuestionVM>(),
            };
            foreach(var question in page.Page.Questions)
            {
                questionVM = new QuestionVM();
                questionVM.Question = question;
                questionVM.Answers = QuoteContext.Instance.Quote.Answers;
                questionVM.Visibility = ConditionalDisplay.Instance.SetPageQuestionVisbility(question, page.Page, questionVM.Answers);
                page.Questions.Add(questionVM);
            }            
            return page;
        }


	}
}