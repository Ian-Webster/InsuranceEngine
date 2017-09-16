using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Utility.Enums.AdminUIEnums;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class SchemeVM
    {

        public SchemeDTO Scheme { get; set; }
        public ManageSchemeStatsDTO Stats { get; set; }
        public SchemePageTabs SelectedTab { get; set; }

    }
}