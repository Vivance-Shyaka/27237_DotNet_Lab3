using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class QuestionController
{
    private readonly QuestionRepository _questions;
    private readonly QuizRepository _quizzes;

    public QuestionController(QuestionRepository questions, QuizRepository quizzes)
    {
        _questions = questions;
        _quizzes = quizzes;
    }

    private static int CurrentUserId =>
        Session.CurrentUser?.UserID ?? throw new InvalidOperationException("No user is logged in.");

    public DataTable GetQuestions(int quizId)
    {
        EnsureQuizOwner(quizId);
        return _questions.GetByQuiz(quizId);
    }

    public List<AnswerOption> GetOptions(int questionId)
    {
        Question question = GetQuestion(questionId);
        EnsureQuizOwner(question.QuizID);
        return _questions.GetOptions(questionId);
    }

    public void AddQuestion(int quizId, string text, string type, int marks,
                            List<AnswerOption> options)
    {
        EnsureEditable(quizId);
        text = text.Trim();
        List<AnswerOption> clean = Validate(text, type, marks, options);

        _questions.Insert(new Question
        {
            QuizID = quizId,
            QuestionText = text,
            QuestionType = type,
            Marks = marks
        }, clean);
    }

    public void EditQuestion(int questionId, string text, string type, int marks,
                             List<AnswerOption> options)
    {
        Question existing = GetQuestion(questionId);
        EnsureEditable(existing.QuizID);
        text = text.Trim();
        List<AnswerOption> clean = Validate(text, type, marks, options);

        _questions.Update(new Question
        {
            QuestionID = questionId,
            QuizID = existing.QuizID,
            QuestionText = text,
            QuestionType = type,
            Marks = marks
        }, clean);
    }

    public void RemoveQuestion(int questionId)
    {
        Question existing = GetQuestion(questionId);
        EnsureEditable(existing.QuizID);
        _questions.Delete(questionId);
    }

    private Question GetQuestion(int questionId)
    {
        if (questionId <= 0)
            throw new ArgumentException("Select a question first.");
        return _questions.GetById(questionId) ?? throw new ArgumentException("Question not found.");
    }

    private void EnsureQuizOwner(int quizId)
    {
        int? owner = _quizzes.GetOwnerId(quizId);
        if (owner == null)
            throw new ArgumentException("Quiz not found.");
        if (owner != CurrentUserId)
            throw new ArgumentException("You can only manage the questions of your own quizzes.");
    }

    // Owner check + "no attempts yet" rule
    private void EnsureEditable(int quizId)
    {
        EnsureQuizOwner(quizId);

        int attempts = _quizzes.CountAttempts(quizId);
        if (attempts > 0)
            throw new ArgumentException(
                "Learners have already made " + attempts + " attempt(s) on this quiz, so its questions " +
                "can no longer be changed. Unpublish the quiz or create a new one.");
    }

    // Checks the question and returns the cleaned list of options to save
    private static List<AnswerOption> Validate(string text, string type, int marks,
                                               List<AnswerOption> options)
    {
        if (text.Length == 0)
            throw new ArgumentException("Enter the question text.");
        if (text.Length > 1000)
            throw new ArgumentException("The question is too long (maximum 1000 characters).");
        if (marks < 1 || marks > 100)
            throw new ArgumentException("Marks must be between 1 and 100.");

        // trim every option and ignore the empty ones
        List<AnswerOption> clean = options
            .Select(o => new AnswerOption { OptionText = o.OptionText.Trim(), IsCorrect = o.IsCorrect })
            .Where(o => o.OptionText.Length > 0)
            .ToList();

        if (clean.Any(o => o.OptionText.Length > 255))
            throw new ArgumentException("An option is too long (maximum 255 characters).");

        switch (type)
        {
            case QuestionTypes.ShortAnswer:
                return new List<AnswerOption>();          // no options, graded by the instructor

            case QuestionTypes.TrueFalse:
                {
                    bool trueIsCorrect = clean.Any(o => o.IsCorrect && o.OptionText == "True");
                    bool falseIsCorrect = clean.Any(o => o.IsCorrect && o.OptionText == "False");

                    if (trueIsCorrect == falseIsCorrect)
                        throw new ArgumentException("Choose the correct answer: True or False.");

                    return new List<AnswerOption>
                {
                    new AnswerOption { OptionText = "True", IsCorrect = trueIsCorrect },
                    new AnswerOption { OptionText = "False", IsCorrect = falseIsCorrect }
                };
                }

            case QuestionTypes.SingleChoice:
                if (clean.Count < 2)
                    throw new ArgumentException("Enter at least two options.");
                if (clean.Select(o => o.OptionText.ToLowerInvariant()).Distinct().Count() != clean.Count)
                    throw new ArgumentException("The options must be different from each other.");
                if (clean.Count(o => o.IsCorrect) != 1)
                    throw new ArgumentException(
                        "Select the correct answer among the options you entered.");
                return clean;

            default:
                throw new ArgumentException("Choose a valid question type.");
        }
    }
}