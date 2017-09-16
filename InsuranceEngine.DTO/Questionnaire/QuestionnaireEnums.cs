namespace InsuranceEngine.DTO.Questionnaire.QuestionnaireEnums
{

    public enum ValidationTypes
    {
        Required = 1,
        Regex = 2,
        Date_Valid_Format = 3,
        Date_After_Today = 4,
        Date_Before_Today = 5,
        Date_Max_Fixed_Number_of_Days = 6,
        Date_Max_Fixed_Number_of_Years = 7,        
        Numeric = 10,
        Date_Min_Fixed_Number_of_Days = 11,
        Date_Min_Fixed_Number_of_Years = 12
    }

    public enum NodeTypes
    {
        Scheme = 1,
        PageFolder = 2,
        Page = 3,
        PageQuestion = 4,
        QuestionFolder = 5,
        Question = 6,
        RenderedPageFolder = 7,
        RenderedPage = 8
    }

}
