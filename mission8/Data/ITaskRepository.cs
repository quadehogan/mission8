using mission8.Models;

namespace mission8.Data;

// Repository interface -- defines the contract for all database operations.
// Controllers depend on this interface, not the concrete class (loose coupling).
public interface ITaskRepository
{
    // --- Task operations ---
    IEnumerable<ToDoTask> GetAllTasks();
    ToDoTask? GetTaskById(int id);
    void AddTask(ToDoTask task);
    void UpdateTask(ToDoTask task);
    void DeleteTask(int id);
    void MarkCompleted(int id);

    // --- Category operations (used to populate the dropdown) ---
    IEnumerable<Category> GetCategories();
}
