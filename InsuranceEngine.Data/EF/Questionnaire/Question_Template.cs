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
    
    public partial class Question_Template
    {
        public Question_Template()
        {
            this.Questions = new HashSet<Question>();
        }
    
        public int Question_Template_ID { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
        public Nullable<System.DateTime> LastRenderDate { get; set; }
        public int Question_Type_ID { get; set; }
    
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Question_Type Question_Type { get; set; }
    }
}
