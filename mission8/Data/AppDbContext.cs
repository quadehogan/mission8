using Microsoft.EntityFrameworkCore;
using mission8.Models;

namespace mission8.Data;

// AppDbContext is the EF Core "bridge" between C# model classes and the SQLite database
public class AppDbContext : DbContext
{
    // Constructor receives options (like the connection string) from dependency injection
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // These DbSet properties represent the database tables
    public DbSet<ToDoTask> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed the four Category options that populate the dropdown
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Home" },
            new Category { CategoryId = 2, CategoryName = "School" },
            new Category { CategoryId = 3, CategoryName = "Work" },
            new Category { CategoryId = 4, CategoryName = "Church" }
        );

        // Seed some sample tasks spread across all four quadrants
        // Note: navigation properties (Category) cannot be set in HasData -- use FK (CategoryId) instead
        modelBuilder.Entity<ToDoTask>().HasData(
            // Quadrant 1: Important / Urgent
            new ToDoTask
            {
                TaskId = 1,
                Task = "Pay rent",
                DueDate = new DateOnly(2026, 3, 1),
                Quadrant = 1,
                CategoryId = 1, // Home
                Completed = false
            },
            new ToDoTask
            {
                TaskId = 2,
                Task = "Study for midterm",
                DueDate = new DateOnly(2026, 3, 5),
                Quadrant = 1,
                CategoryId = 2, // School
                Completed = false
            },
            // Quadrant 2: Important / Not Urgent
            new ToDoTask
            {
                TaskId = 3,
                Task = "Build exercise routine",
                Quadrant = 2,
                CategoryId = 1, // Home
                Completed = false
            },
            new ToDoTask
            {
                TaskId = 4,
                Task = "Complete IS413 group project",
                DueDate = new DateOnly(2026, 3, 10),
                Quadrant = 2,
                CategoryId = 2, // School
                Completed = false
            },
            // Quadrant 3: Not Important / Urgent
            new ToDoTask
            {
                TaskId = 5,
                Task = "Reply to work emails",
                Quadrant = 3,
                CategoryId = 3, // Work
                Completed = false
            },
            new ToDoTask
            {
                TaskId = 6,
                Task = "Attend optional department meeting",
                Quadrant = 3,
                CategoryId = 3, // Work
                Completed = false
            },
            // Quadrant 4: Not Important / Not Urgent
            new ToDoTask
            {
                TaskId = 7,
                Task = "Browse social media",
                Quadrant = 4,
                CategoryId = 1, // Home
                Completed = false
            },
            new ToDoTask
            {
                TaskId = 8,
                Task = "Organize old files",
                Quadrant = 4,
                CategoryId = 3, // Work
                Completed = false
            }
        );
    }
}
