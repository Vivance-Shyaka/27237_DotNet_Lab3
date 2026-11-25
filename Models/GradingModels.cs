using System.Data;

namespace WLAMS.Models;

// What the grading code needs to know about one answer
public class AnswerInfo
{
    public int AnswerID { get; set; }
    public int AttemptID { get; set; }
    public int QuestionMarks { get; set; }
    public int? MarksAwarded { get; set; }          // null = waiting for the instructor
    public string AttemptStatus { get; set; } = "";
    public int InstructorID { get; set; }
    public int PassMark { get; set; }
}

public class QuizStatistics
{
    public int Attempts { get; set; }
    public int Learners { get; set; }
    public int Pending { get; set; }
    public int Graded { get; set; }
    public int Passed { get; set; }
    public double? AveragePercent { get; set; }
    public double? HighestPercent { get; set; }
    public double? LowestPercent { get; set; }
    public DataTable Questions { get; set; } = new DataTable();

    public int? PassRatePercent =>
        Graded == 0 ? null : (int)Math.Round(100.0 * Passed / Graded);
}