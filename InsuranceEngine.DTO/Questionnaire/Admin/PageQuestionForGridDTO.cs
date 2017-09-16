namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    public class PageQuestionForGridDTO
    {

        public int PageQuestionID { get; set; }
        public string Question { get; set; }
        public string QuestionText { get; set; }
        public string QuestionName { get; set; }
        public int DisplayOrder { get; set; }
        public bool DefaultShow { get; set; }
        public int TotalRows { get; set; }

    }
}
