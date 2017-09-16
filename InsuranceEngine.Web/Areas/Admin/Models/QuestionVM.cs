using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Utility.Enums.AdminUIEnums;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class QuestionVM
    {

        public int QuestionID { get; set; }
        public int SchemeID { get; set; }
        public QuestionTabs SelectedTab { get; set; }
        public QuestionForAdminDisplayDTO Question { get; set; }
        public ManageQuestionStatsDTO Stats { get; set; }

    }
}