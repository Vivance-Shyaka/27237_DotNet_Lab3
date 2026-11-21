namespace WLAMS.Models;

public class Lesson
{
    public int LessonID { get; set; }
    public int CourseID { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public int LessonOrder { get; set; } = 1;
}