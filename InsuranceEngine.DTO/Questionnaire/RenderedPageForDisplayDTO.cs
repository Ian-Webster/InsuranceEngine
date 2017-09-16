using System;
using System.Collections.Generic;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class RenderedPageForDisplayDTO
    {

        public int PageID { get; set; }

        public int SchemeID { get; set; }

        public int DisplayOrder { get; set; }

        public string SchemeCode { get; set; }

        public string SchemeName { get; set; }

        public string PageName { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public int? NextPageID { get; set; }

        public List<PageQuestionForDisplayDTO> Questions { get; set; }

        public string DynamicViewsRootFolder { get; set; }

    }
}
