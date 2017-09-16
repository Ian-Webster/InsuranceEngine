using InsuranceEngine.Business.BusinessManagers.Questionnaire;
using InsuranceEngine.Business.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace InsuranceEngine.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class TemplatesController : Controller
    {

        #region question templates

        public ActionResult ListQuestionTemplates()
        {
            List<QuestionTemplateDTO> templates = QuestionnaireBusinessManager.Instance.ListQuestionTemplates();
            return View(templates);
        }

        public ActionResult AddQuestionTemplate()
        {
            AddAdditionalViewDataForQuestionTemplate();
            return View(QuestionnaireBusinessManager.Instance.NewQuestionTemplate());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestionTemplate(QuestionTemplateDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireBusinessManager.Instance.SaveQuestionTemplate(dto);
                return RedirectToAction("ListQuestionTemplates");
            }
            else
            {
                AddAdditionalViewDataForQuestionTemplate();
                return View(dto);
            }
        }

        [Route("Templates/EditQuestionTemplate/{questionTemplateId:int}", Name = "EdittingQuestionTemplate", Order = 1)]
        public ActionResult EditQuestionTemplate(int questionTemplateId)
        {
            QuestionTemplateDTO dto = QuestionnaireBusinessManager.Instance.LoadQuestionTemplate(questionTemplateId);
            if (dto != null)
            {
                AddAdditionalViewDataForQuestionTemplate();
                return View(dto);
            }
            else
            {
                throw new HttpException("Unable to load question template id " + questionTemplateId.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Templates/EditQuestionTemplate/{questionTemplateId:int}")]
        public ActionResult EditQuestionTemplate(int questionTemplateId, QuestionTemplateDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireBusinessManager.Instance.SaveQuestionTemplate(dto);
                return RedirectToAction("ListQuestionTemplates");
            }
            else
            {
                AddAdditionalViewDataForQuestionTemplate();
                return View(dto);
            }
        }

        public ActionResult RenderQuestionTemplates()
        {
            QuestionnaireRendering.RenderQuestionTemplates();
            return RedirectToAction("ListQuestionTemplates");
        }

        [Route("Templates/RenderQuestionTemplate/{questionTemplateId:int}", Name = "RenderingQuestionnaireTemplate", Order = 1)]
        public ActionResult RenderQuestionTemplate(int questionTemplateId)
        {
            QuestionnaireRendering.RenderQuestionTemplate(questionTemplateId);
            return RedirectToAction("ListQuestionTemplates");
        }

        private void AddAdditionalViewDataForQuestionTemplate()
        {
            ViewBag.QuestionTypes = new SelectList(QuestionnaireBusinessManager.Instance.ListQuestionTypes(), "QuestionTypeID", "Name");
        }

        #endregion

        #region page templates

        public ActionResult ListPageTemplates()
        {
            return View(QuestionnaireBusinessManager.Instance.ListPageTemplates());
        }

        public ActionResult AddPageTemplate()
        {
            return View(QuestionnaireBusinessManager.Instance.NewPageTemplate());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPageTemplate(PageTemplateDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireBusinessManager.Instance.SavePageTemplate(dto);
                return RedirectToAction("ListPageTemplates");
            }
            else
            {
                return View(dto);
            }
        }

        [Route("Templates/EditPageTemplate/{pageTemplateId:int}", Name="EdittingPageTemplate", Order=1)]
        public ActionResult EditPageTemplate(int pageTemplateId)
        {
            PageTemplateDTO dto = QuestionnaireBusinessManager.Instance.LoadPageTemplate(pageTemplateId);
            if (dto != null)
            {
                return View(dto);
            }
            else
            {
                throw new HttpException("Unable to load page template id " + pageTemplateId.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Templates/EditPageTemplate/{pageTemplateId:int}")]
        public ActionResult EditPageTemplate(int pageTemplateId, PageTemplateDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireBusinessManager.Instance.SavePageTemplate(dto);
                return RedirectToAction("ListPageTemplates");
            }
            else
            {
                return View(dto);
            }
        }

        #endregion

    }
}