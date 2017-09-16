using InsuranceEngine.Business.BusinessManagers.Utility;
using InsuranceEngine.Data.DataManagers.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Utility.GridData;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceEngine.Business.BusinessManagers.Questionnaire
{
    public class QuestionnaireAdminBusinessManager : BusinessManagerBase<QuestionnaireAdminBusinessManager>
    {
        public TreeNodeDTO GetTreeStructureForScheme(int schemeId)
        {
            return QuestionnaireAdminDataManager.Instance.GetTreeStructureForScheme(schemeId);
        }

        #region delete

        public void DeleteScheme(int schemeId)
        {
            QuestionnaireAdminDataManager.Instance.DeleteScheme(schemeId);
        }

        public void DeleteQuestion(int questionId)
        {
            QuestionnaireAdminDataManager.Instance.DeleteQuestion(questionId);
        }

        public void DeletePossibleAnswer(int possibleAnswerId)
        {
            QuestionnaireAdminDataManager.Instance.DeletePossibleAnswer(possibleAnswerId);
        }

        public void DeletePage(int pageId)
        {
            QuestionnaireAdminDataManager.Instance.DeletePage(pageId);
        }

        public void DeleteRenderedPage(int renderedPageId)
        {
            QuestionnaireAdminDataManager.Instance.DeleteRenderedPage(renderedPageId);
        }

        public void DeletePageQuestion(int pageQuestionId)
        {
            QuestionnaireAdminDataManager.Instance.DeletePageQuestion(pageQuestionId);
        }

        public void DeletePageQuestionDisplayCondition(int pageQuestionDisplayConditionId)
        {
            QuestionnaireAdminDataManager.Instance.DeletePageQuestionDisplayCondition(pageQuestionDisplayConditionId);
        }

        public void DeletePageQuestionValidation(int pageQuestionValidationId)
        {
            QuestionnaireAdminDataManager.Instance.DeletePageQuestionValidation(pageQuestionValidationId);
        }

        #endregion

        #region rendered pages

        public GridDataResponseDTO<RenderedPageForGridDTO> ListRenderedPagesBySchemeForGrid(DataSourceRequest telerikRequestData, int schemeId)
        {
            GridDataRequestDTO requestDTO = GridHelper.Instance.ConvertTelerikDataSourceRequestToGridDataRequestDTO(telerikRequestData, true);
            return QuestionnaireAdminDataManager.Instance.ListRenderedPagesBySchemeForGrid(requestDTO, schemeId);
        }

        #endregion

        #region schemes

        public List<SchemeDTO> ListSchemes()
        {
            return QuestionnaireAdminDataManager.Instance.ListSchemes();
        }

        public GridDataResponseDTO<SchemeForGridDTO> ListSchemes(DataSourceRequest telerikRequestData)
        {
            GridDataRequestDTO requestDTO = GridHelper.Instance.ConvertTelerikDataSourceRequestToGridDataRequestDTO(telerikRequestData, true);
            return QuestionnaireAdminDataManager.Instance.ListSchemesForGrid(requestDTO);
        }

        public ManageSchemeStatsDTO GetStatsForScheme(int schemeId)
        {
            return QuestionnaireAdminDataManager.Instance.GetStatsForScheme(schemeId);
        }

        public SchemeDTO LoadScheme(int schemeId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadScheme(schemeId);
        }
        public SchemeDTO LoadScheme(string schemeCode)
        {
            return QuestionnaireAdminDataManager.Instance.LoadScheme(schemeCode);
        }

        public SchemeDTO SaveScheme(SchemeDTO dto)
        {
            return QuestionnaireAdminDataManager.Instance.SaveScheme(dto);
        }

        #endregion

        #region pages

        public GridDataResponseDTO<PageForGridDTO> ListPagesBySchemeForGrid(DataSourceRequest telerikRequestData, int schemeId)
        {
            GridDataRequestDTO requestDTO = GridHelper.Instance.ConvertTelerikDataSourceRequestToGridDataRequestDTO(telerikRequestData, true);
            return QuestionnaireAdminDataManager.Instance.ListPagesBySchemeForGrid(requestDTO, schemeId);
        }

        public List<PageDTO> ListPagesByScheme(int schemeId)
        {
            return QuestionnaireAdminDataManager.Instance.ListPagesByScheme(schemeId);
        }

        public List<PageDTO> ListPagesBySchemeForNextPage(int schemeId, int? excludePageId)
        {
            List<PageDTO> result = ListPagesByScheme(schemeId);
            if (excludePageId.HasValue)
            {
                result = result.Where(p => p.PageID != excludePageId.Value).ToList();
            }
            return result;
        }

        public PageDTO LoadPage(int pageId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadPage(pageId);
        }

        public ManagePageStatsDTO GetStatsForPage(int pageId)
        {
            return QuestionnaireAdminDataManager.Instance.GetStatsForPage(pageId);
        }

        public PageDTO SavePage(PageDTO dto)
        {
            return QuestionnaireAdminDataManager.Instance.SavePage(dto);
        }

        public PageDTO NewPage(int schemeId)
        {
            return new PageDTO() { SchemeID = schemeId, DisplayOrder = QuestionnaireAdminDataManager.Instance.GetPageCountForScheme(schemeId) + 1 };
        }

        #endregion

        #region page questions

        public GridDataResponseDTO<PageQuestionForGridDTO> ListPageQuestionsByPageForGrid(DataSourceRequest telerikRequestData, int pageId)
        {
            GridDataRequestDTO requestDTO = GridHelper.Instance.ConvertTelerikDataSourceRequestToGridDataRequestDTO(telerikRequestData, true);
            return QuestionnaireAdminDataManager.Instance.ListPageQuestionsByPageForGrid(requestDTO, pageId);
        }

        public PageQuestionForAdminDisplayDTO LoadPageQuestionForAdminDisplay(int pageQuestionId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadPageQuestionForAdminDisplay(pageQuestionId);
        }

        public ManagePageQuestionStats GetStatsForPageQuestion(int pageQuestionId)
        {
            return QuestionnaireAdminDataManager.Instance.GetStatsForPageQuestion(pageQuestionId);
        }

        public PageQuestionDTO LoadPageQuestion(int pageQuestionId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadPageQuestion(pageQuestionId);
        }

        public PageQuestionDTO SavePageQuestion(PageQuestionDTO dto)
        {
            return QuestionnaireAdminDataManager.Instance.SavePageQuestion(dto);
        }

        public PageQuestionDTO NewPageQuestion(int pageId)
        {
            return new PageQuestionDTO() { PageID = pageId, DefaultShow = true, DisplayOrder = QuestionnaireDataManager.Instance.GetPageQuestionCountForPage(pageId) + 1 };
        }

        #endregion

        #region page question validation

        public GridDataResponseDTO<PageQuestionValidationForGridDTO> ListPageQuestionValidationsByPageQuestionForGrid(DataSourceRequest telerikRequestData, int pageQuestionId)
        {
            GridDataRequestDTO requestDTO = GridHelper.Instance.ConvertTelerikDataSourceRequestToGridDataRequestDTO(telerikRequestData, true);
            return QuestionnaireAdminDataManager.Instance.ListPageQuestionValidationsByPageQuestionForGrid(requestDTO, pageQuestionId);
        }

        public PageQuestionValidationDTO LoadPageQuestionValidation(int pageQuestionValidationId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadPageQuestionValidation(pageQuestionValidationId);
        }

        public PageQuestionValidationDTO SavePageQuestionValidation(PageQuestionValidationDTO dto)
        {
            return QuestionnaireAdminDataManager.Instance.SavePageQuestionValidation(dto);
        }

        public PageQuestionValidationDTO NewPageQuestionValidation(int pageQuestionId)
        {
            return new PageQuestionValidationDTO()
            {
                PageQuestionID = pageQuestionId,
                ErrorMessage = "Please complete the $$QuestionName$$ field"                
            };
        }

        #endregion

        #region page question conditional display

        public GridDataResponseDTO<PageQuestionConditionalDisplayForGridDTO> ListPageQuestionConditionalDisplaysByPageQuestionForGrid(DataSourceRequest telerikRequestData, int pageQuestionId)
        { 
            GridDataRequestDTO requestDTO = GridHelper.Instance.ConvertTelerikDataSourceRequestToGridDataRequestDTO(telerikRequestData, true);
            return QuestionnaireAdminDataManager.Instance.ListPageQuestionConditionalDisplaysByPageQuestionForGrid(requestDTO, pageQuestionId);
        }

        public PageQuestionConditionalDisplayDTO LoadPageQuestionConditionalDisplay(int pageQuestionConditionalDisplayId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadPageQuestionConditionalDisplay(pageQuestionConditionalDisplayId);
        }

        public PageQuestionConditionalDisplayDTO SavePageQuestionConditionalDisplay(PageQuestionConditionalDisplayDTO dto)
        {
            return QuestionnaireAdminDataManager.Instance.SavePageQuestionConditionalDisplay(dto);
        }

        #endregion

        #region possible answers

        public GridDataResponseDTO<PossibleAnswerForGridDTO> ListPossibleAnswersByQuestionForGrid(DataSourceRequest telerikRequestData, int questionId)
        {
            GridDataRequestDTO requestDTO = GridHelper.Instance.ConvertTelerikDataSourceRequestToGridDataRequestDTO(telerikRequestData, true);
            return QuestionnaireAdminDataManager.Instance.ListPossibleAnswersByQuestionForGrid(requestDTO, questionId);
        }

        public QuestionPossibleAnswerDTO LoadPossibleAnswer(int possibleAnswerId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadPossibleAnswer(possibleAnswerId);
        }

        public QuestionPossibleAnswerDTO SavePossibleAnswer(QuestionPossibleAnswerDTO dto)
        {
            return QuestionnaireAdminDataManager.Instance.SavePossibleAnswer(dto);
        }

        public bool PossibleAnswerValueInUseForQuestion(QuestionPossibleAnswerDTO dto)
        {
            return QuestionnaireAdminDataManager.Instance.PossibleAnswerValueInUseForQuestion(dto.QuestionID, dto.AnswerValue, dto.QuestionPossibleAnswerID);
        }

        public QuestionPossibleAnswerDTO NewPossibleAnswer(int questionId)
        {
            return new QuestionPossibleAnswerDTO()
            {
                QuestionID = questionId,
                DisplayOrder = QuestionnaireAdminDataManager.Instance.GetQuestionPossibleAnswerCountForQuestion(questionId) + 1
            };
        }

        #endregion

        #region questions

        public GridDataResponseDTO<QuestionForGridDTO> ListQuestionsBySchemeForGrid(DataSourceRequest telerikRequestData, int schemeId)
        {
            GridDataRequestDTO requestDTO = GridHelper.Instance.ConvertTelerikDataSourceRequestToGridDataRequestDTO(telerikRequestData, true);
            return QuestionnaireAdminDataManager.Instance.ListQuestionsBySchemeForGrid(requestDTO, schemeId);
        }

        public QuestionDTO LoadQuestion(int questionId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadQuestion(questionId);
        }        

        public ManageQuestionStatsDTO GetStatsForQuestion(int questionId)
        {
            return QuestionnaireAdminDataManager.Instance.GetStatsForQuestion(questionId);
        }

        public QuestionForAdminDisplayDTO LoadQuestionForAdminDisplay(int questionId)
        {
            return QuestionnaireAdminDataManager.Instance.LoadQuestionForAdminDisplay(questionId);
        }

        public QuestionDTO SaveQuestion(QuestionDTO dto)
        {
            return QuestionnaireAdminDataManager.Instance.SaveQuestion(dto);
        }

        public QuestionDTO NewQuestion(int schemeId)
        {
            return new QuestionDTO() { SchemeID = schemeId };
        }

        #endregion

    }
}
