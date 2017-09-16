namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    public class PageQuestionConditionalDisplayForGridDTO
    {

        public int PageQuestionConditionalDisplayID { get; set; }
        public string SourcePageQuestionName { get; set; }
        public string TargetPageQuestionName { get; set; }
        public string TriggerAnswer { get; set; }
        public bool Hide { get; set; }
        public int TotalRows { get; set; }

    }
}
