using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Utility.Enums.AdminUIEnums;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class PageQuestionVM
    {

        public int PageQuestionID { get; set; }

        public int PageID { get; set; }

        public int SchemeID { get; set; }

        public PageQuestionForAdminDisplayDTO PageQuestion { get; set; }

        public ManagePageQuestionStats Stats { get; set; }

        public PageQuestionTabs SelectedTab { get; set; }
    }
}