namespace mission8.Models;

// Separate Category table -- used to populate the dropdown on the task form
public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    // Navigation property: one category can have many tasks
    public List<ToDoTask> Tasks { get; set; } = new();
}
