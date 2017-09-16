using InsuranceEngine.Business.BusinessManagers.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace InsuranceEngine.Business.Questionnaire
{
    public class QuestionnaireRendering
    {

        private static string DynamicViewsPath
        {
            get
            {
                string result = string.Empty;
                HttpContext context = HttpContext.Current;
                result = context.Server.MapPath(WebConfigurationManager.AppSettings["Urls.DynamicViewsFolder"]);
                return result;
            }
        }

        private static string QuestionControlsViewsPath
        {
            get
            {
                string result = string.Empty;
                HttpContext context = HttpContext.Current;
                result = context.Server.MapPath(WebConfigurationManager.AppSettings["Urls.QuestionControlFolder"]);
                return result;
            }
        }

        public static void RenderPagesForScheme(int schemeId)
        {
            List<RenderedPageForRenderingDTO> pages = QuestionnaireBusinessManager.Instance.ListPagesForRenderingByScheme(schemeId);

            //get razor template generated for all the pages
            foreach (RenderedPageForRenderingDTO page in pages)
            {
                page.PageTemplateContent = RazorEngineRenderer.Instance.RenderQuestionnairePage(page).ToString();
            }

            //write files to the file system
            string path = DynamicViewsPath + @"\" + pages[0].SchemeCode + @"\";
            string extension = ".cshtml";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            foreach (RenderedPageForRenderingDTO page in pages)
            {
                File.WriteAllText(path + page.PageName + extension, page.PageTemplateContent);
                QuestionnaireBusinessManager.Instance.SaveRenderedPage(page);
            }


        }

        public static void RenderQuestionTemplate(int questionTemplateId)
        {
            List<QuestionTemplateDTO> templates = new List<QuestionTemplateDTO>();
            templates.Add(QuestionnaireBusinessManager.Instance.LoadQuestionTemplate(questionTemplateId));
            RenderQuestionTemplates(templates);
        }

        public static void RenderQuestionTemplates()
        {
            RenderQuestionTemplates(QuestionnaireBusinessManager.Instance.ListQuestionTemplates());
        }

        private static void RenderQuestionTemplates(List<QuestionTemplateDTO> questionTemplates)
        {
            //get razor template generated for all question templates
            foreach (QuestionTemplateDTO questionTemplate in questionTemplates)
            {
                                
            }

            //write files to the file system
            string extension = ".cshtml";
            if (!Directory.Exists(QuestionControlsViewsPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(QuestionControlsViewsPath);
            }
            foreach (QuestionTemplateDTO questionTemplate in questionTemplates)
            {
                File.WriteAllText(QuestionControlsViewsPath + questionTemplate.Name + extension, questionTemplate.Template);
                questionTemplate.LastRenderDate = DateTime.Now;
                QuestionnaireBusinessManager.Instance.SaveQuestionTemplate(questionTemplate);
            }

        }

    }
}
