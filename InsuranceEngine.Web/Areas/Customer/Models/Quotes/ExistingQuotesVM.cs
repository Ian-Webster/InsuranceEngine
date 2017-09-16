using InsuranceEngine.DTO.Questionnaire;
using System.Collections.Generic;

namespace InsuranceEngine.Web.Areas.Customer.Models.Quotes
{
    public class ExistingQuotesVM
    {

        public List<QuoteListItemDTO> Quotes { get; set; }
        
        public int SelectedSchemeID { get; set; }

    }
}