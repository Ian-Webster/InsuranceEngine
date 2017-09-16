using System;

namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    public class RenderedPageForGridDTO
    {
        public int RenderedPageID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime? LastRenderDate { get; set; }
        public int TotalRows { get; set; }

    }
}
