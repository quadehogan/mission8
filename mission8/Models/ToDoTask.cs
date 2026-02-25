using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mission8.Models;

// Main task model -- maps to the Tasks table in the database
public class ToDoTask
{
    [Key]
    public int TaskId { get; set; }

    // Task name is required (validation enforced via data annotation)
    // Property is named "Task" to match the view's asp-for binding
    [Required(ErrorMessage = "Task name is required.")]
    public string Task { get; set; } = string.Empty;

    [Display(Name = "Due Date")]
    public DateOnly? DueDate { get; set; }

    // Quadrant 1-4 as defined by Covey's Time Management Matrix
    [Required(ErrorMessage = "Quadrant is required.")]
    [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
    public int Quadrant { get; set; }

    // Foreign key to Category table
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    // Default to false (not completed) when a task is created
    public bool Completed { get; set; } = false;
}
