INSERT [dbo].[Question_Type] ([Question_Type_ID], [Name], [HasPossibleAnswers]) VALUES (1, N'Free Text', 0)
INSERT [dbo].[Question_Type] ([Question_Type_ID], [Name], [HasPossibleAnswers]) VALUES (2, N'Date Picker', 0)
INSERT [dbo].[Question_Type] ([Question_Type_ID], [Name], [HasPossibleAnswers]) VALUES (3, N'Dropdown List', 1)
INSERT [dbo].[Question_Type] ([Question_Type_ID], [Name], [HasPossibleAnswers]) VALUES (4, N'Radio Button List', 1)
SET IDENTITY_INSERT [dbo].[Question_Template] ON 

INSERT [dbo].[Question_Template] ([Question_Template_ID], [Question_Type_ID], [Name], [Template], [LastRenderDate]) VALUES (1, 1, N'Textbox', N'@model InsuranceEngine.Web.Models.Questionnaire.QuestionVM

@{
    //required for server side validation to get server validated questions to have error messages displayed inline
    bool serverValidationPassed = Html.ViewData.ModelState["Question_" + Model.Question.PageQuestionID.ToString()] == null;
}
<div class="form-group" id="QuestionContainer_@Model.Question.PageQuestionID" @Html.Raw(Model.Visibility.IsVisible ? string.Empty : @" style=""display:none""")>
    <label for="Question_@Model.Question.PageQuestionID" class="control-label col-sm-4 @(Model.Question.IsRequired ? "required": string.Empty)">@Model.Question.QuestionText</label>
    <div class="col-sm-6">
        <input type="text" class="form-control @(!serverValidationPassed ? "input-validation-error" : string.Empty)"
               id="Question_@Model.Question.PageQuestionID" name="Question_@Model.Question.PageQuestionID"
               value="@Model.GetAnswerText(Model.Question.QuestionID)"
               @{ Html.RenderPartial("~/Views/QuestionnaireControls/Validation.cshtml", Model.Question.Validators); } />
        <span class="field-validation-valid" data-valmsg-for="Question_@Model.Question.PageQuestionID" data-valmsg-replace="true"></span>
        @{
            //server side validation
            if (!serverValidationPassed)
            {
                foreach (var error in Html.ViewData.ModelState["Question_" + @Model.Question.PageQuestionID.ToString()].Errors)
                {
                    <text><span class="field-validation-error">@error.ErrorMessage</span></text>
                }
            }
        }
    </div>
</div>', CAST(N'2017-09-16T12:52:45.663' AS DateTime))
INSERT [dbo].[Question_Template] ([Question_Template_ID], [Question_Type_ID], [Name], [Template], [LastRenderDate]) VALUES (2, 3, N'DropdownList', N'@model InsuranceEngine.Web.Models.Questionnaire.QuestionVM

@{
    //required for server side validation to get server validated questions to have error messages displayed inline
    bool serverValidationPassed = Html.ViewData.ModelState["Question_" + Model.Question.PageQuestionID.ToString()] == null;
}
<div class="form-group" id="QuestionContainer_@Model.Question.PageQuestionID" @Html.Raw(Model.Visibility.IsVisible ? string.Empty : @" style=""display:none""")>
    <label for="Question_@Model.Question.PageQuestionID" class="control-label col-sm-4 @(Model.Question.IsRequired ? "required": string.Empty)">@Model.Question.QuestionText</label>
    <div class="col-sm-6">
        <select class="form-control @(!serverValidationPassed ? "input-validation-error" : string.Empty)"
                id="Question_@Model.Question.PageQuestionID" name="Question_@Model.Question.PageQuestionID"
                @{ Html.RenderPartial("~/Views/QuestionnaireControls/Validation.cshtml", Model.Question.Validators); }
                @{ Html.RenderPartial("~/Views/QuestionnaireControls/ConditionalDisplay_DependantQuestions.cshtml", Model.Question.DependantQuestions); }
                @Html.Raw(Model.Question.HasDisplayConditions ? @"onchange=""dropDownChanged(this)""" : string.Empty)>
            <option value="">select one</option>
            @foreach (var possibleAnswer in @Model.Question.PossibleAnswers)
            {
                <option value="@possibleAnswer.AnswerValue" @( Model.GetPossibleAnswerID(Model.Question.QuestionID).GetValueOrDefault(0) == possibleAnswer.QuestionPossibleAnswerID ? "selected" : string.Empty)
                        @{ Html.RenderPartial("~/Views/QuestionnaireControls/ConditionalDisplay_TargetQuestions.cshtml", possibleAnswer.DisplayConditions); }>
                    @possibleAnswer.AnswerText
                </option>
            }
        </select>
        <span class="field-validation-valid" data-valmsg-for="Question_@Model.Question.PageQuestionID" data-valmsg-replace="true"></span>
        @{
            //server side validation
            if (!serverValidationPassed)
            {
                foreach (var error in Html.ViewData.ModelState["Question_" + @Model.Question.PageQuestionID.ToString()].Errors)
                {
                    <text><span class="field-validation-error">@error.ErrorMessage</span><br /></text>
                }

            }
        }
    </div>
</div>', CAST(N'2017-09-16T12:52:45.630' AS DateTime))
INSERT [dbo].[Question_Template] ([Question_Template_ID], [Question_Type_ID], [Name], [Template], [LastRenderDate]) VALUES (3, 4, N'RadioButtonList', N'@model InsuranceEngine.Web.Models.Questionnaire.QuestionVM

@{
    //required for server side validation to get server validated questions to have error messages displayed inline
    bool serverValidationPassed = Html.ViewData.ModelState["Question_" + Model.Question.PageQuestionID.ToString()] == null;
}
<div class="form-group" id="QuestionContainer_@Model.Question.PageQuestionID" @Html.Raw(Model.Visibility.IsVisible ? string.Empty : @" style=""display:none"""))>
    <label for="Question_@Model.Question.PageQuestionID" class="control-label col-sm-4 @(Model.Question.IsRequired ? "required": string.Empty)">@Model.Question.QuestionText</label>
    <div class="col-sm-6">
        <div class="form">
            @foreach (var possibleAnswer in @Model.Question.PossibleAnswers)
            {
                if (Model.Question.PossibleAnswers.IndexOf(possibleAnswer) == 0)
                {
                    <label class="radio">
                        <input value="@possibleAnswer.AnswerValue" type="radio" name="Question_@Model.Question.PageQuestionID"
                               id="Question_@Model.Question.PageQuestionID"
                               @( Model.GetPossibleAnswerID(Model.Question.QuestionID).GetValueOrDefault(0) == possibleAnswer.QuestionPossibleAnswerID ? "checked" : string.Empty)
                               @{ Html.RenderPartial("~/Views/QuestionnaireControls/Validation.cshtml", Model.Question.Validators); }
                               @{ Html.RenderPartial("~/Views/QuestionnaireControls/ConditionalDisplay_TargetQuestions.cshtml", possibleAnswer.DisplayConditions); }
                               @{ Html.RenderPartial("~/Views/QuestionnaireControls/ConditionalDisplay_DependantQuestions.cshtml", Model.Question.DependantQuestions); }
                               @Html.Raw(Model.Question.HasDisplayConditions ? @"onchange=""radioButtonChanged(this)""" : string.Empty)>@possibleAnswer.AnswerText
                        </label>
                }
                else
                {
                    <label class="radio">
                        <input value="@possibleAnswer.AnswerValue" type="radio" name="Question_@Model.Question.PageQuestionID"
                               id="Question_@Model.Question.PageQuestionID"
                               @( Model.GetPossibleAnswerID(Model.Question.QuestionID).GetValueOrDefault(0) == possibleAnswer.QuestionPossibleAnswerID ? "checked" : string.Empty)
                               @{ Html.RenderPartial("~/Views/QuestionnaireControls/ConditionalDisplay_TargetQuestions.cshtml", possibleAnswer.DisplayConditions); }
                               @{ Html.RenderPartial("~/Views/QuestionnaireControls/ConditionalDisplay_DependantQuestions.cshtml", Model.Question.DependantQuestions); }
                               @Html.Raw(Model.Question.HasDisplayConditions ? @"onchange=""radioButtonChanged(this)""" : string.Empty)>@possibleAnswer.AnswerText
                        </label>
                }

            }
            <span class="field-validation-valid" data-valmsg-for="Question_@Model.Question.PageQuestionID" data-valmsg-replace="true"></span>
            @{
                //server side validation
                if (!serverValidationPassed)
                {
                    foreach (var error in Html.ViewData.ModelState["Question_" + @Model.Question.PageQuestionID.ToString()].Errors)
                    {
                        <text><span class="field-validation-error">@error.ErrorMessage</span><br /></text>
                    }

                }
            }
        </div>
    </div>
</div>', CAST(N'2017-09-16T12:52:45.643' AS DateTime))
INSERT [dbo].[Question_Template] ([Question_Template_ID], [Question_Type_ID], [Name], [Template], [LastRenderDate]) VALUES (4, 2, N'DatePicker', N'@model InsuranceEngine.Web.Models.Questionnaire.QuestionVM

@{
    //required for server side validation to get server validated questions to have error messages displayed inline
    bool serverValidationPassed = Html.ViewData.ModelState["Question_" + Model.Question.PageQuestionID.ToString()] == null;
}
<div class="form-group" id="QuestionContainer_@Model.Question.PageQuestionID" @Html.Raw(Model.Visibility.IsVisible ? string.Empty : @" style=""display:none""")>
    <label for="Question_@Model.Question.PageQuestionID" class="control-label col-sm-4 @(Model.Question.IsRequired ? "required": string.Empty)">@Model.Question.QuestionText</label>
    <div class="col-sm-6">
        <div class=''input-group date'' id=''Question_@Model.Question.PageQuestionID''>
            <input type="text" class="form-control @(!serverValidationPassed ? "input-validation-error" : string.Empty)" value="@Model.GetAnswerText(Model.Question.QuestionID)" name=''Question_@Model.Question.PageQuestionID''
                   @{ Html.RenderPartial("~/Views/QuestionnaireControls/Validation.cshtml", Model.Question.Validators); } />
            <span class="input-group-addon">
                <span class="fa fa-calendar"></span>
            </span>
        </div>
        <span class="field-validation-valid" data-valmsg-for="Question_@Model.Question.PageQuestionID" data-valmsg-replace="true"></span>
        @{
            //server side validation
            if (!serverValidationPassed)
            {
                foreach (var error in Html.ViewData.ModelState["Question_" + @Model.Question.PageQuestionID.ToString()].Errors)
                {
                    <text><span class="field-validation-error">@error.ErrorMessage</span><br /></text>
                }

            }
        }
    </div>
</div>



<script type="text/javascript">
    $(document).ready(
    function () {
        $(''#Question_@Model.Question.PageQuestionID'').datepicker({
            pickTime: true,
            format: "dd/mm/yyyy",
            todayBtn: "linked",
            autoclose: true,
            todayHighlight: true
        });
    });
</script>

@*http://bootstrap-datepicker.readthedocs.org/en/release/options.html*@', CAST(N'2017-09-16T12:52:45.627' AS DateTime))
INSERT [dbo].[Question_Template] ([Question_Template_ID], [Question_Type_ID], [Name], [Template], [LastRenderDate]) VALUES (5, 1, N'LargeTextbox', N'@model InsuranceEngine.Web.Models.Questionnaire.QuestionVM

@{
    //required for server side validation to get server validated questions to have error messages displayed inline
    bool serverValidationPassed = Html.ViewData.ModelState["Question_" + Model.Question.PageQuestionID.ToString()] == null;
}
<div class="form-group" id="QuestionContainer_@Model.Question.PageQuestionID" @Html.Raw(Model.Visibility.IsVisible ? string.Empty : @" style=""display:none""")>
    <label for="Question_@Model.Question.PageQuestionID" class="control-label col-sm-4 @(Model.Question.IsRequired ? "required": string.Empty)">@Model.Question.QuestionText</label>
    <div class="col-sm-6">
        <textarea rows="5" class="form-control @(!serverValidationPassed ? "input-validation-error" : string.Empty)"
                  id="Question_@Model.Question.PageQuestionID" name="Question_@Model.Question.PageQuestionID"
                  @{ Html.RenderPartial("~/Views/QuestionnaireControls/Validation.cshtml", Model.Question.Validators); }>
            @Model.GetAnswerText(Model.Question.QuestionID)
        </textarea>
        <span class="field-validation-valid" data-valmsg-for="Question_@Model.Question.PageQuestionID" data-valmsg-replace="true"></span>
        @{
            //server side validation
            if (!serverValidationPassed)
            {
                foreach (var error in Html.ViewData.ModelState["Question_" + @Model.Question.PageQuestionID.ToString()].Errors)
                {
                    <text><span class="field-validation-error">@error.ErrorMessage</span><br /></text>
                }
            }
        }
    </div>
</div>
', CAST(N'2017-09-16T12:52:45.637' AS DateTime))
INSERT [dbo].[Question_Template] ([Question_Template_ID], [Question_Type_ID], [Name], [Template], [LastRenderDate]) VALUES (6, 1, N'SmallTextbox', N'@model InsuranceEngine.Web.Models.Questionnaire.QuestionVM

@{
    //required for server side validation to get server validated questions to have error messages displayed inline
    bool serverValidationPassed = Html.ViewData.ModelState["Question_" + Model.Question.PageQuestionID.ToString()] == null;
}
<div class="form-group" id="QuestionContainer_@Model.Question.PageQuestionID" @Html.Raw(Model.Visibility.IsVisible ? string.Empty : @" style=""display:none""")>
    <label for="Question_@Model.Question.PageQuestionID" class="control-label col-sm-4 @(Model.Question.IsRequired ? "required": string.Empty)">@Model.Question.QuestionText</label>
    <div class="col-sm-6">
        <input type="text" class="form-control smallTextBox @(!serverValidationPassed ? "input-validation-error" : string.Empty)" id="Question_@Model.Question.PageQuestionID" name="Question_@Model.Question.PageQuestionID"
               value="@Model.GetAnswerText(Model.Question.QuestionID)"
               @{ Html.RenderPartial("~/Views/QuestionnaireControls/Validation.cshtml", Model.Question.Validators); } />
        <span class="field-validation-valid" data-valmsg-for="Question_@Model.Question.PageQuestionID" data-valmsg-replace="true"></span>
        @{
            //server side validation
            if (!serverValidationPassed)
            {
                foreach (var error in Html.ViewData.ModelState["Question_" + @Model.Question.PageQuestionID.ToString()].Errors)
                {
                    <text><span class="field-validation-error">@error.ErrorMessage</span><br /></text>
                }

            }
        }
    </div>
</div>', CAST(N'2017-09-16T12:52:45.653' AS DateTime))
SET IDENTITY_INSERT [dbo].[Question_Template] OFF
SET IDENTITY_INSERT [dbo].[Scheme] ON 

INSERT [dbo].[Scheme] ([Scheme_ID], [Name], [Code]) VALUES (7, N'Pet Scheme', N'Pet')
INSERT [dbo].[Scheme] ([Scheme_ID], [Name], [Code]) VALUES (8, N'Bouncey Castle', N'BC1')
SET IDENTITY_INSERT [dbo].[Scheme] OFF
SET IDENTITY_INSERT [dbo].[Question] ON 

INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (48, 7, 3, N'Pet Type', N'PetType')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (49, 7, 1, N'Pet Name', N'PetName')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (50, 7, 3, N'Pet Cat Let Out', N'PetIndoor')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (51, 7, 6, N'Pet Cat No Of Posts', N'PetNoPosts')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (52, 7, 6, N'Pet Dog No Of Walks', N'PetWalks')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (53, 7, 1, N'Pet Other Type', N'PetOther')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (54, 7, 4, N'Pet Cat Purchase Date', N'PetCatDate')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (55, 7, 4, N'Pet Dog Purchase Date', N'PetDogDate')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (56, 7, 4, N'Pet Other Purchase Date', N'PetOtherDate')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (57, 7, 1, N'Customer Name', N'Name')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (58, 7, 4, N'Customer DOB', N'DOB')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (59, 7, 1, N'Customer Email', N'Email')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (60, 7, 1, N'Customer Phone', N'Phone')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (61, 7, 2, N'Cover Level', N'CoverLevel')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (62, 7, 3, N'Cover Include Legal', N'CoverLegal')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (63, 7, 4, N'Cover Start Date', N'CoverStart')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (64, 8, 2, N'Make of Bouncy Castle', N'make_bc')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (65, 8, 6, N'Bouncy Castle Volume (msq)', N'volume_bc')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (66, 8, 6, N'Bouncy Castle Value', N'value_bc')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (67, 8, 2, N'Bouncy King Makes', N'bouncy_king_makes')
INSERT [dbo].[Question] ([Question_ID], [Scheme_ID], [Question_Template_ID], [Name], [Code]) VALUES (68, 8, 1, N'Other Makes', N'make_other')
SET IDENTITY_INSERT [dbo].[Question] OFF
SET IDENTITY_INSERT [dbo].[Quote] ON 

INSERT [dbo].[Quote] ([Quote_ID], [Scheme_ID], [Reference], [QuoteDate]) VALUES (21, 7, N'Pet-141014-120740', CAST(N'2014-10-14T12:07:40.460' AS DateTime))
INSERT [dbo].[Quote] ([Quote_ID], [Scheme_ID], [Reference], [QuoteDate]) VALUES (22, 7, N'Pet-141029-122813', CAST(N'2014-10-29T12:28:13.337' AS DateTime))
INSERT [dbo].[Quote] ([Quote_ID], [Scheme_ID], [Reference], [QuoteDate]) VALUES (23, 7, N'Pet-141029-122816', CAST(N'2014-10-29T12:28:16.957' AS DateTime))
INSERT [dbo].[Quote] ([Quote_ID], [Scheme_ID], [Reference], [QuoteDate]) VALUES (24, 7, N'Pet-141029-125631', CAST(N'2014-10-29T12:56:31.693' AS DateTime))
INSERT [dbo].[Quote] ([Quote_ID], [Scheme_ID], [Reference], [QuoteDate]) VALUES (25, 7, N'Pet-150623-174803', CAST(N'2015-06-23T17:48:03.817' AS DateTime))
INSERT [dbo].[Quote] ([Quote_ID], [Scheme_ID], [Reference], [QuoteDate]) VALUES (26, 8, N'BC1-150623-181303', CAST(N'2015-06-23T18:13:03.587' AS DateTime))
INSERT [dbo].[Quote] ([Quote_ID], [Scheme_ID], [Reference], [QuoteDate]) VALUES (27, 7, N'Pet-151219-113920', CAST(N'2015-12-19T11:39:20.613' AS DateTime))
SET IDENTITY_INSERT [dbo].[Quote] OFF
SET IDENTITY_INSERT [dbo].[Page_Template] ON 

INSERT [dbo].[Page_Template] ([Page_Template_ID], [Name], [TemplateContent]) VALUES (1, N'Standard', N'@model InsuranceEngine.Web.Models.Questionnaire.PageVM

@using (Html.BeginForm())
{
    
    @Html.AntiForgeryToken()
    
    <div class="row">

        @if (!Html.ViewData.ModelState.IsValid)
        {
            <div class="alert alert-danger" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>
        }
        else
        {
            <div class="alert alert-danger" style="display:none" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>            
        }

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">@Model.Page.PageTitle</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    
                    @foreach (var question in @Model.Questions)
                    {
                        Html.RenderPartial(question.Question.QuestionTemplatePath, question);
                    }                    

                    <div class="form-group">
                        <div class="col-sm-1 col-xs-1">
                            @Html.ActionLink("Back", "PreviousPage", null, new { @class = "btn btn-default" })
                        </div>
                        <div class="col-sm-3 col-xs-7">
                            &nbsp;
                        </div>
                        <div class="col-sm-1 col-xs-1">
                            <input type="submit" class="btn btn-primary" value="Next" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
}

<script type="text/javascript">
    $(document).ready(function () {
        var validator = $("form").bind("invalid-form.validate", function (f, v) {
            if (v.errorList.length > 0) {
                $(''#summary'').show();
            }
            else {
                $(''#summary'').hide();
            }
        });
    });
</script>


')
SET IDENTITY_INSERT [dbo].[Page_Template] OFF
SET IDENTITY_INSERT [dbo].[Page] ON 

INSERT [dbo].[Page] ([Page_ID], [Page_Template_ID], [Scheme_ID], [Title], [Name], [DisplayOrder], [Next_Page_ID]) VALUES (19, 1, 7, N'Contact Details', N'Contact Details', 1, 20)
INSERT [dbo].[Page] ([Page_ID], [Page_Template_ID], [Scheme_ID], [Title], [Name], [DisplayOrder], [Next_Page_ID]) VALUES (20, 1, 7, N'Pet Details', N'Pet Details', 2, 21)
INSERT [dbo].[Page] ([Page_ID], [Page_Template_ID], [Scheme_ID], [Title], [Name], [DisplayOrder], [Next_Page_ID]) VALUES (21, 1, 7, N'Insurance Details', N'Insurance Details', 3, NULL)
INSERT [dbo].[Page] ([Page_ID], [Page_Template_ID], [Scheme_ID], [Title], [Name], [DisplayOrder], [Next_Page_ID]) VALUES (22, 1, 8, N'Bouncy Castle Details', N'BouncyCastleDetails', 1, NULL)
SET IDENTITY_INSERT [dbo].[Page] OFF
SET IDENTITY_INSERT [dbo].[Question_Possible_Answer] ON 

INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (36, 48, N'Cat', N'Cat', 1)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (37, 48, N'Dog', N'Dog', 2)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (38, 48, N'Other', N'Other', 3)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (39, 50, N'Yes', N'Yes', 1)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (40, 50, N'No', N'No', 2)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (41, 61, N'Gold', N'Gold', 1)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (42, 61, N'Silver', N'Silver', 2)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (43, 61, N'Bronze', N'Bronze', 3)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (44, 62, N'Yes', N'Yes', 1)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (45, 62, N'No', N'No', 2)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (46, 64, N'Bounce King', N'bk', 1)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (47, 64, N'Bounce Right', N'br', 2)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (48, 64, N'Bills Bouncey Castles', N'bbc', 3)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (49, 67, N'Bounce King XL', N'BKXL', 1)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (50, 67, N'Bounce King L', N'BKL', 2)
INSERT [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID], [Question_ID], [AnswerText], [AnswerValue], [DisplayOrder]) VALUES (51, 67, N'Bounce King M', N'BKM', 3)
SET IDENTITY_INSERT [dbo].[Question_Possible_Answer] OFF
SET IDENTITY_INSERT [dbo].[Quote_Question_Answer] ON 

INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (77, 21, 57, NULL, N'Mr Test Person')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (78, 21, 58, NULL, N'01/01/1996')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (79, 21, 59, NULL, N'test@test.com')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (80, 21, 60, NULL, N'020 300 4001')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (81, 21, 48, 36, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (82, 21, 49, NULL, N'billy')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (83, 21, 50, 40, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (84, 21, 51, NULL, N'1')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (85, 21, 54, NULL, N'01/10/2014')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (86, 21, 61, 41, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (87, 21, 62, 44, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (88, 21, 63, NULL, N'02/11/2014')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (89, 24, 57, NULL, N'test')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (90, 24, 58, NULL, N'01/01/1979')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (91, 24, 59, NULL, N'test@test.com')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (92, 24, 60, NULL, N'02088552472')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (93, 24, 48, 36, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (94, 24, 49, NULL, N'test')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (95, 24, 50, 40, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (96, 24, 51, NULL, N'1')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (97, 24, 54, NULL, N'01/10/2014')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (98, 24, 61, 41, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (99, 24, 62, 45, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (100, 24, 63, NULL, N'30/10/2014')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (101, 25, 57, NULL, N'test')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (102, 25, 58, NULL, N'01/01/1990')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (103, 25, 59, NULL, N'test@test.com')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (104, 25, 60, NULL, N'07900100200')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (105, 25, 48, 36, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (106, 25, 49, NULL, N'test')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (107, 25, 50, 40, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (108, 25, 51, NULL, N'1')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (109, 25, 54, NULL, N'01/05/2015')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (110, 25, 61, 43, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (111, 25, 63, NULL, N'24/06/2015')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (112, 26, 64, 46, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (113, 26, 67, 49, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (114, 26, 66, NULL, N'100')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (115, 27, 57, NULL, N'Mr Test Test')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (116, 27, 58, NULL, N'01/01/1980')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (117, 27, 59, NULL, N'test@test.com')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (118, 27, 60, NULL, N'020 88552472')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (119, 27, 48, 36, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (120, 27, 49, NULL, N'Test')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (121, 27, 50, 40, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (122, 27, 51, NULL, N'2')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (123, 27, 54, NULL, N'08/12/2015')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (124, 27, 61, 41, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (125, 27, 62, 44, N'')
INSERT [dbo].[Quote_Question_Answer] ([Quote_Question_Answer_ID], [Quote_ID], [Question_ID], [Question_Possible_Answer_ID], [Answer]) VALUES (126, 27, 63, NULL, N'20/12/2015')
SET IDENTITY_INSERT [dbo].[Quote_Question_Answer] OFF
SET IDENTITY_INSERT [dbo].[Page_Question] ON 

INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (48, 19, 57, 1, N'What is your name?', N'name', 1)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (49, 19, 58, 2, N'What is your date of birth?', N'date of birth', 1)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (50, 19, 59, 3, N'What is your email address?', N'email address', 1)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (51, 19, 60, 4, N'What is your phone number?', N'Customer Phone', 1)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (52, 20, 48, 1, N'What type of pet do you have?', N'pet type', 1)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (53, 20, 49, 2, N'What is your pets name?', N'pet name', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (54, 20, 50, 3, N'Do you let your cat out?', N'let your cat out', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (55, 20, 51, 4, N'How many scratch posts do you have?', N'number of scratch posts', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (56, 20, 52, 5, N'How often do you walk your dog?', N'number of times you walk your dog', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (57, 20, 53, 6, N'What type of pet is it?', N'type of pet', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (58, 20, 54, 7, N'When did you buy your cat?', N'date you bought your cat', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (59, 20, 55, 8, N'When did you buy your dog?', N'date you bought your dog', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (60, 20, 56, 9, N'When did you buy your pet?', N'date you bought your pet', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (61, 21, 61, 1, N'What level of cover do you want?', N'cover level', 1)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (62, 21, 62, 2, N'Do you want legal cover?', N'include legal cover', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (63, 21, 63, 3, N'When do you want the cover to start?', N'cover start', 1)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (64, 22, 64, 1, N'What make is your bouncy castle', N'Bouncy castle make', 1)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (65, 22, 67, 2, N'What Bounce King make is the castle', N'Bounce king make', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (66, 22, 68, 3, N'What model of bouncy castle is it', N'Other make model', 0)
INSERT [dbo].[Page_Question] ([Page_Question_ID], [Page_ID], [Question_ID], [DisplayOrder], [QuestionText], [QuestionName], [DefaultShow]) VALUES (67, 22, 66, 4, N'How much is your bouncy castle worth', N'Bouncy castle value', 1)
SET IDENTITY_INSERT [dbo].[Page_Question] OFF
SET IDENTITY_INSERT [dbo].[Page_Question_Conditional_Display] ON 

INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (17, 52, 53, 36, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (18, 52, 53, 37, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (19, 52, 53, 38, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (20, 52, 54, 36, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (21, 52, 55, 36, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (22, 52, 56, 37, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (23, 52, 57, 38, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (24, 52, 58, 36, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (25, 52, 59, 37, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (26, 61, 62, 41, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (27, 52, 60, 38, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (28, 64, 65, 46, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (29, 64, 66, 47, 0)
INSERT [dbo].[Page_Question_Conditional_Display] ([Page_Question_Conditional_Display_ID], [Source_Page_Question_ID], [Target_Page_Question_ID], [Trigger_Question_Possible_Answer_ID], [Hide]) VALUES (30, 64, 66, 48, 0)
SET IDENTITY_INSERT [dbo].[Page_Question_Conditional_Display] OFF
SET IDENTITY_INSERT [dbo].[Rendered_Page] ON 

INSERT [dbo].[Rendered_Page] ([Rendered_Page_ID], [Page_ID], [PageContent], [LastRenderDate]) VALUES (18, 19, N'@model InsuranceEngine.Web.Models.Questionnaire.PageVM

@using (Html.BeginForm())
{
    
    @Html.AntiForgeryToken()
    
    <div class="row">

        @if (!Html.ViewData.ModelState.IsValid)
        {
            <div class="alert alert-danger" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>
        }
        else
        {
            <div class="alert alert-danger" style="display:none" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>            
        }

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">@Model.Page.PageTitle</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    
                    @foreach (var question in @Model.Questions)
                    {
                        Html.RenderPartial(question.Question.QuestionTemplatePath, question);
                    }                    

                    <div class="form-group">
                        <div class="col-sm-1 col-xs-1">
                            @Html.ActionLink("Back", "PreviousPage", null, new { @class = "btn btn-default" })
                        </div>
                        <div class="col-sm-3 col-xs-7">
                            &nbsp;
                        </div>
                        <div class="col-sm-1 col-xs-1">
                            <input type="submit" class="btn btn-primary" value="Next" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
}

<script type="text/javascript">
    $(document).ready(function () {
        var validator = $("form").bind("invalid-form.validate", function (f, v) {
            if (v.errorList.length > 0) {
                $(''#summary'').show();
            }
            else {
                $(''#summary'').hide();
            }
        });
    });
</script>



', CAST(N'2017-09-16T12:53:08.540' AS DateTime))
INSERT [dbo].[Rendered_Page] ([Rendered_Page_ID], [Page_ID], [PageContent], [LastRenderDate]) VALUES (19, 20, N'@model InsuranceEngine.Web.Models.Questionnaire.PageVM

@using (Html.BeginForm())
{
    
    @Html.AntiForgeryToken()
    
    <div class="row">

        @if (!Html.ViewData.ModelState.IsValid)
        {
            <div class="alert alert-danger" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>
        }
        else
        {
            <div class="alert alert-danger" style="display:none" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>            
        }

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">@Model.Page.PageTitle</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    
                    @foreach (var question in @Model.Questions)
                    {
                        Html.RenderPartial(question.Question.QuestionTemplatePath, question);
                    }                    

                    <div class="form-group">
                        <div class="col-sm-1 col-xs-1">
                            @Html.ActionLink("Back", "PreviousPage", null, new { @class = "btn btn-default" })
                        </div>
                        <div class="col-sm-3 col-xs-7">
                            &nbsp;
                        </div>
                        <div class="col-sm-1 col-xs-1">
                            <input type="submit" class="btn btn-primary" value="Next" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
}

<script type="text/javascript">
    $(document).ready(function () {
        var validator = $("form").bind("invalid-form.validate", function (f, v) {
            if (v.errorList.length > 0) {
                $(''#summary'').show();
            }
            else {
                $(''#summary'').hide();
            }
        });
    });
</script>



', CAST(N'2017-09-16T12:53:08.547' AS DateTime))
INSERT [dbo].[Rendered_Page] ([Rendered_Page_ID], [Page_ID], [PageContent], [LastRenderDate]) VALUES (20, 21, N'@model InsuranceEngine.Web.Models.Questionnaire.PageVM

@using (Html.BeginForm())
{
    
    @Html.AntiForgeryToken()
    
    <div class="row">

        @if (!Html.ViewData.ModelState.IsValid)
        {
            <div class="alert alert-danger" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>
        }
        else
        {
            <div class="alert alert-danger" style="display:none" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>            
        }

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">@Model.Page.PageTitle</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    
                    @foreach (var question in @Model.Questions)
                    {
                        Html.RenderPartial(question.Question.QuestionTemplatePath, question);
                    }                    

                    <div class="form-group">
                        <div class="col-sm-1 col-xs-1">
                            @Html.ActionLink("Back", "PreviousPage", null, new { @class = "btn btn-default" })
                        </div>
                        <div class="col-sm-3 col-xs-7">
                            &nbsp;
                        </div>
                        <div class="col-sm-1 col-xs-1">
                            <input type="submit" class="btn btn-primary" value="Next" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
}

<script type="text/javascript">
    $(document).ready(function () {
        var validator = $("form").bind("invalid-form.validate", function (f, v) {
            if (v.errorList.length > 0) {
                $(''#summary'').show();
            }
            else {
                $(''#summary'').hide();
            }
        });
    });
</script>



', CAST(N'2017-09-16T12:53:08.553' AS DateTime))
INSERT [dbo].[Rendered_Page] ([Rendered_Page_ID], [Page_ID], [PageContent], [LastRenderDate]) VALUES (21, 22, N'@model InsuranceEngine.Web.Models.Questionnaire.PageVM

@using (Html.BeginForm())
{
    
    @Html.AntiForgeryToken()
    
    <div class="row">

        @if (!Html.ViewData.ModelState.IsValid)
        {
            <div class="alert alert-danger" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>
        }
        else
        {
            <div class="alert alert-danger" style="display:none" id="summary">
                <strong>Please correct the following</strong>;
                @Html.ValidationSummary()
            </div>            
        }

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">@Model.Page.PageTitle</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    
                    @foreach (var question in @Model.Questions)
                    {
                        Html.RenderPartial(question.Question.QuestionTemplatePath, question);
                    }                    

                    <div class="form-group">
                        <div class="col-sm-1 col-xs-1">
                            @Html.ActionLink("Back", "PreviousPage", null, new { @class = "btn btn-default" })
                        </div>
                        <div class="col-sm-3 col-xs-7">
                            &nbsp;
                        </div>
                        <div class="col-sm-1 col-xs-1">
                            <input type="submit" class="btn btn-primary" value="Next" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
}

<script type="text/javascript">
    $(document).ready(function () {
        var validator = $("form").bind("invalid-form.validate", function (f, v) {
            if (v.errorList.length > 0) {
                $(''#summary'').show();
            }
            else {
                $(''#summary'').hide();
            }
        });
    });
</script>



', CAST(N'2017-09-16T12:52:59.310' AS DateTime))
SET IDENTITY_INSERT [dbo].[Rendered_Page] OFF
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (1, N'Required', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (2, N'Regex', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (3, N'Date (Valid Format)', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (4, N'Date (After Today)', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (5, N'Date (Before Today)', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (6, N'Date (Maximum Fixed Number of Days From Today)', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (7, N'Date (Maximum Fixed Number of Years From Today)', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (10, N'Numeric', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (11, N'Date (Minimum Fixed Number of Days From Today)', NULL)
INSERT [dbo].[Validation_Type] ([Validation_Type_ID], [Name], [DataType]) VALUES (12, N'Date (Minimum Fixed Number of Years From Today)', NULL)
SET IDENTITY_INSERT [dbo].[Page_Question_Validation] ON 

INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (41, 48, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (42, 49, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (43, 49, 3, N'The $$QuestionName$$ field must be a valid date', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (44, 49, 5, N'Your $$QuestionName$$ must be in the past', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (45, 49, 12, N'You must be at least 18 years old', N'-18')
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (46, 50, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (47, 50, 2, N'The $$QuestionName$$ field must be a valid email address', N'[a-z0-9]+([-+._][a-z0-9]+){0,2}@.*?(\.(a(?:[cdefgilmnoqrstuwxz]|ero|(?:rp|si)a)|b(?:[abdefghijmnorstvwyz]iz)|c(?:[acdfghiklmnoruvxyz]|at|o(?:m|op))|d[ejkmoz]|e(?:[ceghrstu]|du)|f[ijkmor]|g(?:[abdefghilmnpqrstuwy]|ov)|h[kmnrtu]|i(?:[delmnoqrst]|n(?:fo|t))|j(?:[emop]|obs)|k[eghimnprwyz]|l[abcikrstuvy]|m(?:[acdeghklmnopqrstuvwxyz]|il|obi|useum)|n(?:[acefgilopruz]|ame|et)|o(?:m|rg)|p(?:[aefghklmnrstwy]|ro)|qa|r[eosuw]|s[abcdeghijklmnortuvyz]|t(?:[cdfghjklmnoprtvwz]|(?:rav)?el)|u[agkmsyz]|v[aceginu]|w[fs]|y[etu]|z[amw])\b){1,2}')
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (48, 52, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (49, 53, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (50, 54, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (51, 55, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (52, 55, 10, N'The $$QuestionName$$ field must be numeric', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (53, 56, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (54, 56, 10, N'The $$QuestionName$$ field must be numeric', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (55, 57, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (56, 51, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (57, 51, 2, N'The $$QuestionName$$ field must be a valid phone number', N'^\s*\(?(020[78]?\)? ?[1-9][0-9]{2,3} ?[0-9]{4})|(0[1-8][0-9]{3}\)? ?[1-9][0-9]{2} ?[0-9]{3})\s*$')
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (58, 58, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (59, 58, 3, N'The $$QuestionName$$ field must be a valid date', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (60, 58, 5, N'You must have purchased your cat in the past', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (61, 59, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (62, 59, 3, N'The $$QuestionName$$ field must be a valid date', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (63, 59, 5, N'You must have purchased your dog in the past', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (64, 60, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (65, 60, 3, N'The $$QuestionName$$ field must be a valid date', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (66, 60, 5, N'You must have bought your pet in the past', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (67, 61, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (68, 62, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (69, 63, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (70, 63, 3, N'The $$QuestionName$$ field must be a valid date', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (71, 63, 4, N'$$QuestionName$$ must be in the future', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (72, 63, 6, N'$$QuestionName$$ must be within 30 days', N'30')
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (73, 64, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (74, 65, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (75, 66, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (76, 67, 1, N'Please complete the $$QuestionName$$ field', NULL)
INSERT [dbo].[Page_Question_Validation] ([Page_Question_Validation_ID], [Page_Question_ID], [Validation_Type_ID], [ErrorMessage], [ValidationExpression]) VALUES (77, 67, 10, N'The $$QuestionName$$ field must be a valid number', NULL)
SET IDENTITY_INSERT [dbo].[Page_Question_Validation] OFF
