//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsuranceEngine.Data.EF.Questionnaire
{
    using System;
    using System.Collections.Generic;
    
    public partial class Quote
    {
        public Quote()
        {
            this.Quote_Question_Answer = new HashSet<Quote_Question_Answer>();
        }
    
        public int Quote_ID { get; set; }
        public int Scheme_ID { get; set; }
        public string Reference { get; set; }
        public System.DateTime QuoteDate { get; set; }
    
        public virtual ICollection<Quote_Question_Answer> Quote_Question_Answer { get; set; }
        public virtual Scheme Scheme { get; set; }
    }
}
