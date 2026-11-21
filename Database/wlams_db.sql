-- =====================================================================
--  WLAMS - Web-Based Learning and Assessment Management System
--  Database script for MariaDB (XAMPP) - import with phpMyAdmin
-- =====================================================================

DROP DATABASE IF EXISTS wlams_db;
CREATE DATABASE wlams_db
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;
USE wlams_db;

-- ---------------------------------------------------------------------
-- 1. Users (Admin, Instructor, Learner)
-- ---------------------------------------------------------------------
CREATE TABLE Users
(
    UserID        INT          NOT NULL AUTO_INCREMENT,
    FullName      VARCHAR(100) NOT NULL,
    Email         VARCHAR(100) NOT NULL,
    Username      VARCHAR(50)  NOT NULL,
    PasswordHash  CHAR(64)     NOT NULL,                 -- SHA-256 hex
    Role          ENUM('Admin','Instructor','Learner') NOT NULL DEFAULT 'Learner',
    IsActive      TINYINT(1)   NOT NULL DEFAULT 1,
    ApprovalStatus ENUM('Approved','Pending','Rejected') NOT NULL DEFAULT 'Approved',
    CreatedAt     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (UserID),
    UNIQUE KEY uq_users_email (Email),
    UNIQUE KEY uq_users_username (Username)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 2. Categories
-- ---------------------------------------------------------------------
CREATE TABLE Categories
(
    CategoryID    INT          NOT NULL AUTO_INCREMENT,
    CategoryName  VARCHAR(100) NOT NULL,
    PRIMARY KEY (CategoryID),
    UNIQUE KEY uq_categories_name (CategoryName)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 3. Courses
-- ---------------------------------------------------------------------
CREATE TABLE Courses
(
    CourseID      INT          NOT NULL AUTO_INCREMENT,
    Title         VARCHAR(150) NOT NULL,
    Description   TEXT         NULL,
    CategoryID    INT          NOT NULL,
    InstructorID  INT          NOT NULL,
    CourseLevel   ENUM('Beginner','Intermediate','Advanced') NOT NULL DEFAULT 'Beginner',
    IsPublished   TINYINT(1)   NOT NULL DEFAULT 0,
    CreatedAt     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (CourseID),
    CONSTRAINT fk_courses_category   FOREIGN KEY (CategoryID)   REFERENCES Categories (CategoryID),
    CONSTRAINT fk_courses_instructor FOREIGN KEY (InstructorID) REFERENCES Users (UserID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 4. Lessons
-- ---------------------------------------------------------------------
CREATE TABLE Lessons
(
    LessonID      INT          NOT NULL AUTO_INCREMENT,
    CourseID      INT          NOT NULL,
    Title         VARCHAR(150) NOT NULL,
    Content       TEXT         NULL,
    LessonOrder   INT          NOT NULL DEFAULT 1,
    CreatedAt     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (LessonID),
    CONSTRAINT fk_lessons_course FOREIGN KEY (CourseID) REFERENCES Courses (CourseID) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 5. Enrollments (Learner <-> Course)
-- ---------------------------------------------------------------------
CREATE TABLE Enrollments
(
    EnrollmentID  INT      NOT NULL AUTO_INCREMENT,
    UserID        INT      NOT NULL,
    CourseID      INT      NOT NULL,
    EnrolledAt    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (EnrollmentID),
    UNIQUE KEY uq_enrollment (UserID, CourseID),                -- no double enrolment
    CONSTRAINT fk_enroll_user   FOREIGN KEY (UserID)   REFERENCES Users (UserID)     ON DELETE CASCADE,
    CONSTRAINT fk_enroll_course FOREIGN KEY (CourseID) REFERENCES Courses (CourseID) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 6. LessonProgress (which lessons a learner has completed)
-- ---------------------------------------------------------------------
CREATE TABLE LessonProgress
(
    ProgressID    INT      NOT NULL AUTO_INCREMENT,
    UserID        INT      NOT NULL,
    LessonID      INT      NOT NULL,
    CompletedAt   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ProgressID),
    UNIQUE KEY uq_progress (UserID, LessonID),
    CONSTRAINT fk_progress_user   FOREIGN KEY (UserID)   REFERENCES Users (UserID)     ON DELETE CASCADE,
    CONSTRAINT fk_progress_lesson FOREIGN KEY (LessonID) REFERENCES Lessons (LessonID) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 7. Quizzes
-- ---------------------------------------------------------------------
CREATE TABLE Quizzes
(
    QuizID           INT          NOT NULL AUTO_INCREMENT,
    CourseID         INT          NOT NULL,
    Title            VARCHAR(150) NOT NULL,
    Instructions     TEXT         NULL,
    TimeLimitMinutes INT          NOT NULL DEFAULT 0,           -- 0 = no time limit
    PassMark         INT          NOT NULL DEFAULT 50,          -- percentage
    MaxAttempts      INT          NOT NULL DEFAULT 3,           -- 0 = unlimited
    IsPublished      TINYINT(1)   NOT NULL DEFAULT 0,
    CreatedAt        DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (QuizID),
    CONSTRAINT fk_quizzes_course FOREIGN KEY (CourseID) REFERENCES Courses (CourseID) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 8. Questions
-- ---------------------------------------------------------------------
CREATE TABLE Questions
(
    QuestionID    INT  NOT NULL AUTO_INCREMENT,
    QuizID        INT  NOT NULL,
    QuestionText  TEXT NOT NULL,
    QuestionType  ENUM('SingleChoice','TrueFalse','ShortAnswer') NOT NULL DEFAULT 'SingleChoice',
    Marks         INT  NOT NULL DEFAULT 1,
    PRIMARY KEY (QuestionID),
    CONSTRAINT fk_questions_quiz FOREIGN KEY (QuizID) REFERENCES Quizzes (QuizID) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 9. AnswerOptions (choices of SingleChoice / TrueFalse questions)
-- ---------------------------------------------------------------------
CREATE TABLE AnswerOptions
(
    OptionID      INT          NOT NULL AUTO_INCREMENT,
    QuestionID    INT          NOT NULL,
    OptionText    VARCHAR(255) NOT NULL,
    IsCorrect     TINYINT(1)   NOT NULL DEFAULT 0,
    PRIMARY KEY (OptionID),
    CONSTRAINT fk_options_question FOREIGN KEY (QuestionID) REFERENCES Questions (QuestionID) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 10. QuizAttempts (one row each time a learner takes a quiz)
-- ---------------------------------------------------------------------
CREATE TABLE QuizAttempts
(
    AttemptID     INT      NOT NULL AUTO_INCREMENT,
    QuizID        INT      NOT NULL,
    UserID        INT      NOT NULL,
    StartedAt     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    SubmittedAt   DATETIME NULL,
    Score         INT      NOT NULL DEFAULT 0,
    TotalMarks    INT      NOT NULL DEFAULT 0,
    IsPassed      TINYINT(1) NOT NULL DEFAULT 0,
    Status        ENUM('InProgress','PendingGrading','Graded') NOT NULL DEFAULT 'InProgress',
    PRIMARY KEY (AttemptID),
    CONSTRAINT fk_attempts_quiz FOREIGN KEY (QuizID) REFERENCES Quizzes (QuizID) ON DELETE CASCADE,
    CONSTRAINT fk_attempts_user FOREIGN KEY (UserID) REFERENCES Users (UserID)   ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------------------
-- 11. AttemptAnswers (the answers given in an attempt)
-- ---------------------------------------------------------------------
CREATE TABLE AttemptAnswers
(
    AnswerID         INT  NOT NULL AUTO_INCREMENT,
    AttemptID        INT  NOT NULL,
    QuestionID       INT  NOT NULL,
    SelectedOptionID INT  NULL,          -- for SingleChoice / TrueFalse
    AnswerText       TEXT NULL,          -- for ShortAnswer
    MarksAwarded     INT  NULL,          -- NULL = not graded yet
    PRIMARY KEY (AnswerID),
    CONSTRAINT fk_ans_attempt  FOREIGN KEY (AttemptID)  REFERENCES QuizAttempts (AttemptID) ON DELETE CASCADE,
    CONSTRAINT fk_ans_question FOREIGN KEY (QuestionID) REFERENCES Questions (QuestionID)   ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =====================================================================
--  SAMPLE DATA (so you can test straight away)
--  Passwords: Admin@123 / Instructor@123 / Learner@123  (stored as SHA-256)
-- =====================================================================
INSERT INTO Users (FullName, Email, Username, PasswordHash, Role) VALUES
('System Administrator', 'admin@wlams.com',      'admin',      'e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7', 'Admin'),
('Jane Instructor',      'jane@wlams.com',       'instructor', 'b1401718671311238747767c675dddad6fcf05730b1ff2c4edec2264caa3612a', 'Instructor'),
('Amina Learner',        'amina@wlams.com',      'learner',    'ea23d7d4906833bca6e6dc89e577e5ca8c9e4b5fd1ddc66ce60b442738737c34', 'Learner');

INSERT INTO Categories (CategoryName) VALUES
('Programming'), ('Databases'), ('Networking'), ('Mathematics');

INSERT INTO Courses (Title, Description, CategoryID, InstructorID, CourseLevel, IsPublished) VALUES
('Introduction to C#', 'Learn the basics of C# programming: variables, conditions, loops and methods.', 1, 2, 'Beginner', 1);

INSERT INTO Lessons (CourseID, Title, Content, LessonOrder) VALUES
(1, 'Variables and Data Types', 'A variable stores a value. Common C# types are int, double, string and bool.', 1),
(1, 'Conditions',               'Use if / else to make decisions in your program.', 2),
(1, 'Loops',                    'Use for, while and foreach to repeat actions.', 3);

INSERT INTO Quizzes (CourseID, Title, Instructions, TimeLimitMinutes, PassMark, MaxAttempts, IsPublished) VALUES
(1, 'C# Basics Quiz', 'Answer all questions. You have 10 minutes.', 10, 50, 3, 1);

INSERT INTO Questions (QuizID, QuestionText, QuestionType, Marks) VALUES
(1, 'Which keyword declares a constant in C#?',        'SingleChoice', 1),
(1, 'C# is a case-sensitive language.',                'TrueFalse',    1),
(1, 'Which method opens a database connection?',       'SingleChoice', 1);

INSERT INTO AnswerOptions (QuestionID, OptionText, IsCorrect) VALUES
(1, 'const',  1), (1, 'final',  0), (1, 'static', 0),
(2, 'True',   1), (2, 'False',  0),
(3, 'Open()', 1), (3, 'Start()', 0), (3, 'Run()', 0);
