using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class LearnerController
{
    private readonly EnrollmentRepository _enrollments;
    private readonly ProgressRepository _progress;
    private readonly CourseRepository _courses;
    private readonly LessonRepository _lessons;

    public LearnerController(EnrollmentRepository enrollments, ProgressRepository progress,
                             CourseRepository courses, LessonRepository lessons)
    {
        _enrollments = enrollments;
        _progress = progress;
        _courses = courses;
        _lessons = lessons;
    }

    // Convenience: builds the controller with all its repositories
    public static LearnerController Create() => new LearnerController(
        new EnrollmentRepository(DbConfig.ConnectionString),
        new ProgressRepository(DbConfig.ConnectionString),
        new CourseRepository(DbConfig.ConnectionString),
        new LessonRepository(DbConfig.ConnectionString));

    private static int CurrentUserId =>
        Session.CurrentUser?.UserID ?? throw new InvalidOperationException("No user is logged in.");

    // ---------- catalogue and enrolment ----------

    public DataTable GetCatalogue(string keyword = "") =>
        _enrollments.GetCatalogue(CurrentUserId, keyword.Trim());

    public void Enroll(int courseId)
    {
        if (Session.CurrentUser?.Role != Roles.Learner)
            throw new ArgumentException("Only learners can enroll in courses.");
        if (courseId <= 0)
            throw new ArgumentException("Select a course first.");

        Course course = _courses.GetById(courseId) ?? throw new ArgumentException("Course not found.");
        if (!course.IsPublished)
            throw new ArgumentException("This course is not available.");
        if (_enrollments.IsEnrolled(CurrentUserId, courseId))
            throw new ArgumentException("You are already enrolled in this course.");

        _enrollments.Enroll(CurrentUserId, courseId);
    }

    public void Unenroll(int courseId)
    {
        if (courseId <= 0)
            throw new ArgumentException("Select a course first.");
        if (!_enrollments.IsEnrolled(CurrentUserId, courseId))
            throw new ArgumentException("You are not enrolled in this course.");

        _enrollments.Unenroll(CurrentUserId, courseId);
    }

    // ---------- my courses (with a calculated Progress column) ----------

    public DataTable GetMyCourses(string keyword = "")
    {
        DataTable table = _enrollments.GetMyCourses(CurrentUserId, keyword.Trim());

        table.Columns.Add("Progress", typeof(string));
        foreach (DataRow row in table.Rows)
        {
            int total = Convert.ToInt32(row["Lessons"]);
            int done = Convert.ToInt32(row["Completed"]);
            int percent = total == 0 ? 0 : (int)Math.Round(100.0 * done / total);
            row["Progress"] = done + " / " + total + " (" + percent + "%)";
        }
        return table;
    }

    // ---------- studying ----------

    public DataTable GetStudyLessons(int courseId)
    {
        EnsureCanStudy(courseId);
        return _progress.GetLessonsWithProgress(CurrentUserId, courseId);
    }

    public (int Completed, int Total, int Percent) GetProgress(int courseId)
    {
        EnsureCanStudy(courseId);
        int total = _courses.CountLessons(courseId);
        int completed = _progress.CountCompleted(CurrentUserId, courseId);
        int percent = total == 0 ? 0 : (int)Math.Round(100.0 * completed / total);
        return (completed, total, percent);
    }

    public void SetLessonCompleted(int lessonId, bool completed)
    {
        if (lessonId <= 0)
            throw new ArgumentException("Select a lesson first.");

        Lesson lesson = _lessons.GetById(lessonId) ?? throw new ArgumentException("Lesson not found.");
        EnsureCanStudy(lesson.CourseID);

        if (completed)
            _progress.MarkComplete(CurrentUserId, lessonId);
        else
            _progress.MarkIncomplete(CurrentUserId, lessonId);
    }

    // The course must be published and the learner must be enrolled
    private void EnsureCanStudy(int courseId)
    {
        Course course = _courses.GetById(courseId) ?? throw new ArgumentException("Course not found.");
        if (!course.IsPublished)
            throw new ArgumentException("This course is not available.");
        if (!_enrollments.IsEnrolled(CurrentUserId, courseId))
            throw new ArgumentException("You are not enrolled in this course.");
    }
}