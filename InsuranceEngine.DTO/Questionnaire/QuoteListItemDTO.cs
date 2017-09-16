using System;

namespace InsuranceEngine.DTO.Questionnaire
{
    public class QuoteListItemDTO
    {

        public int QuoteID { get; set; }

        public int SchemeID { get; set; }

        public string Reference { get; set; }

        public string SchemeCode { get; set; }

        public DateTime QuoteDate { get; set; }

    }
}
