using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Utility.Enums.AdminUIEnums;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class PageVM
    {
        public PageDTO Page { get; set; }
        public int PageID { get; set; }
        public int SchemeID { get; set; }
        public PageTabs SelectedTab { get; set; }
        public ManagePageStatsDTO Stats { get; set; }

    }
}