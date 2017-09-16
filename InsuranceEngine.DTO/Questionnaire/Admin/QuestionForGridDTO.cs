namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    public class QuestionForGridDTO
    {

        public int QuestionID { get; set; }
        public string QuestionTemplate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int TotalRows { get; set; }

    }
}
