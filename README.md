# WLAMS: Web-Based Learning and Assessment Management System

A Windows Forms desktop application (C#, ADO.NET, MariaDB) where learners study online and take quizzes at their own pace. Instructors create courses, lessons and quizzes, and administrators manage users and categories.

Built for the **.NET** course, following the **MVC architecture**.

---

## Features

### Done so far

| Area | Features |
|---|---|
| **Accounts** | Register as Learner or Instructor, login/logout, hashed passwords, input validation |
| **Instructor approval** | New instructor accounts stay *Pending* until an administrator approves or rejects them |
| **Admin** | Dashboard, **Categories CRUD**, **Users CRUD** (add, edit, delete, activate/deactivate, reset password, approve/reject instructors), search |
| **Instructor** | Dashboard, **Courses CRUD** (draft / publish / unpublish), **Lessons CRUD** per course, search |
| **Business rules** | A course needs at least one lesson before publishing; a course with enrolled learners cannot be deleted; instructors manage only their own courses; admins cannot lock themselves out |

### Planned

- Quizzes and Questions CRUD (single choice, true/false, short answer)
- Learner dashboard: course catalogue, enrolment, lessons, progress tracking
- Taking quizzes: timer, automatic grading, results and attempt history
- Manual grading of short answers, gradebook and reports

---

## Technology stack

| Area | Technology |
|---|---|
| Language | C# |
| Desktop framework | .NET Windows Forms |
| Database | MariaDB (XAMPP) |
| Data access | ADO.NET with `MySqlConnector` |
| Database tool | phpMyAdmin |
| Architecture | MVC (Model / View / Controller) |

---

## Architecture (MVC)

```
WLAMS
├── Models        Entity classes + repositories (all SQL / ADO.NET lives here)
├── Views         Windows Forms (no SQL, no business rules)
├── Controllers   Validation and business rules; call the repositories
├── Helpers       DbConfig, PasswordHelper, Session, Validator
├── Program.cs
Database
└── wlams_db.sql  Database script with sample data
```

- **Model**: `User`, `Course`, `Lesson`, `Category` and their repositories use parameterised `MySqlCommand` queries.
- **View**: forms collect input and show results. They call controllers only.
- **Controller**: checks the rules (for example, ownership, duplicates, publishing rules) and throws friendly messages.

---

## Database

Database name: `wlams_db` (11 tables)

`Users`, `Categories`, `Courses`, `Lessons`, `Enrollments`, `LessonProgress`, `Quizzes`, `Questions`, `AnswerOptions`, `QuizAttempts`, `AttemptAnswers`

---

## How to run

### Requirements

- Visual Studio (with the **.NET desktop development** workload)
- XAMPP (Apache + MySQL/MariaDB)

### Steps

1. Open the **XAMPP Control Panel** and start **Apache** and **MySQL**.
2. Open <http://localhost/phpmyadmin/> → **Import** tab → choose `Database/wlams_db.sql` → **Go**. This creates `wlams_db` with sample data.
3. Open the solution in Visual Studio. The `MySqlConnector` package is restored automatically from NuGet.
4. If your MySQL user or port is different, edit `Helpers/DbConfig.cs`:
   ```csharp
   "Server=127.0.0.1;Port=3306;Database=wlams_db;User ID=root;Password=;"
   ```
5. Press **F5** to run.

---

## Sample accounts (for testing only)

These accounts come with the sample data in `wlams_db.sql`.

| Role | Username | Password |
|---|---|---|
| Administrator | `admin` | `Admin@123` |
| Instructor | `instructor` | `Instructor@123` |
| Learner | `learner` | `Learner@123` |

New users can register from the login screen. Instructor registrations wait for approval by the administrator.

> The default `root` account with an empty password is for the local XAMPP lab setup only. Use a strong password for any real deployment.

---

## Author

| | |
|---|---|
| **Name** | Vivance Shyaka |
| **Email** | shyakavivance1@gmail.com |
| **GitHub** | [Vivance-Shyaka](https://github.com/Vivance-Shyaka) |
| **Course** | .NET Programming |
| **Repository** | 27237_DotNet_Lab3 |

---

## Status

Work in progress. This repository is updated step by step as new modules are completed.
