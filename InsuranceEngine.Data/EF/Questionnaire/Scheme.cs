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
    
    public partial class Scheme
    {
        public Scheme()
        {
            this.Questions = new HashSet<Question>();
            this.Quotes = new HashSet<Quote>();
            this.Pages = new HashSet<Page>();
        }
    
        public int Scheme_ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }
}
