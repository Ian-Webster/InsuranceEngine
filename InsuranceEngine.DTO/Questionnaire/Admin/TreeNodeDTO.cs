using InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums;
using System.Collections.Generic;

namespace InsuranceEngine.DTO.Questionnaire.Admin
{
    public class TreeNodeDTO
    {
        public string id;
        public string text;
        public string icon;
        public TreeNodeStateDTO state;
        public List<TreeNodeDTO> children;
        public Dictionary<string, string> data { get; set; }
        public NodeTypes NodeType { get; set; }
    }
}
