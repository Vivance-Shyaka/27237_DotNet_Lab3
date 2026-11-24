namespace WLAMS.Models;

public class Quiz
{
    public int QuizID { get; set; }
    public int CourseID { get; set; }
    public string Title { get; set; } = "";
    public string Instructions { get; set; } = "";
    public int TimeLimitMinutes { get; set; }          // 0 = no time limit
    public int PassMark { get; set; } = 50;            // percentage
    public int MaxAttempts { get; set; } = 3;          // 0 = unlimited
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
}