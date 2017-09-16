namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    public class QuestionForAdminDisplayDTO
    {

        public int QuestionID { get; set; }
        public string QuestionTemplate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool HasPossibleAnswers { get; set; }
    }
}
