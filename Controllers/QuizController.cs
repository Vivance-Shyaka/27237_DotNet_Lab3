using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class QuizController
{
    private readonly QuizRepository _quizzes;
    private readonly CourseRepository _courses;

    public QuizController(QuizRepository quizzes, CourseRepository courses)
    {
        _quizzes = quizzes;
        _courses = courses;
    }

    private static int CurrentUserId =>
        Session.CurrentUser?.UserID ?? throw new InvalidOperationException("No user is logged in.");

    public DataTable GetMyQuizzes(string keyword = "") =>
        _quizzes.GetByInstructor(CurrentUserId, keyword.Trim());

    // The instructor's own courses (for the Course list)
    public DataTable GetMyCourses() => _courses.GetByInstructor(CurrentUserId);

    public void AddQuiz(int courseId, string title, string instructions,
                        int timeLimit, int passMark, int maxAttempts)
    {
        Course? course = _courses.GetById(courseId);
        if (course == null)
            throw new ArgumentException("Choose a course. Create a course first if the list is empty.");
        if (course.InstructorID != CurrentUserId)
            throw new ArgumentException("You can only add quizzes to your own courses.");

        title = title.Trim();
        Check(title, timeLimit, passMark, maxAttempts);

        _quizzes.Insert(new Quiz
        {
            CourseID = courseId,
            Title = title,
            Instructions = instructions.Trim(),
            TimeLimitMinutes = timeLimit,
            PassMark = passMark,
            MaxAttempts = maxAttempts
        });
    }

    public void EditQuiz(int quizId, string title, string instructions,
                         int timeLimit, int passMark, int maxAttempts)
    {
        GetOwned(quizId);

        title = title.Trim();
        Check(title, timeLimit, passMark, maxAttempts);

        _quizzes.Update(new Quiz
        {
            QuizID = quizId,
            Title = title,
            Instructions = instructions.Trim(),
            TimeLimitMinutes = timeLimit,
            PassMark = passMark,
            MaxAttempts = maxAttempts
        });
    }

    public void RemoveQuiz(int quizId)
    {
        GetOwned(quizId);

        int attempts = _quizzes.CountAttempts(quizId);
        if (attempts > 0)
            throw new ArgumentException(
                "Learners have already made " + attempts + " attempt(s) on this quiz, so it cannot be " +
                "deleted. Unpublish it instead.");

        _quizzes.Delete(quizId);
    }

    public void SetPublished(int quizId, bool publish)
    {
        Quiz quiz = GetOwned(quizId);

        if (publish)
        {
            if (quiz.IsPublished)
                throw new ArgumentException("This quiz is already published.");
            if (_quizzes.CountQuestions(quizId) == 0)
                throw new ArgumentException("Add at least one question before publishing the quiz.");
        }
        else if (!quiz.IsPublished)
        {
            throw new ArgumentException("This quiz is not published.");
        }

        _quizzes.SetPublished(quizId, publish);
    }

    // Returns the quiz only if it belongs to the logged-in instructor
    private Quiz GetOwned(int quizId)
    {
        if (quizId <= 0)
            throw new ArgumentException("Select a quiz first.");

        Quiz quiz = _quizzes.GetById(quizId) ?? throw new ArgumentException("Quiz not found.");

        if (_quizzes.GetOwnerId(quizId) != CurrentUserId)
            throw new ArgumentException("You can only manage your own quizzes.");

        return quiz;
    }

    private static void Check(string title, int timeLimit, int passMark, int maxAttempts)
    {
        if (title.Length == 0)
            throw new ArgumentException("Enter the quiz title.");
        if (title.Length > 150)
            throw new ArgumentException("The quiz title is too long (maximum 150 characters).");
        if (timeLimit < 0 || timeLimit > 600)
            throw new ArgumentException("The time limit must be between 0 and 600 minutes (0 = no limit).");
        if (passMark < 1 || passMark > 100)
            throw new ArgumentException("The pass mark must be between 1 and 100 percent.");
        if (maxAttempts < 0 || maxAttempts > 50)
            throw new ArgumentException("Attempts must be between 0 and 50 (0 = unlimited).");
    }
}