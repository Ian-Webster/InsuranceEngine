using System;

namespace InsuranceEngine.DTO.Questionnaire
{
    [Serializable]
    public class PageQuestionVisibilityDTO
    {
        public int PageQuestionID { get; set; }
        public bool IsVisible { get; set; }

    }
}
