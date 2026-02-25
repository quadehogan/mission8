using Microsoft.EntityFrameworkCore;
using mission8.Models;

namespace mission8.Data;

// Concrete implementation of ITaskRepository -- all actual EF Core database calls live here.
// Registered as a scoped service in Program.cs so each HTTP request gets its own instance.
public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    // AppDbContext is injected automatically via dependency injection
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    // Return all tasks, including their related Category (eager loading via Include)
    public IEnumerable<ToDoTask> GetAllTasks()
    {
        return _context.Tasks
            .Include(t => t.Category)
            .ToList();
    }

    // Return a single task by its primary key, including Category
    public ToDoTask? GetTaskById(int id)
    {
        return _context.Tasks
            .Include(t => t.Category)
            .FirstOrDefault(t => t.TaskId == id);
    }

    // Add a new task to the database
    public void AddTask(ToDoTask task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    // Update an existing task (EF Core tracks changes automatically)
    public void UpdateTask(ToDoTask task)
    {
        _context.Tasks.Update(task);
        _context.SaveChanges();
    }

    // Delete a task by ID
    public void DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }

    // Mark a task as completed without deleting it
    public void MarkCompleted(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            task.Completed = true;
            _context.SaveChanges();
        }
    }

    // Return all categories -- used by the controller to populate the dropdown
    public IEnumerable<Category> GetCategories()
    {
        return _context.Categories
            .OrderBy(c => c.CategoryName)
            .ToList();
    }
}
