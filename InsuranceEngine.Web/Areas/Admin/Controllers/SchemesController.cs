using InsuranceEngine.Business.BusinessManagers.Questionnaire;
using InsuranceEngine.Business.Contexts;
using InsuranceEngine.DTO.AttributeNames;
using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Utility.Enums.AdminUIEnums;
using InsuranceEngine.DTO.Utility.GridData;
using InsuranceEngine.Web.Areas.Admin.Models;
using Kendo.Mvc.UI;
using System.Web;
using System.Web.Mvc;

namespace InsuranceEngine.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class SchemesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListSchemes([DataSourceRequest]DataSourceRequest request)
        {
            GridDataResponseDTO<SchemeForGridDTO> dataResponse = QuestionnaireAdminBusinessManager.Instance.ListSchemes(request);

            return Json(new DataSourceResult
            {
                Data = dataResponse.GridRows,
                Total = dataResponse.ItemCount
            });
        }

        public ActionResult ClearCache()
        {
            WebContext.Instance.ClearCache();
            return RedirectToAction("Index");
        }

        public ActionResult AddScheme()
        {
            return View(new SchemeDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddScheme(SchemeDTO dto)
        {
            if (ModelState.IsValid)
            {
                SchemeDTO existingSchemeDTO = QuestionnaireAdminBusinessManager.Instance.LoadScheme(dto.Code);
                if (existingSchemeDTO != null && existingSchemeDTO.SchemeID != dto.SchemeID)
                {
                    //duplicate scheme code
                    ModelState.AddModelError("Code", "Code already in use, please enter a different code");
                    return View(dto);
                }
                else
                {
                    QuestionnaireAdminBusinessManager.Instance.SaveScheme(dto);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(dto);
            }
        }
        
        [Route("Schemes/EditScheme/{"+RouteAttributeNames.SchemeID+":int}", Name="EditingScheme", Order=1)]
        public ActionResult EditScheme(int schemeId)
        {
            SchemeDTO dto = QuestionnaireAdminBusinessManager.Instance.LoadScheme(schemeId);
            if (dto != null)
            {
                return View(dto);
            }
            else
            {
                throw new HttpException("Could not load scheme id " + schemeId.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Schemes/EditScheme/{" + RouteAttributeNames.SchemeID + ":int}")]
        public ActionResult EditScheme(SchemeDTO dto, int schemeId)
        {
            if (ModelState.IsValid)
            {
                SchemeDTO existingSchemeDTO = QuestionnaireAdminBusinessManager.Instance.LoadScheme(dto.Code);
                if (existingSchemeDTO != null && existingSchemeDTO.SchemeID != dto.SchemeID)
                {
                    //duplicate scheme code
                    ModelState.AddModelError("Code", "Code already in use, please enter a different code");
                    return View(dto);
                }
                else
                {
                    QuestionnaireAdminBusinessManager.Instance.SaveScheme(dto);
                    return RedirectToAction("Scheme", new { schemeId = schemeId });
                }
            }
            else
            {
                return View(dto);
            }
        }

        [Route("Schemes/Scheme/{"+RouteAttributeNames.SchemeID+":int}", Order = 1, Name = "ViewingScheme")]
        public ActionResult Scheme(int schemeId, SchemePageTabs? selectedTab)
        {
            SchemeVM schemeVM = new SchemeVM()
            {
                Scheme = QuestionnaireAdminBusinessManager.Instance.LoadScheme(schemeId),
                Stats = QuestionnaireAdminBusinessManager.Instance.GetStatsForScheme(schemeId),
                SelectedTab = SchemePageTabs.SchemeDetails
            };

            if (selectedTab != null)
            {
                schemeVM.SelectedTab = selectedTab.Value;
            }
            return View(schemeVM);
        }

        [Route("Schemes/DeleteScheme/{" + RouteAttributeNames.SchemeID + ":int}", Order = 1, Name = "DeletingScheme")]
        public ActionResult DeleteScheme(int schemeId)
        {
            return View(QuestionnaireAdminBusinessManager.Instance.LoadScheme(schemeId));
        }

        [HttpPost]
        [Route("Schemes/DeleteScheme/{" + RouteAttributeNames.SchemeID + ":int}")]
        public ActionResult DeleteScheme(int schemeId, SchemeDTO dto)
        {
            QuestionnaireAdminBusinessManager.Instance.DeleteScheme(schemeId);
            return RedirectToAction("SchemeDeleted");
        }

        public ActionResult SchemeDeleted()
        {
            return View();
        }

	}
}