using System;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class RenderedPageDTO
    {
        [DataMember]
        public int RenderedPageID { get; set; }
        [DataMember]
        public int PageID { get; set; }
        [DataMember]
        public string PageContent { get; set; }
        [DataMember]
        public DateTime? LastRenderDate { get; set; }

    }
}
