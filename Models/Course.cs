namespace WLAMS.Models;

public static class CourseLevels
{
    public const string Beginner = "Beginner";
    public const string Intermediate = "Intermediate";
    public const string Advanced = "Advanced";
}

public class Course
{
    public int CourseID { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int CategoryID { get; set; }
    public int InstructorID { get; set; }
    public string CourseLevel { get; set; } = CourseLevels.Beginner;
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
}