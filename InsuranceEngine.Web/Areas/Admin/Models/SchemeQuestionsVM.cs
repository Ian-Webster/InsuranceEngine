﻿using InsuranceEngine.DTO.Questionnaire.Admin;
using System.Collections.Generic;

namespace InsuranceEngine.Web.Areas.Admin.Models
{
    public class SchemeQuestionsVM
    {

        public int SchemeID { get; set; }
        public IEnumerable<QuestionForGridDTO> Questions { get; set; }

    }
}