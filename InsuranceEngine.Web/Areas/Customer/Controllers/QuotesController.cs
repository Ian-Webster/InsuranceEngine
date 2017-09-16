using InsuranceEngine.Business.BusinessManagers.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.Web.Areas.Customer.Models.Quotes;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InsuranceEngine.Web.Areas.Customer.Controllers
{
    [RouteArea("Customer")]
    public class QuotesController : Controller
    {
        
        public ActionResult ExistingQuotes()
        {
            List<QuoteListItemDTO> quotes = QuestionnaireBusinessManager.Instance.ListQuotesForDisplay();
            ExistingQuotesVM vm = new ExistingQuotesVM()
            {
                Quotes = quotes,
                SelectedSchemeID = -1
            };
            AddAdditionalViewData();
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExistingQuotes(ExistingQuotesVM vm)
        {
            return RedirectToAction("StartNewQuestionnaire", "Questionnaire", new { schemeId = vm.SelectedSchemeID });
        }

        [Route("Quotes/OpenQuote/{quoteId:int}", Name = "OpenningExistingQuote", Order = 1)]
        public ActionResult OpenQuote(int quoteId)
        {
            return RedirectToAction("OpenExistingQuestionnaire", "Questionnaire", new { quoteId = quoteId });
        }

        private void AddAdditionalViewData()
        {
            SelectList schemes = new SelectList(QuestionnaireAdminBusinessManager.Instance.ListSchemes(), "SchemeID", "Name");
            ViewBag.Schemes = schemes;
        }

	}
}