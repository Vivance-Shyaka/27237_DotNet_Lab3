namespace WLAMS.Models;

public static class QuestionTypes
{
    public const string SingleChoice = "SingleChoice";
    public const string TrueFalse = "TrueFalse";
    public const string ShortAnswer = "ShortAnswer";
}

public class Question
{
    public int QuestionID { get; set; }
    public int QuizID { get; set; }
    public string QuestionText { get; set; } = "";
    public string QuestionType { get; set; } = QuestionTypes.SingleChoice;
    public int Marks { get; set; } = 1;
}