using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class CourseController
{
    private readonly CourseRepository _courses;
    private readonly CategoryRepository _categories;

    public CourseController(CourseRepository courses, CategoryRepository categories)
    {
        _courses = courses;
        _categories = categories;
    }

    private static int CurrentUserId =>
        Session.CurrentUser?.UserID ?? throw new InvalidOperationException("No user is logged in.");

    public DataTable GetMyCourses(string keyword = "") =>
        _courses.GetByInstructor(CurrentUserId, keyword.Trim());

    public DataTable GetCategories() => _categories.GetAll();

    public void AddCourse(string title, string description, int categoryId, string level)
    {
        title = title.Trim();
        description = description.Trim();
        Check(title, categoryId, level);

        _courses.Insert(new Course
        {
            Title = title,
            Description = description,
            CategoryID = categoryId,
            InstructorID = CurrentUserId,
            CourseLevel = level
        });
    }

    public void EditCourse(int courseId, string title, string description,
                           int categoryId, string level)
    {
        GetOwned(courseId);

        title = title.Trim();
        description = description.Trim();
        Check(title, categoryId, level);

        _courses.Update(new Course
        {
            CourseID = courseId,
            Title = title,
            Description = description,
            CategoryID = categoryId,
            CourseLevel = level
        });
    }

    public void RemoveCourse(int courseId)
    {
        GetOwned(courseId);

        int learners = _courses.CountEnrollments(courseId);
        if (learners > 0)
            throw new ArgumentException(
                learners + " learner(s) are enrolled in this course, so it cannot be deleted. " +
                "Unpublish it instead.");

        _courses.Delete(courseId);
    }

    public void SetPublished(int courseId, bool publish)
    {
        Course course = GetOwned(courseId);

        if (publish)
        {
            if (course.IsPublished)
                throw new ArgumentException("This course is already published.");
            if (_courses.CountLessons(courseId) == 0)
                throw new ArgumentException("Add at least one lesson before publishing the course.");
        }
        else if (!course.IsPublished)
        {
            throw new ArgumentException("This course is not published.");
        }

        _courses.SetPublished(courseId, publish);
    }

    // Returns the course only if it belongs to the logged-in instructor
    private Course GetOwned(int courseId)
    {
        if (courseId <= 0)
            throw new ArgumentException("Select a course first.");

        Course course = _courses.GetById(courseId)
                        ?? throw new ArgumentException("Course not found.");

        if (course.InstructorID != CurrentUserId)
            throw new ArgumentException("You can only manage your own courses.");

        return course;
    }

    private static void Check(string title, int categoryId, string level)
    {
        if (title.Length == 0)
            throw new ArgumentException("Enter the course title.");
        if (title.Length > 150)
            throw new ArgumentException("The course title is too long (maximum 150 characters).");
        if (categoryId <= 0)
            throw new ArgumentException("Choose a category. Ask the administrator to add one if the list is empty.");
        if (level != CourseLevels.Beginner && level != CourseLevels.Intermediate &&
            level != CourseLevels.Advanced)
            throw new ArgumentException("Choose a valid level.");
    }
}