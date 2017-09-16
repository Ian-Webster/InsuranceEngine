using InsuranceEngine.DTO.Questionnaire.Admin;
using System.Collections.Generic;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class SchemeRenderedPagesVM
    {
        public int SchemeID { get; set; }
        public IEnumerable<RenderedPageForGridDTO> Pages { get; set; }

    }
}