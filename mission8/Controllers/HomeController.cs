using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mission8.Models;
using mission8.Data;

namespace mission8.Controllers;

public class HomeController : Controller
{
    private readonly ITaskRepository _repo;

    // Inject ITaskRepository via constructor (dependency injection)
    public HomeController(ITaskRepository repo)
    {
        _repo = repo;
    }

    // GET: Home/Index - Display all incomplete tasks in quadrants view
    public IActionResult Index()
    {
        var tasks = _repo.GetAllTasks();
        return View(tasks);
    }

    // GET: Home/Add - Show form to add a new task
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.Categories = _repo.GetCategories();
        return View(new ToDoTask());
    }

    // POST: Home/Add - Save a new task
    [HttpPost]
    public IActionResult Add(ToDoTask task)
    {
        if (ModelState.IsValid)
        {
            _repo.AddTask(task);
            return RedirectToAction("Index");
        }

        // If validation fails, return to form with categories
        ViewBag.Categories = _repo.GetCategories();
        return View(task);
    }

    // GET: Home/Edit/{id} - Show form to edit an existing task
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var task = _repo.GetTaskById(id);
        if (task == null)
        {
            return NotFound();
        }

        ViewBag.Categories = _repo.GetCategories();
        return View(task);
    }

    // POST: Home/Edit - Update an existing task
    [HttpPost]
    public IActionResult Edit(ToDoTask task)
    {
        if (ModelState.IsValid)
        {
            _repo.UpdateTask(task);
            return RedirectToAction("Index");
        }

        // If validation fails, return to form with categories
        ViewBag.Categories = _repo.GetCategories();
        return View(task);
    }

    // POST: Home/Delete/{id} - Delete a task
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _repo.DeleteTask(id);
        return RedirectToAction("Index");
    }

    // POST: Home/MarkComplete/{id} - Mark a task as completed
    [HttpPost]
    public IActionResult MarkComplete(int id)
    {
        _repo.MarkCompleted(id);
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}