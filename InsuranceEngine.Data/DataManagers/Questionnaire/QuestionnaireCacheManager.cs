using InsuranceEngine.Caching;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.DTO.Questionnaire.Admin;
using System.Collections.Generic;

namespace InsuranceEngine.Data.DataManagers.Questionnaire
{
    internal class QuestionnaireCacheManager : DataManagerBase<QuestionnaireCacheManager>
    {

        private OutofProcessAccess CacheAccess
        {
            get
            {
                return OutofProcessAccess.Instance;
            }
        }

        private const string SubsystemKey = "QuestionnaireDataManager";

        #region per method caching

        #region scheme

        private const string ListSchemesKey = "ListSchemes";
        public List<SchemeDTO> ListSchemesFromCache()
        {
            string key = SubsystemKey + ListSchemesKey;
            return CacheAccess.GetApplicationSpecificCacheItem<List<SchemeDTO>>(SubsystemKey, key);
        }
        public void InsertlistSchemesIntoCache(List<SchemeDTO> dtos)
        {
            string key = SubsystemKey + ListSchemesKey;
            CacheAccess.InsertApplicationSpecificCacheItem<List<SchemeDTO>>(SubsystemKey, key, dtos);
        }
        public void ClearSchemesFromCache()
        {
            string key = SubsystemKey + ListSchemesKey;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        private const string GetTreeStructureBySchemeKey = "GetTreeStructureByScheme";
        public TreeNodeDTO GetTreeStructureBySchemeFromCache(int schemeId)
        {
            string key = SubsystemKey + GetTreeStructureBySchemeKey + schemeId;
            return CacheAccess.GetApplicationSpecificCacheItem<TreeNodeDTO>(SubsystemKey, key);
        }
        public void InsertTreeStructureBySchemeIntoCache(int schemeId, TreeNodeDTO dto)
        {
            string key = SubsystemKey + GetTreeStructureBySchemeKey + schemeId;
            CacheAccess.InsertApplicationSpecificCacheItem<TreeNodeDTO>(SubsystemKey, key, dto);
        }
        public void ClearTreeStructureBySchemeFromCache(int schemeId)
        {
            string key = SubsystemKey + GetTreeStructureBySchemeKey + schemeId;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        private const string GetStatsForSchemeKey = "GetStatsForScheme";
        public ManageSchemeStatsDTO GetStatsForSchemeFromCache(int schemeId)
        {
            string key = SubsystemKey + GetStatsForSchemeKey + schemeId;
            return CacheAccess.GetApplicationSpecificCacheItem<ManageSchemeStatsDTO>(SubsystemKey, key);
        }
        public void InsertStatsForSchemeIntoCache(int schemeId, ManageSchemeStatsDTO dto)
        {
            string key = SubsystemKey + GetStatsForSchemeKey + schemeId;
            CacheAccess.InsertApplicationSpecificCacheItem<ManageSchemeStatsDTO>(SubsystemKey, key, dto);
        }
        public void ClearStatsForSchemeFromCache(int schemeId)
        {
            string key = SubsystemKey + GetStatsForSchemeKey + schemeId;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        private const string GetStatsForPageKey = "GetStatsForPage";
        public ManagePageStatsDTO GetStatsForPageFromCache(int pageId)
        {
            string key = SubsystemKey + GetStatsForPageKey + pageId;
            return CacheAccess.GetApplicationSpecificCacheItem<ManagePageStatsDTO>(SubsystemKey, key);
        }
        public void InsertStatsForPageIntoCache(int pageId, ManagePageStatsDTO dto)
        {
            string key = SubsystemKey + GetStatsForPageKey + pageId;
            CacheAccess.InsertApplicationSpecificCacheItem<ManagePageStatsDTO>(SubsystemKey, key, dto);
        }
        public void ClearStatsForPageFromCache(int pageId)
        {
            string key = SubsystemKey + GetStatsForPageKey + pageId;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        private const string GetStatsForPageQuestionKey = "GetStatsForPageQuestion";
        public ManagePageQuestionStats GetStatsForPageQuestionFromCache(int pageQuestionId)
        {
            string key = SubsystemKey + GetStatsForPageQuestionKey + pageQuestionId;
            return CacheAccess.GetApplicationSpecificCacheItem<ManagePageQuestionStats>(SubsystemKey, key);
        }
        public void InsertStatsForPageQuestionIntoCache(int pageQuestionId, ManagePageQuestionStats dto)
        {
            string key = SubsystemKey + GetStatsForPageQuestionKey + pageQuestionId;
            CacheAccess.InsertApplicationSpecificCacheItem<ManagePageQuestionStats>(SubsystemKey, key, dto);
        }
        public void ClearStatsForPageQuestionFromCache(int pageQuestionId)
        {
            string key = SubsystemKey + GetStatsForPageQuestionKey + pageQuestionId;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        #endregion

        private const string ListQuotesForDisplayKey = "ListQuotesForDisplay";
        public List<QuoteListItemDTO> ListQuotesForDisplayFromCache()
        {
            string key = SubsystemKey + ListQuotesForDisplayKey;
            return CacheAccess.GetApplicationSpecificCacheItem<List<QuoteListItemDTO>>(SubsystemKey, key);
        }
        public void InsertListQuotesForDisplayIntoCache(List<QuoteListItemDTO> dtos)
        {
            string key = SubsystemKey + ListQuotesForDisplayKey;
            CacheAccess.InsertApplicationSpecificCacheItem<List<QuoteListItemDTO>>(SubsystemKey, key, dtos);
        }
        public void ClearListQuotesForDisplayFromCache()
        {
            string key = SubsystemKey + ListQuotesForDisplayKey;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        private const string ListPossibleAnswersByQuestionKey = "ListPossibleAnswersByQuestion";
        public List<QuestionPossibleAnswerDTO> ListPossibleAnswersByQuestionFromCache(int questionId)
        {
            string key = SubsystemKey + ListPossibleAnswersByQuestionKey + questionId;
            return CacheAccess.GetApplicationSpecificCacheItem<List<QuestionPossibleAnswerDTO>>(SubsystemKey, key);
        }
        public void InsertPossibleAnswersByQuestionIntoCache(int questionId, List<QuestionPossibleAnswerDTO> dtos)
        {
            string key = SubsystemKey + ListPossibleAnswersByQuestionKey + questionId;
            CacheAccess.InsertApplicationSpecificCacheItem<List<QuestionPossibleAnswerDTO>>(SubsystemKey, key, dtos);
        }

        private const string ListPossibleAnswersByQuestionForDisplayKey = "ListPossibleAnswersByQuestionForDisplay";
        public List<QuestionPossibleAnswerForDisplayDTO> ListPossibleAnswersByQuestionForDisplayFromCache(int questionId)
        {
            string key = SubsystemKey + ListPossibleAnswersByQuestionForDisplayKey + questionId;
            return CacheAccess.GetApplicationSpecificCacheItem<List<QuestionPossibleAnswerForDisplayDTO>>(SubsystemKey, key);
        }
        public void InsertPossibleAnswersByQuestionForDisplayIntoCache(int questionId, List<QuestionPossibleAnswerForDisplayDTO> dtos)
        {
            string key = SubsystemKey + ListPossibleAnswersByQuestionForDisplayKey + questionId;
            CacheAccess.InsertApplicationSpecificCacheItem<List<QuestionPossibleAnswerForDisplayDTO>>(SubsystemKey, key, dtos);
        }
        
        private const string ListRenderedPagesForDisplayBySchemeKey = "ListRenderedPagesForDisplayByScheme";
        public List<RenderedPageForDisplayDTO> ListRenderedPagesForDisplayBySchemeFromCache(int schemeId)
        {
            string key = SubsystemKey + ListRenderedPagesForDisplayBySchemeKey + schemeId;
            return CacheAccess.GetApplicationSpecificCacheItem<List<RenderedPageForDisplayDTO>>(SubsystemKey, key);
        }
        public void InsertRenderedPagesForDisplayBySchemeIntoCache(int schemeId, List<RenderedPageForDisplayDTO> dtos)
        {
            string key = SubsystemKey + ListRenderedPagesForDisplayBySchemeKey + schemeId;
            CacheAccess.InsertApplicationSpecificCacheItem<List<RenderedPageForDisplayDTO>>(SubsystemKey, key, dtos);
        }
        public void ClearRenderedPagesForDisplayBySchemeFromCache(int schemeId)
        {
            string key = SubsystemKey + ListRenderedPagesForDisplayBySchemeKey + schemeId;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }
        
        private const string ListPageQuestionsForDisplayByPageKey = "ListPageQuestionsForDisplayByPage";
        public List<PageQuestionForDisplayDTO> ListPageQuestionsForDisplayByPageFromCache(int pageId)
        {
            string key = SubsystemKey + ListPageQuestionsForDisplayByPageKey + pageId;
            return CacheAccess.GetApplicationSpecificCacheItem<List<PageQuestionForDisplayDTO>>(SubsystemKey, key);
        }
        public void InsertPageQuestionsForDisplayByPageIntoCache(int pageId, List<PageQuestionForDisplayDTO> dtos)
        {
            string key = SubsystemKey + ListPageQuestionsForDisplayByPageKey + pageId;
            CacheAccess.InsertApplicationSpecificCacheItem<List<PageQuestionForDisplayDTO>>(SubsystemKey, key, dtos);
        }
        public void ClearPageQuestionsForDisplayByPageFromCache(int pageId)
        {
            string key = SubsystemKey + ListPageQuestionsForDisplayByPageKey + pageId;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        private const string ListPageTemplatesKey = "ListPageTemplates";
        public List<PageTemplateDTO> ListPageTemplatesFromCache()
        {
            string key = SubsystemKey + ListPageTemplatesKey;
            return CacheAccess.GetApplicationSpecificCacheItem<List<PageTemplateDTO>>(SubsystemKey, key);
        }
        public void InsertListPageTemplatesIntoCache(List<PageTemplateDTO> dtos)
        {
            string key = SubsystemKey + ListPageTemplatesKey;
            CacheAccess.InsertApplicationSpecificCacheItem<List<PageTemplateDTO>>(SubsystemKey, key, dtos);
        }
        public void ClearListPageTemplatesFromCache()
        {
            string key = SubsystemKey + ListPageTemplatesKey;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        private const string ListQuestionTypesKey = "ListQuestionTypes";
        public List<QuestionTypeDTO> ListQuestionTypesFromCache()
        {
            string key = SubsystemKey + ListQuestionTypesKey;
            return CacheAccess.GetApplicationSpecificCacheItem<List<QuestionTypeDTO>>(SubsystemKey, key);
        }
        public void InsertQuestionTypesIntoCache(List<QuestionTypeDTO> dtos)
        {
            string key = SubsystemKey + ListQuestionTypesKey;
            CacheAccess.InsertApplicationSpecificCacheItem<List<QuestionTypeDTO>>(SubsystemKey, key, dtos);
        }
        public void ClearQuestionTypesFromCache()
        {
            string key = SubsystemKey + ListQuestionTypesKey;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        private const string ListPageValidationsByPageQuestionKey = "ListPageValidationsByPageQuestion";
        public List<PageQuestionValidationDTO> ListPageValidationsByPageQuestionFromCache(int pageQuestionId)
        {
            string key = SubsystemKey + ListPageValidationsByPageQuestionKey + pageQuestionId;
            return CacheAccess.GetApplicationSpecificCacheItem<List<PageQuestionValidationDTO>>(SubsystemKey, key);
        }
        public void InsertPageValidationsByPageQuestionIntoCache(int pageQuestionId, List<PageQuestionValidationDTO> dtos)
        {
            string key = SubsystemKey + ListPageValidationsByPageQuestionKey + pageQuestionId;
            CacheAccess.InsertApplicationSpecificCacheItem<List<PageQuestionValidationDTO>>(SubsystemKey, key, dtos);
        }
        public void ClearPageValidationsByPageQuestionFromCache(int pageQuestionId)
        {
            string key = SubsystemKey + ListPageValidationsByPageQuestionKey + pageQuestionId;
            CacheAccess.ClearApplicationSpecificCacheItem(SubsystemKey, key);
        }

        #endregion

        #region area cache clearing

        public void ClearScheme(int schemeId)
        {
            ClearSchemesFromCache();
            ClearRenderedPagesForDisplayBySchemeFromCache(schemeId);
            ClearTreeStructureBySchemeFromCache(schemeId);
            ClearStatsForSchemeFromCache(schemeId);
        }

        public void ClearPage(int pageId, int schemeId)
        {
            ClearRenderedPagesForDisplayBySchemeFromCache(schemeId);
            ClearPageQuestionsForDisplayByPageFromCache(pageId);
            ClearTreeStructureBySchemeFromCache(schemeId);
            ClearStatsForPageFromCache(pageId);
            ClearStatsForSchemeFromCache(schemeId);            
        }

        public void ClearPageQuestion(int pageQuestionId, int pageId, int schemeId)
        {
            ClearStatsForPageQuestionFromCache(pageQuestionId);
            ClearPage(pageId, schemeId);
        }

        public void ClearQuestion(int questionId)
        {
            CacheAccess.ClearApplicationSpecificSubsystemCacheItems(SubsystemKey);
        }

        public void ClearPageTemplate(int pageTemplateId)
        {
            CacheAccess.ClearApplicationSpecificSubsystemCacheItems(SubsystemKey);
        }

        #endregion

    }
}
