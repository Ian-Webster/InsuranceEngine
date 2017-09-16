using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class PageQuestionConditionalDisplayDTO
    {
        [DataMember]
        public int PageQuestionConditionalDisplayID { get; set; }
        [DataMember]
        [Display(Name = "Source Page Question")]
        [Required(ErrorMessage = "You must select a Source Page Question")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Source Page Question")]
        public int SourcePageQuestionID { get; set; }
        [DataMember]
        [Display(Name = "Target Page Question")]
        [Required(ErrorMessage = "You must select a Target Page Question")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Target Page Question")]
        public int TargetPageQuestionID { get; set; }
        [DataMember]
        [Display(Name = "Trigger Question Possible Answer")]
        [Required(ErrorMessage = "You must select a Trigger Question Possible Answer")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Trigger Question Possible Answer")]
        public int TriggerQuestionPossibleAnswerID { get; set; }
        [DataMember]
        [Display(Name = "Hide")]
        public bool Hide { get; set; }

    }
}
