using System;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    [Serializable]
    public class PageForGridDTO
    {

        [DataMember]
        public int PageID { get; set; }
        [DataMember]
        public string PageTemplate { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public string NextPageName { get; set; }
        [DataMember]
        public int TotalRows { get; set; }

    }
}
