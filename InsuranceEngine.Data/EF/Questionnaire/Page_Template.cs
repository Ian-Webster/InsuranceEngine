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
    
    public partial class Page_Template
    {
        public Page_Template()
        {
            this.Pages = new HashSet<Page>();
        }
    
        public int Page_Template_ID { get; set; }
        public string Name { get; set; }
        public string TemplateContent { get; set; }
    
        public virtual ICollection<Page> Pages { get; set; }
    }
}