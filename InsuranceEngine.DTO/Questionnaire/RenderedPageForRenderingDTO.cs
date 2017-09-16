using System;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class RenderedPageForRenderingDTO
    {

        public int PageID { get; set; }

        public string SchemeCode { get; set; }

        public string PageName { get; set; }

        public string PageTemplateContent { get; set; }

    }
}
