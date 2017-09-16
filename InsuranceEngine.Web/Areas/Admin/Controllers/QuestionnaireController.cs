using InsuranceEngine.Business.BusinessManagers.Questionnaire;
using InsuranceEngine.DTO.AttributeNames;
using InsuranceEngine.DTO.Questionnaire;
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
    public class QuestionnaireController : Controller
    {

        #region partial view actions

        /// <summary>
        /// Partial view action for the list question kendo grid
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        [Route("Questionnaire/ListQuestionsByScheme/{" + RouteAttributeNames.SchemeID + ":int}", Name = "Partial_ListingQuestionsByScheme", Order = 1)]
        public ActionResult ListQuestionsByScheme(int schemeId)
        {
            SchemeQuestionsVM result = new SchemeQuestionsVM()
            {
                Questions = null,
                SchemeID = schemeId
            };
            return View(result);
        }

        /// <summary>
        /// Json result for the question kendo grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public JsonResult GetQuestionsByScheme([DataSourceRequest]DataSourceRequest request, int schemeId)
        {
            GridDataResponseDTO<QuestionForGridDTO> dataResponse = QuestionnaireAdminBusinessManager.Instance.ListQuestionsBySchemeForGrid(request, schemeId);

            return Json(new DataSourceResult
            {
                Data = dataResponse.GridRows,
                Total = dataResponse.ItemCount
            });
        }

        /// <summary>
        /// Partial view action for the list possible answers kendo grid
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        [Route("Questionnaire/ListPossibleAnswersByQuestion/{" + RouteAttributeNames.SchemeID + ":int}/{questionId:int}", Name = "Partial_ListPossibleAnswersByQuestion", Order = 1)]
        public ActionResult ListPossibleAnswersByQuestion(int schemeId, int questionId)
        {
            QuestionPossibleAnswersVM result = new QuestionPossibleAnswersVM()
            {
                PossibleAnswers = null,
                SchemeID = schemeId,
                QuestionID = questionId
            };
            return View(result);
        }

        /// <summary>
        /// Json result for the possible answer kendo grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public JsonResult GetPossibleAnswersByQuestion([DataSourceRequest]DataSourceRequest request, int questionId)
        {
            GridDataResponseDTO<PossibleAnswerForGridDTO> dataResponse = QuestionnaireAdminBusinessManager.Instance.ListPossibleAnswersByQuestionForGrid(request, questionId);

            return Json(new DataSourceResult
            {
                Data = dataResponse.GridRows,
                Total = dataResponse.ItemCount
            });
        }

        #endregion

        #region delete

        [Route("Questionnaire/DeleteQuestion/{" + RouteAttributeNames.SchemeID + ":int}/{questionId:int}", Order = 1, Name = "DeletingQuestion")]
        public ActionResult DeleteQuestion(int schemeId, int questionId)
        {
            QuestionForAdminDisplayDTO result = QuestionnaireAdminBusinessManager.Instance.LoadQuestionForAdminDisplay(questionId);
            return View(result);
        }

        [HttpPost]
        [Route("Questionnaire/DeleteQuestion/{" + RouteAttributeNames.SchemeID + ":int}/{questionId:int}")]
        public ActionResult DeleteQuestion(PageDTO dto, int schemeId, int questionId)
        {
            QuestionnaireAdminBusinessManager.Instance.DeleteQuestion(questionId);
            return RedirectToAction("QuestionDeleted");
        }

        [Route("Questionnaire/QuestionDeleted/{" + RouteAttributeNames.SchemeID + ":int}/{questionId:int}", Order = 1, Name = "QuestionDeleted")]
        public ActionResult QuestionDeleted(int schemeId, int questionId)
        {
            return View();
        }

        [Route("Questionnaire/DeletePossibleAnswer/{" + RouteAttributeNames.SchemeID + ":int}/{questionId:int}/{possibleAnswerId:int}", Order = 1, Name = "DeletingPossibleAnswer")]
        public ActionResult DeletePossibleAnswer(int schemeId, int questionId, int possibleAnswerId)
        {
            QuestionPossibleAnswerDTO result = QuestionnaireAdminBusinessManager.Instance.LoadPossibleAnswer(possibleAnswerId);
            return View(result);
        }

        [HttpPost]
        [Route("Questionnaire/DeletePossibleAnswer/{" + RouteAttributeNames.SchemeID + ":int}/{questionId:int}/{possibleAnswerId:int}")]
        public ActionResult DeletePossibleAnswer(PageDTO dto, int schemeId, int questionId, int possibleAnswerId)
        {
            QuestionnaireAdminBusinessManager.Instance.DeletePossibleAnswer(possibleAnswerId);
            return RedirectToAction("PossibleAnswerDeleted");
        }

        [Route("Questionnaire/PossibleAnswerDeleted/{" + RouteAttributeNames.SchemeID + ":int}/{questionId:int}", Order = 1, Name = "PossibleAnswerDeleted")]
        public ActionResult PossibleAnswerDeleted(int schemeId, int questionId)
        {
            return View();
        }        

        #endregion

        #region question

        [Route("Questionnaire/AddQuestion/{schemeId:int}", Name = "AddingQuestion", Order = 1)]
        public ActionResult AddQuestion(int schemeId)
        {
            AddAdditionalQuestionViewData();
            return View(QuestionnaireAdminBusinessManager.Instance.NewQuestion(schemeId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Questionnaire/AddQuestion/{schemeId:int}")]
        public ActionResult AddQuestion(int schemeId, QuestionDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionDTO existingQuestion = QuestionnaireBusinessManager.Instance.LoadQuestion(dto.Code);
                if (existingQuestion != null && existingQuestion.QuestionID != dto.QuestionID)
                {
                    //duplicate code
                    ModelState.AddModelError("Code", "Code already in use, please enter a different code");
                    AddAdditionalQuestionViewData();
                    return View(dto);
                }
                else
                {
                    dto = QuestionnaireAdminBusinessManager.Instance.SaveQuestion(dto);
                    return RedirectToAction("ViewQuestion", new { schemeId = schemeId, questionId = dto.QuestionID });
                }
            }
            else
            {
                AddAdditionalQuestionViewData();
                return View(dto);
            }
        }

        [Route("Questionnaire/EditQuestion/{schemeId:int}/{questionId:int}", Name = "EditingQuestion", Order = 1)]
        public ActionResult EditQuestion(int schemeId, int questionId)
        {
            QuestionDTO dto = QuestionnaireAdminBusinessManager.Instance.LoadQuestion(questionId);
            if (dto != null)
            {
                AddAdditionalQuestionViewData();
                return View(dto);
            }
            else
            {
                throw new HttpException("Unable to load question id " + questionId.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Questionnaire/EditQuestion/{schemeId:int}/{questionId:int}")]
        public ActionResult EditQuestion(int schemeId, QuestionDTO dto)
        {
            if (ModelState.IsValid)
            {
                QuestionDTO existingQuestion = QuestionnaireBusinessManager.Instance.LoadQuestion(dto.Code);
                if (existingQuestion != null && existingQuestion.QuestionID != dto.QuestionID)
                {
                    //duplicate code
                    ModelState.AddModelError("Code", "Code already in use, please enter a different code");
                    AddAdditionalQuestionViewData();
                    return View(dto);
                }
                else
                {
                    QuestionnaireAdminBusinessManager.Instance.SaveQuestion(dto);
                    return RedirectToAction("ViewQuestion", new { schemeId = schemeId, questionId = dto.QuestionID });
                }
            }
            else
            {
                AddAdditionalQuestionViewData();
                return View(dto);
            }
        }

        [Route("Questionnaire/ViewQuestion/{schemeId:int}/{questionId:int}", Name="ViewQuestion", Order= 1)]
        public ActionResult ViewQuestion(int schemeId, int questionId, QuestionTabs? selectedTab)
        {
            QuestionVM result = new QuestionVM()
            {
                Question = QuestionnaireAdminBusinessManager.Instance.LoadQuestionForAdminDisplay(questionId),
                QuestionID = questionId,
                SchemeID = schemeId,
                SelectedTab = QuestionTabs.QuestionDetails,
                Stats = QuestionnaireAdminBusinessManager.Instance.GetStatsForQuestion(questionId)
            };
            if (selectedTab.HasValue)
            {
                result.SelectedTab = selectedTab.Value;
            }
            return View(result);
        }

        private void AddAdditionalQuestionViewData()
        {
            ViewBag.QuestionTemplates = new SelectList(QuestionnaireBusinessManager.Instance.ListQuestionTemplates(), "QuestionTemplateID", "Name");
        }

        #endregion

        #region possible answer

        [Route("Questionnaire/AddPossibleAnswer/{schemeId:int}/{questionId:int}", Name = "AddingPossibleAnswer", Order = 1)]
        public ActionResult AddPossibleAnswer(int schemeId, int questionId)
        {
            return View(QuestionnaireAdminBusinessManager.Instance.NewPossibleAnswer(questionId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Questionnaire/AddPossibleAnswer/{schemeId:int}/{questionId:int}")]
        public ActionResult AddPossibleAnswer(int schemeId, int questionId, QuestionPossibleAnswerDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (QuestionnaireAdminBusinessManager.Instance.PossibleAnswerValueInUseForQuestion(dto))
                {
                    ModelState.AddModelError("AnswerValue", "The Answer Value is already in use for this question, please enter a different one");
                    return View(dto);
                }
                else
                {
                    QuestionnaireAdminBusinessManager.Instance.SavePossibleAnswer(dto);
                    return RedirectToAction("ViewQuestion", new { schemeId = schemeId, questionId = dto.QuestionID, selectedTab = QuestionTabs.PossibleAnswers });
                }
            }
            else
            {
                return View(dto);
            }
        }

        [Route("Questionnaire/EditPossibleAnswer/{schemeId:int}/{questionId:int}/{possibleAnswerId:int}", Name = "EditingPossibleAnswer", Order = 1)]
        public ActionResult EditPossibleAnswer(int schemeId, int questionId, int possibleAnswerId)
        {
            QuestionPossibleAnswerDTO dto = QuestionnaireAdminBusinessManager.Instance.LoadPossibleAnswer(possibleAnswerId);
            if (dto != null)
            {
                return View(dto);
            }
            else
            {
                throw new HttpException("Unable to load possible answer id " + possibleAnswerId.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Questionnaire/EditPossibleAnswer/{schemeId:int}/{questionId:int}/{possibleAnswerId:int}")]
        public ActionResult EditPossibleAnswer(int schemeId, int possibleAnswerId, QuestionPossibleAnswerDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (QuestionnaireAdminBusinessManager.Instance.PossibleAnswerValueInUseForQuestion(dto))
                {
                    ModelState.AddModelError("AnswerValue", "The Answer Value is already in use for this question, please enter a different one");
                    return View(dto);
                }
                else
                {
                    QuestionnaireAdminBusinessManager.Instance.SavePossibleAnswer(dto);
                    return RedirectToAction("ViewQuestion", new { schemeId = schemeId, questionId = dto.QuestionID, selectedTab = QuestionTabs.PossibleAnswers });
                }
            }
            else
            {
                return View(dto);            
            }
        }

        #endregion        

    }
}