using System.Data;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class CategoryController
{
    private readonly CategoryRepository _repository;

    public CategoryController(CategoryRepository repository)
    {
        _repository = repository;
    }

    public DataTable GetCategories(string keyword = "") => _repository.GetAll(keyword.Trim());

    public void Add(string name)
    {
        name = name.Trim();
        CheckName(name, 0);
        _repository.Insert(new Category { CategoryName = name });
    }

    public void Edit(int categoryId, string name)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Select a category first.");

        name = name.Trim();
        CheckName(name, categoryId);
        _repository.Update(new Category { CategoryID = categoryId, CategoryName = name });
    }

    public void Remove(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Select a category first.");

        int courses = _repository.CountCourses(categoryId);
        if (courses > 0)
            throw new ArgumentException(
                "This category is used by " + courses + " course(s) and cannot be deleted.");

        _repository.Delete(categoryId);
    }

    private void CheckName(string name, int exceptId)
    {
        if (name.Length == 0)
            throw new ArgumentException("Enter the category name.");
        if (name.Length > 100)
            throw new ArgumentException("The category name is too long (maximum 100 characters).");
        if (_repository.NameExists(name, exceptId))
            throw new ArgumentException("This category already exists.");
    }
}