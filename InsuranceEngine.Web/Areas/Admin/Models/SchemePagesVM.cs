using InsuranceEngine.DTO.Questionnaire.Admin;
using System.Collections.Generic;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class SchemePagesVM
    {
        public int SchemeID { get; set; }
        public IEnumerable<PageForGridDTO> Pages { get; set; }

    }
}