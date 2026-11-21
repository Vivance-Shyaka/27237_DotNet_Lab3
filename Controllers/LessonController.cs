using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class LessonController
{
    private readonly LessonRepository _lessons;
    private readonly CourseRepository _courses;

    public LessonController(LessonRepository lessons, CourseRepository courses)
    {
        _lessons = lessons;
        _courses = courses;
    }

    public DataTable GetLessons(int courseId)
    {
        EnsureOwner(courseId);
        return _lessons.GetByCourse(courseId);
    }

    public int NextOrder(int courseId) => _lessons.NextOrder(courseId);

    public void AddLesson(int courseId, string title, string content, int order)
    {
        EnsureOwner(courseId);
        title = title.Trim();
        Check(title, order);

        _lessons.Insert(new Lesson
        {
            CourseID = courseId,
            Title = title,
            Content = content.Trim(),
            LessonOrder = order
        });
    }

    public void EditLesson(int lessonId, string title, string content, int order)
    {
        Lesson existing = GetLesson(lessonId);
        EnsureOwner(existing.CourseID);
        title = title.Trim();
        Check(title, order);

        _lessons.Update(new Lesson
        {
            LessonID = lessonId,
            Title = title,
            Content = content.Trim(),
            LessonOrder = order
        });
    }

    public void RemoveLesson(int lessonId)
    {
        Lesson existing = GetLesson(lessonId);
        EnsureOwner(existing.CourseID);
        _lessons.Delete(lessonId);
    }

    private Lesson GetLesson(int lessonId)
    {
        if (lessonId <= 0)
            throw new ArgumentException("Select a lesson first.");
        return _lessons.GetById(lessonId) ?? throw new ArgumentException("Lesson not found.");
    }

    // The course must belong to the logged-in instructor
    private void EnsureOwner(int courseId)
    {
        Course course = _courses.GetById(courseId)
                        ?? throw new ArgumentException("Course not found.");

        if (course.InstructorID != Session.CurrentUser?.UserID)
            throw new ArgumentException("You can only manage the lessons of your own courses.");
    }

    private static void Check(string title, int order)
    {
        if (title.Length == 0)
            throw new ArgumentException("Enter the lesson title.");
        if (title.Length > 150)
            throw new ArgumentException("The lesson title is too long (maximum 150 characters).");
        if (order < 1 || order > 999)
            throw new ArgumentException("The lesson number must be between 1 and 999.");
    }
}