using InsuranceEngine.Data.EF.Questionnaire;
using InsuranceEngine.DTO.Questionnaire;
using InsuranceEngine.DTO.Questionnaire.Admin;
using InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums;

namespace InsuranceEngine.Data.Mapping.Mappings
{
    public class QuestionnaireMappings
    {

        public static void Map()
        {

            #region direct table mappings

            AutoMapper.Mapper.CreateMap<Scheme, SchemeDTO>()
                .ForMember(dest => dest.SchemeID, opt => opt.MapFrom(source => source.Scheme_ID))
                .ForMember(dest => dest.Pages, opt => opt.Ignore())
                .ForMember(dest => dest.RenderedPages, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Scheme, SchemeForGridDTO>()
                .ForMember(dest => dest.SchemeID, opt => opt.MapFrom(source => source.Scheme_ID));

            AutoMapper.Mapper.CreateMap<Quote, QuoteDTO>()
                .ForMember(dest => dest.QuoteID, opt => opt.MapFrom(source => source.Quote_ID))
                .ForMember(dest => dest.SchemeID, opt => opt.MapFrom(source => source.Scheme_ID))
                .ForMember(dest => dest.Answers, opt => opt.Ignore())
                .ForMember(dest => dest.RemovedAnswers, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Question_Template, QuestionTemplateDTO>()
                .ForMember(dest => dest.QuestionTemplateID, opt => opt.MapFrom(source => source.Question_Template_ID))
                .ForMember(dest => dest.QuestionTypeID, opt => opt.MapFrom(source => source.Question_Type_ID));

            AutoMapper.Mapper.CreateMap<Page, PageDTO>()
                .ForMember(dest => dest.PageID, opt => opt.MapFrom(source => source.Page_ID))
                .ForMember(dest => dest.PageTemplateID, opt => opt.MapFrom(source => source.Page_Template_ID))
                .ForMember(dest => dest.SchemeID, opt => opt.MapFrom(source => source.Scheme_ID))
                .ForMember(dest => dest.NextPageID, opt => opt.MapFrom(source => source.Next_Page_ID))
                .ForMember(dest => dest.PageQuestions, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Rendered_Page, RenderedPageDTO>()
                .ForMember(dest => dest.RenderedPageID, opt => opt.MapFrom(source => source.Rendered_Page_ID))
                .ForMember(dest => dest.PageID, opt => opt.MapFrom(source => source.Page_ID));

            AutoMapper.Mapper.CreateMap<Page_Question, PageQuestionDTO>()
                .ForMember(dest => dest.PageQuestionID, opt => opt.MapFrom(source => source.Page_Question_ID))
                .ForMember(dest => dest.PageID, opt => opt.MapFrom(source => source.Page_ID))
                .ForMember(dest => dest.QuestionID, opt => opt.MapFrom(source => source.Question_ID));
                
            AutoMapper.Mapper.CreateMap<Quote_Question_Answer, QuoteQuestionAnswerDTO>()
                .ForMember(dest => dest.QuoteQuestionAnswerID, opt => opt.MapFrom(source => source.Quote_Question_Answer_ID))
                .ForMember(dest => dest.QuoteID, opt => opt.MapFrom(source => source.Quote_ID))
                .ForMember(dest => dest.QuestionID, opt => opt.MapFrom(source => source.Question_ID))
                .ForMember(dest => dest.QuestionPossibleAnswerID, opt => opt.MapFrom(source => source.Question_Possible_Answer_ID));

            AutoMapper.Mapper.CreateMap<Question_Possible_Answer, QuestionPossibleAnswerDTO>()
                .ForMember(dest => dest.QuestionPossibleAnswerID, opt => opt.MapFrom(source => source.Question_Possible_Answer_ID))
                .ForMember(dest => dest.QuestionID, opt => opt.MapFrom(source => source.Question_ID));

            AutoMapper.Mapper.CreateMap<Question_Possible_Answer, QuestionPossibleAnswerForDisplayDTO>()
                .ForMember(dest => dest.QuestionPossibleAnswerID, opt => opt.MapFrom(source => source.Question_Possible_Answer_ID))
                .ForMember(dest => dest.QuestionID, opt => opt.MapFrom(source => source.Question_ID))
                .ForMember(dest => dest.DisplayConditions, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Quote_Question_Answer, QuoteQuestionAnswerDTO>()
                .ForMember(dest => dest.QuoteQuestionAnswerID, opt => opt.MapFrom(source => source.Quote_Question_Answer_ID))
                .ForMember(dest => dest.QuoteID, opt => opt.MapFrom(source => source.Quote_ID))
                .ForMember(dest => dest.QuestionID, opt => opt.MapFrom(source => source.Question_ID))
                .ForMember(dest => dest.QuestionPossibleAnswerID, opt => opt.MapFrom(source => source.Question_Possible_Answer_ID));

            AutoMapper.Mapper.CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.QuestionID, opt => opt.MapFrom(source => source.Question_ID))
                .ForMember(dest => dest.SchemeID, opt => opt.MapFrom(source => source.Scheme_ID))
                .ForMember(dest => dest.QuestionTemplateID, opt => opt.MapFrom(source => source.Question_Template_ID))
                .ForMember(dest => dest.PossibleAnswers, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Page_Template, PageTemplateDTO>()
                .ForMember(dest => dest.PageTemplateID, opt => opt.MapFrom(source => source.Page_Template_ID));

            AutoMapper.Mapper.CreateMap<Page_Question_Validation, PageQuestionValidationDTO>()
                .ForMember(dest => dest.PageQuestionValidationID, opt => opt.MapFrom(source => source.Page_Question_Validation_ID))
                .ForMember(dest => dest.PageQuestionID, opt => opt.MapFrom(source => source.Page_Question_ID))
                .ForMember(dest => dest.ValidationType, opt => opt.MapFrom(source => (ValidationTypes)source.Validation_Type_ID));

            AutoMapper.Mapper.CreateMap<Question_Type, QuestionTypeDTO>()
                .ForMember(dest => dest.QuestionTypeID, opt => opt.MapFrom(source => source.Question_Type_ID));

            AutoMapper.Mapper.CreateMap<Page_Question_Conditional_Display, PageQuestionConditionalDisplayDTO>()
                .ForMember(dest => dest.PageQuestionConditionalDisplayID, opt => opt.MapFrom(source => source.Page_Question_Conditional_Display_ID))
                .ForMember(dest => dest.SourcePageQuestionID, opt => opt.MapFrom(source => source.Source_Page_Question_ID))
                .ForMember(dest => dest.TargetPageQuestionID, opt => opt.MapFrom(source => source.Target_Page_Question_ID))
                .ForMember(dest => dest.TriggerQuestionPossibleAnswerID, opt => opt.MapFrom(source => source.Trigger_Question_Possible_Answer_ID));

            AutoMapper.Mapper.CreateMap<Page_Question_Conditional_Display, PageQuestionConditionalDisplayForDisplayDTO>()
                .ForMember(dest => dest.TargetPageQuestionID, opt => opt.MapFrom(source => source.Target_Page_Question_ID));

            #endregion

            #region composite mappings

            AutoMapper.Mapper.CreateMap<Quote, QuoteListItemDTO>()
                .ForMember(dest => dest.QuoteID, opt => opt.MapFrom(source => source.Quote_ID))
                .ForMember(dest => dest.SchemeID, opt => opt.MapFrom(source => source.Scheme_ID))
                .ForMember(dest => dest.SchemeCode, opt => opt.MapFrom(source => source.Scheme.Code));

            AutoMapper.Mapper.CreateMap<Rendered_Page, RenderedPageForDisplayDTO>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(source => source.PageContent))
                .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(source => source.Page.DisplayOrder))
                .ForMember(dest => dest.PageName, opt => opt.MapFrom(source => source.Page.Name))
                .ForMember(dest => dest.PageTitle, opt => opt.MapFrom(source => source.Page.Title))
                .ForMember(dest => dest.SchemeCode, opt => opt.MapFrom(source => source.Page.Scheme.Code))
                .ForMember(dest => dest.SchemeID, opt => opt.MapFrom(source => source.Page.Scheme_ID))
                .ForMember(dest => dest.SchemeName, opt => opt.MapFrom(source => source.Page.Scheme.Name))
                .ForMember(dest => dest.NextPageID, opt => opt.MapFrom(source => source.Page.Next_Page_ID))
                .ForMember(dest => dest.PageID, opt => opt.MapFrom(source => source.Page_ID))
                .ForMember(dest => dest.Questions, opt => opt.Ignore())
                .ForMember(dest => dest.DynamicViewsRootFolder, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Page_Question, PageQuestionForDisplayDTO>()
                .ForMember(dest => dest.QuestionID, opt => opt.MapFrom(source => source.Question_ID))
                .ForMember(dest => dest.QuestionTemplatePath, opt => opt.MapFrom(source => source.Question.Question_Template.Name))
                .ForMember(dest => dest.PossibleAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.PageQuestionID, opt => opt.MapFrom(source => source.Page_Question_ID))
                .ForMember(dest => dest.HasPossibleAnswers, opt => opt.MapFrom(source => source.Question.Question_Template.Question_Type.HasPossibleAnswers))
                .ForMember(dest => dest.DependantQuestions, opt => opt.Ignore())
                .ForMember(dest => dest.HasDisplayConditions, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Page, RenderedPageForRenderingDTO>()
                .ForMember(dest => dest.PageName, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.SchemeCode, opt => opt.MapFrom(source => source.Scheme.Code))
                .ForMember(dest => dest.PageTemplateContent, opt => opt.MapFrom(source => source.Page_Template.TemplateContent))
                .ForMember(dest => dest.PageID, opt => opt.MapFrom(source => source.Page_ID));
                

            #endregion

            #region stored proc mappings

            AutoMapper.Mapper.CreateMap<Pages_ListPagesForGrid_Result, PageForGridDTO>();

            AutoMapper.Mapper.CreateMap<Questions_ListQuestionsForGrid_Result, QuestionForGridDTO>();

            AutoMapper.Mapper.CreateMap<RenderedPages_ListRenderedPagesForGrid_Result, RenderedPageForGridDTO>();

            AutoMapper.Mapper.CreateMap<PageQuestions_ListPageQuestionsForGrid_Result, PageQuestionForGridDTO>();

            AutoMapper.Mapper.CreateMap<PageQuestions_ListPageQuestionValidationForGrid_Result, PageQuestionValidationForGridDTO>();

            AutoMapper.Mapper.CreateMap<PageQuestions_ListPageQuestionDisplayConditionsForGrid_Result, PageQuestionConditionalDisplayForGridDTO>();

            AutoMapper.Mapper.CreateMap<Questions_ListPossibleAnswerForGrid_Result, PossibleAnswerForGridDTO>();

            #endregion

        }

    }
}
