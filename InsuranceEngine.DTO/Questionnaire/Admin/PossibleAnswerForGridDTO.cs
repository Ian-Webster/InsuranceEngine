namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    public class PossibleAnswerForGridDTO
    {

        public int PossibleAnswerID { get; set; }
        public string AnswerText { get; set; }
        public string AnswerValue { get; set; }
        public int DisplayOrder { get; set; }
        public int TotalRows { get; set; }

    }
}
