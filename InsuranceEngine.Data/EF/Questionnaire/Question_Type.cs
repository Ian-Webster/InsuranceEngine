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
    
    public partial class Question_Type
    {
        public Question_Type()
        {
            this.Question_Template = new HashSet<Question_Template>();
        }
    
        public int Question_Type_ID { get; set; }
        public string Name { get; set; }
        public bool HasPossibleAnswers { get; set; }
    
        public virtual ICollection<Question_Template> Question_Template { get; set; }
    }
}