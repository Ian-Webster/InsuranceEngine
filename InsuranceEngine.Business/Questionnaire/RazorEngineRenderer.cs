using InsuranceEngine.DTO.Questionnaire;
using System.Text;

namespace InsuranceEngine.Business.Questionnaire
{
    public class RazorEngineRenderer : QuestionnaireBase<RazorEngineRenderer>
    {

        public StringBuilder RenderQuestionnairePage(RenderedPageForRenderingDTO pageDTO)
        {
            StringBuilder result = new StringBuilder();
            //result.AppendLine(RazorEngine.Razor.Parse(pageDTO.PageTemplateContent, new { Page = pageDTO }));
            result.AppendLine(pageDTO.PageTemplateContent);
            result = result.Replace("@*", string.Empty);
            result = result.Replace("*@", string.Empty);
            return result;
        }

    }
}
