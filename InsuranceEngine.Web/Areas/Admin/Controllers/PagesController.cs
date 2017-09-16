using InsuranceEngine.Business.BusinessManagers.Questionnaire;
using InsuranceEngine.Business.Questionnaire;
using InsuranceEngine.DTO.AttributeNames;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Utility.Enums.AdminUIEnums;
using InsuranceEngine.DTO.Utility.GridData;
using InsuranceEngine.Web.Areas.Admin.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace InsuranceEngine.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class PagesController : Controller
    {

        #region delete

        [Route("Pages/DeletePage/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}", Order = 1, Name = "DeletingPage")]
        public ActionResult DeletePage(int schemeId, int pageId)
        {
            PageDTO result = QuestionnaireAdminBusinessManager.Instance.LoadPage(pageId);
            return View(result);
        }

        [HttpPost]
        [Route("Pages/DeletePage/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}")]
        public ActionResult DeletePage(PageDTO dto, int schemeId, int pageId)
        {
            QuestionnaireAdminBusinessManager.Instance.DeletePage(pageId);
            return RedirectToAction("PageDeleted");
        }

        [Route("Pages/PageDeleted/{" + RouteAttributeNames.SchemeID + ":int}", Order = 1, Name = "PageDeleted")]
        public ActionResult PageDeleted(int schemeId)
        {
            return View(schemeId);
        }

        [Route("Pages/DeletePageQuestion/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}/{pageQuestionId:int}", Order = 1, Name = "DeletingPageQuestion")]
        public ActionResult DeletePageQuestion(int schemeId, int pageId, int pageQuestionId)
        {
            PageQuestionForAdminDisplayDTO result = QuestionnaireAdminBusinessManager.Instance.LoadPageQuestionForAdminDisplay(pageQuestionId);
            return View(result);
        }

        [HttpPost]
        [Route("Pages/DeletePageQuestion/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}/{pageQuestionId:int}")]
        public ActionResult DeletePageQuestion(PageDTO dto, int schemeId, int pageId, int pageQuestionId)
        {
            QuestionnaireAdminBusinessManager.Instance.DeletePageQuestion(pageQuestionId);
            return RedirectToAction("PageQuestionDeleted");
        }

        [Route("Pages/PageQuestionDeleted/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}", Order = 1, Name = "PageQuestionDeleted")]
        public ActionResult PageQuestionDeleted(int schemeId, int pageId)
        {
            return View();
        }


        #endregion

        #region partial view actions

        /// <summary>
        /// Partial view action for the list pages kendo grid
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        [Route("Pages/ListPagesByScheme/{" + RouteAttributeNames.SchemeID + ":int}", Name = "Partial_ListingPagesByScheme", Order = 1)]
        public ActionResult ListPagesByScheme(int schemeId)
        {
            SchemePagesVM result = new SchemePagesVM()
            {
                Pages = null,
                SchemeID = schemeId
            };
            return View(result);
        }

        /// <summary>
        /// Json result for the pages kendo grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public JsonResult GetPagesByScheme([DataSourceRequest]DataSourceRequest request, int schemeId)
        {
            GridDataResponseDTO<PageForGridDTO> dataResponse = QuestionnaireAdminBusinessManager.Instance.ListPagesBySchemeForGrid(request, schemeId);

            return Json(new DataSourceResult
            {
                Data = dataResponse.GridRows,
                Total = dataResponse.ItemCount
            });
        }

        /// <summary>
        /// Partial view action for the list rendered kendo grid
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        [Route("Pages/ListRenderedPagesByScheme/{" + RouteAttributeNames.SchemeID + ":int}", Name = "Partial_ListingRenderdPagesByScheme", Order = 1)]
        public ActionResult ListRenderedPagesByScheme(int schemeId)
        {
            SchemeRenderedPagesVM result = new SchemeRenderedPagesVM()
            {
                Pages = null,
                SchemeID = schemeId
            };
            return View(result);
        }

        /// <summary>
        /// Json result for the rendered pages kendo grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public JsonResult GetRenderedPagesByScheme([DataSourceRequest]DataSourceRequest request, int schemeId)
        {
            GridDataResponseDTO<RenderedPageForGridDTO> dataResponse = QuestionnaireAdminBusinessManager.Instance.ListRenderedPagesBySchemeForGrid(request, schemeId);

            return Json(new DataSourceResult
            {
                Data = dataResponse.GridRows,
                Total = dataResponse.ItemCount
            });
        }

        /// <summary>
        /// Partial view action for the list page questions kendo grid
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        [Route("Pages/ListPageQuestionsByPage/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}", Name = "Partial_ListingPageQuestionsByPage", Order = 1)]
        public ActionResult ListPageQuestionsByPage(int schemeId, int pageId)
        {
            PagePageQuestionsVM result = new PagePageQuestionsVM()
            {
                PageID = pageId,
                SchemeID = schemeId,
                PageQuestions = null
            };
            return View(result);
        }

        /// <summary>
        /// Json result for the page questions kendo grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public JsonResult GetPageQuestionsByPage([DataSourceRequest]DataSourceRequest request, int pageId)
        {
            GridDataResponseDTO<PageQuestionForGridDTO> dataResponse = QuestionnaireAdminBusinessManager.Instance.ListPageQuestionsByPageForGrid(request, pageId);

            return Json(new DataSourceResult
            {
                Data = dataResponse.GridRows,
                Total = dataResponse.ItemCount
            });
        }

        /// <summary>
        /// Partial view action for the list page question validations kendo grid
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        [Route("Pages/ListPageQuestionValidationsByPageQuestion/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}/{pageQuestionId:int}", Name = "Partial_ListPageQuestionValidationsByPageQuestion", Order = 1)]
        public ActionResult ListPageQuestionValidationsByPageQuestion(int schemeId, int pageId, int pageQuestionId)
        {
            PageQuestionValidationVM result = new PageQuestionValidationVM()
            {
                PageID = pageId,
                SchemeID = schemeId,
                PageQuestionID = pageQuestionId,
                Validations = null
            };
            return View(result);
        }

        /// <summary>
        /// Json result for the page questions validations kendo grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public JsonResult GetPageQuestionValidationsByPageQuestion([DataSourceRequest]DataSourceRequest request, int pageQuestionId)
        {
            GridDataResponseDTO<PageQuestionValidationForGridDTO> dataResponse = QuestionnaireAdminBusinessManager.Instance.ListPageQuestionValidationsByPageQuestionForGrid(request, pageQuestionId);

            return Json(new DataSourceResult
            {
                Data = dataResponse.GridRows,
                Total = dataResponse.ItemCount
            });
        }

        /// <summary>
        /// Partial view action for the list page question display conditions kendo grid
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        [Route("Pages/ListPageQuestionDisplayConditionsByPageQuestion/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}/{pageQuestionId:int}", Name = "Partial_ListPageQuestionDisplayConditionsByPageQuestion", Order = 1)]
        public ActionResult ListPageQuestionDisplayConditionsByPageQuestion(int schemeId, int pageId, int pageQuestionId)
        {
            PageQuestionDisplayConditionVM result = new PageQuestionDisplayConditionVM()
            {
                PageID = pageId,
                SchemeID = schemeId,
                PageQuestionID = pageQuestionId,
                DisplayConditions = null
            };
            return View(result);
        }

        /// <summary>
        /// Json result for the page questions display conditions kendo grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public JsonResult GetPageQuestionDisplayConditionsByPageQuestion([DataSourceRequest]DataSourceRequest request, int pageQuestionId)
        {
            GridDataResponseDTO<PageQuestionConditionalDisplayForGridDTO> dataResponse = QuestionnaireAdminBusinessManager.Instance.ListPageQuestionConditionalDisplaysByPageQuestionForGrid(request, pageQuestionId);

            return Json(new DataSourceResult
            {
                Data = dataResponse.GridRows,
                Total = dataResponse.ItemCount
            });
        }

        #endregion

        #region pages

        [Route("Pages/ViewPage/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}", Name="ViewingPage", Order=1)]
        public ActionResult ViewPage(int schemeId, int pageId, PageTabs? selectedTab)
        {
            PageVM result = new PageVM()
            {
                Page = QuestionnaireAdminBusinessManager.Instance.LoadPage(pageId),
                PageID = pageId,
                SchemeID = schemeId,
                SelectedTab = PageTabs.PageDetails,
                Stats = QuestionnaireAdminBusinessManager.Instance.GetStatsForPage(pageId)
            };
            if (selectedTab != null)
            {
                result.SelectedTab = selectedTab.Value;
            }
            return View(result);
        }

        [Route("Pages/AddPage/{"+RouteAttributeNames.SchemeID+":int}", Name = "AddingPage", Order = 1)]
        public ActionResult AddPage(int schemeId)
        {
            AddPageManagementAdditionalViewData();
            return View(QuestionnaireAdminBusinessManager.Instance.NewPage(schemeId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pages/AddPage/{" + RouteAttributeNames.SchemeID + ":int}")]
        public ActionResult AddPage(int schemeId, PageDTO dto)
        {
            if (ModelState.IsValid)
            {
                dto = QuestionnaireAdminBusinessManager.Instance.SavePage(dto);
                return RedirectToAction("ViewPage", new { schemeId = schemeId, pageId = dto.PageID, selectedTab = PageTabs.PageDetails  });
            }
            else
            {
                AddPageManagementAdditionalViewData();
                return View(dto);
            }
        }

        [Route("Pages/EditPage/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}", Name = "EditingPage", Order = 1)]
        public ActionResult EditPage(int schemeId, int pageId)
        {
            PageDTO dto = QuestionnaireAdminBusinessManager.Instance.LoadPage(pageId);
            if (dto != null)
            {
                AddPageManagementAdditionalViewData();
                return View(dto);
            }
            else
            {
                throw new HttpException("Could not load Page id " + pageId.ToString());
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pages/EditPage/{" + RouteAttributeNames.SchemeID + ":int}/{pageId:int}")]
        public ActionResult EditPage(int schemeId, int pageId, PageDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireAdminBusinessManager.Instance.SavePage(dto);
                return RedirectToAction("ViewPage", new { schemeId = schemeId, pageId = dto.PageID, selectedTab = PageTabs.PageDetails });
            }
            else
            {
                AddPageManagementAdditionalViewData();
                return View(dto);
            }
        }

        private void AddPageManagementAdditionalViewData()
        {
            ViewBag.PageTemplates = new SelectList(QuestionnaireBusinessManager.Instance.ListPageTemplates(), "PageTemplateID", "Name");
            int? excludePageId = null;
            int schemeId = int.Parse(RouteData.Values["schemeId"].ToString());
            if (RouteData.Values["pageId"] != null)
            {
                excludePageId = int.Parse(RouteData.Values["pageId"].ToString());
            }
            ViewBag.Pages = new SelectList(QuestionnaireAdminBusinessManager.Instance.ListPagesBySchemeForNextPage(schemeId, excludePageId), "PageID", "Name");
        }

        #endregion

        #region page questions

        [Route("Pages/ViewPageQuestion/{schemeId:int}/{pageId:int}/{pageQuestionId:int}", Name = "ViewingPageQuestion", Order = 1)]
        public ActionResult ViewPageQuestion(int schemeId, int pageId, int pageQuestionId, PageQuestionTabs? selectedTab)
        {
            PageQuestionVM result = new PageQuestionVM()
            {
                PageQuestion = QuestionnaireAdminBusinessManager.Instance.LoadPageQuestionForAdminDisplay(pageQuestionId),
                PageID = pageId,
                SchemeID = schemeId,
                PageQuestionID = pageQuestionId,
                SelectedTab = PageQuestionTabs.PageQuestionDetails,
                Stats = QuestionnaireAdminBusinessManager.Instance.GetStatsForPageQuestion(pageQuestionId)
            };
            if (selectedTab.HasValue)
            {
                result.SelectedTab = selectedTab.Value;
            }
            return View(result);
        }

        [Route("Pages/AddPageQuestion/{schemeId:int}/{pageId:int}", Name = "AddingPageQuestions", Order = 1)]
        public ActionResult AddPageQuestion(int schemeId, int pageId)
        {
            AddAdditionPageQuestionViewData();
            return View(QuestionnaireAdminBusinessManager.Instance.NewPageQuestion(pageId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pages/AddPageQuestion/{schemeId:int}/{pageId:int}")]
        public ActionResult AddPageQuestion(int schemeId, int pageId, PageQuestionDTO dto)
        {
            if (ModelState.IsValid)
            {
                dto = QuestionnaireAdminBusinessManager.Instance.SavePageQuestion(dto);
                return RedirectToAction("ViewPageQuestion", new { schemeId = schemeId, pageId = pageId, pageQuestionId = dto.PageQuestionID, selectedTab = PageQuestionTabs.PageQuestionDetails });
            }
            else
            {
                AddAdditionPageQuestionViewData();
                return View(dto);
            }
        }

        [Route("Pages/EditPageQuestion/{schemeId:int}/{pageId:int}/{pageQuestionId:int}", Name = "EditingPageQuestions", Order = 1)]
        public ActionResult EditPageQuestion(int schemeId, int pageId, int pageQuestionId)
        {
            PageQuestionDTO dto = QuestionnaireAdminBusinessManager.Instance.LoadPageQuestion(pageQuestionId);
            if (dto != null)
            {
                AddAdditionPageQuestionViewData();
                return View(dto);
            }
            else
            {
                throw new HttpException("Could not load page question id " + pageQuestionId.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pages/EditPageQuestion/{schemeId:int}/{pageId:int}/{pageQuestionId:int}")]
        public ActionResult EditPageQuestion(int schemeId, int pageId, int pageQuestionId, PageQuestionDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireAdminBusinessManager.Instance.SavePageQuestion(dto);
                return RedirectToAction("ViewPage", new { schemeId = schemeId, pageId = pageId, selectedTab = PageTabs.PageQuestions });
            }
            else
            {
                AddAdditionPageQuestionViewData();
                return View(dto);
            }
        }

        private void AddAdditionPageQuestionViewData()
        {
            int schemeId = int.Parse(RouteData.Values["schemeId"].ToString());

            ViewBag.Pages = new SelectList(QuestionnaireBusinessManager.Instance.ListQuestionsForScheme(schemeId), "QuestionID", "Name");
        }

        #endregion

        #region page question validations

        [Route("Pages/AddPageQuestionValidation/{schemeId:int}/{pageId:int}/{pageQuestionId:int}", Name = "AddingPageQuestionValidation", Order = 1)]
        public ActionResult AddPageQuestionValidation(int schemeId, int pageId, int pageQuestionId)
        {
            return View(QuestionnaireAdminBusinessManager.Instance.NewPageQuestionValidation(pageQuestionId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pages/AddPageQuestionValidation/{schemeId:int}/{pageId:int}/{pageQuestionId:int}")]
        public ActionResult AddPageQuestionValidation(int schemeId, int pageId, int pageQuestionId, PageQuestionValidationDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireAdminBusinessManager.Instance.SavePageQuestionValidation(dto);
                return RedirectToAction("ViewPageQuestion", new { selectedTab = PageQuestionTabs.Validation });
            }
            else
            {
                return View(dto);
            }
        }

        [Route("Pages/EditPageQuestionValidation/{schemeId:int}/{pageId:int}/{pageQuestionId:int}/{pageQuestionValidationId:int}", Name = "EditingPageQuestionValidation", Order = 1)]
        public ActionResult EditPageQuestionValidation(int schemeId, int pageId, int pageQuestionId, int pageQuestionValidationId)
        {
            PageQuestionValidationDTO dto = QuestionnaireAdminBusinessManager.Instance.LoadPageQuestionValidation(pageQuestionValidationId);
            if (dto != null)
            {
                return View(dto);
            }
            else
            {
                throw new HttpException("Could not load page question validation id " + pageQuestionValidationId.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pages/EditPageQuestionValidation/{schemeId:int}/{pageId:int}/{pageQuestionId:int}/{pageQuestionValidationId:int}")]
        public ActionResult EditPageQuestionValidation(int schemeId, int pageId, int pageQuestionId, int pageQuestionValidationId, PageQuestionValidationDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireAdminBusinessManager.Instance.SavePageQuestionValidation(dto);
                return RedirectToAction("ViewPageQuestion", new { selectedTab = PageQuestionTabs.Validation });
            }
            else
            {
                return View(dto);
            }
        }

        public JsonResult DeletePageQuestionValidation(int pageQuestionValidationId)
        {
            QuestionnaireAdminBusinessManager.Instance.DeletePageQuestionValidation(pageQuestionValidationId);
            return new JsonResult()
            {
                Data = "Page Question Validation deleted",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region page question conditional displays

        [Route("Pages/AddPageQuestionConditionalDisplay/{schemeId:int}/{pageId:int}/{pageQuestionId:int}", Name = "AddingPageQuestionConditionalDisplay", Order = 1)]
        public ActionResult AddPageQuestionConditionalDisplay(int schemeId, int pageId, int pageQuestionId)
        {
            AddSourceQuestionsForConditionalDisplay();
            AddSourceQuestionPossibleAnswersForConditionalDisplay();
            return View(QuestionnaireBusinessManager.Instance.NewPageQuestionConditionalDisplay(pageQuestionId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pages/AddPageQuestionConditionalDisplay/{schemeId:int}/{pageId:int}/{pageQuestionId:int}")]
        public ActionResult AddPageQuestionConditionalDisplay(int schemeId, int pageId, int pageQuestionId, PageQuestionConditionalDisplayDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireAdminBusinessManager.Instance.SavePageQuestionConditionalDisplay(dto);
                return RedirectToAction("ViewPageQuestion", new { selectedTab = PageQuestionTabs.DisplayConditions });
            }
            else
            {
                AddSourceQuestionsForConditionalDisplay();
                AddSourceQuestionPossibleAnswersForConditionalDisplay();
                return View(dto);
            }
        }

        [Route("Pages/EditPageQuestionConditionalDisplay/{schemeId:int}/{pageId:int}/{pageQuestionId:int}/{pageQuestionConditionalDisplayId:int}", Name = "EditingPageQuestionConditionalDisplay", Order = 1)]
        public ActionResult EditPageQuestionConditionalDisplay(int schemeId, int pageId, int pageQuestionId, int pageQuestionConditionalDisplayId)
        {
            PageQuestionConditionalDisplayDTO dto = QuestionnaireAdminBusinessManager.Instance.LoadPageQuestionConditionalDisplay(pageQuestionConditionalDisplayId);
            if (dto != null)
            {
                AddSourceQuestionsForConditionalDisplay();
                AddSourceQuestionPossibleAnswersForConditionalDisplay();
                return View(dto);
            }
            else
            {
                throw new HttpException("Could not load page question conditional display id " + pageQuestionConditionalDisplayId.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pages/EditPageQuestionConditionalDisplay/{schemeId:int}/{pageId:int}/{pageQuestionId:int}/{pageQuestionConditionalDisplayId:int}")]
        public ActionResult EditPageQuestionConditionalDisplay(int schemeId, int pageId, int pageQuestionId, int pageQuestionConditionalDisplayId, PageQuestionConditionalDisplayDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireAdminBusinessManager.Instance.SavePageQuestionConditionalDisplay(dto);
                return RedirectToAction("ViewPageQuestion", new { selectedTab = PageQuestionTabs.DisplayConditions });
            }
            else
            {
                AddSourceQuestionsForConditionalDisplay();
                AddSourceQuestionPossibleAnswersForConditionalDisplay();
                return View(dto);
            }
        }

        public JsonResult DeletePageQuestionConditionalDisplay(int pageQuestionConditionalDisplayId)
        {
            QuestionnaireAdminBusinessManager.Instance.DeletePageQuestionDisplayCondition(pageQuestionConditionalDisplayId);
            return new JsonResult()
            {
                Data = "Page Question Conditional Dispaly deleted",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private void AddSourceQuestionsForConditionalDisplay()
        {
            int pageId = Convert.ToInt32(RouteData.Values["pageId"]);
            int targetPageQuestionId = -1;
            if (RouteData.Values["pageQuestionId"] != null)
            {
                targetPageQuestionId = Convert.ToInt32(RouteData.Values["pageQuestionId"]);
            }
            ViewBag.SourceQuestions = new SelectList(QuestionnaireBusinessManager.Instance.ListPageQuestionsByPageWithPossibleAnswers(pageId, targetPageQuestionId), "PageQuestionID", "QuestionText");

        }

        private void AddSourceQuestionPossibleAnswersForConditionalDisplay()
        {
            int pageId = Convert.ToInt32(RouteData.Values["pageId"]);
            int targetPageQuestionId = -1;
            if (RouteData.Values["pageQuestionId"] != null)
            {
                targetPageQuestionId = Convert.ToInt32(RouteData.Values["pageQuestionId"]);
            }
            List<PageQuestionDTO> pageQuestionDTOs = QuestionnaireBusinessManager.Instance.ListPageQuestionsByPageWithPossibleAnswers(pageId, targetPageQuestionId);
            List<SelectListItem> selectItems = new List<SelectListItem>();
            List<QuestionPossibleAnswerDTO> answerDTOs = null;
            foreach (PageQuestionDTO pageQuestionDTO in pageQuestionDTOs)
            {
                answerDTOs = QuestionnaireBusinessManager.Instance.ListPossibleAnswersForQuestion(pageQuestionDTO.QuestionID);
                foreach(QuestionPossibleAnswerDTO answerDTO in answerDTOs)
                {
                    selectItems.Add(new SelectListItem() { Value = answerDTO.QuestionPossibleAnswerID.ToString(), Text = pageQuestionDTO.QuestionName + " - " + answerDTO.AnswerText });
                }
            }
            ViewBag.TriggerAnswers = selectItems;
        }

        #endregion

        #region rendered pages

        [Route("Pages/RenderPages/{schemeId:int}", Name="RenderingPages", Order=1)]
        public ActionResult RenderPages(int schemeId)
        {
            //command to trigger rendering of pages
            //& create the .cshtml files to be inserted into DynamicViewsFolder (web.config setting)
            //will use the razor view engine to generate the .cshtml files (out putting razor content)
            QuestionnaireRendering.RenderPagesForScheme(schemeId);
            return RedirectToAction("Scheme", "Schemes", new { schemeId = schemeId, selectedTab = SchemePageTabs.RenderedPages });
        }

        #endregion

	}
}