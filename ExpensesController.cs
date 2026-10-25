using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNetProject386.Models;

namespace WebNetProject386.Controllers
{
public class ExpensesController : Controller
{
private readonly ApplicationDbContext _context;

    public ExpensesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Display all expenses
    public async Task<IActionResult> Index()
{
    var expenses = await _context.Expenses
        .OrderByDescending(e => e.ExpenseDate)
        .ToListAsync();

    return View(expenses);
}

    // Display the add expense form
    public IActionResult Create()
    {
        return View();
    }

    // Save a new expense
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Expense expense)
    {
        if (!ModelState.IsValid)
            return View(expense);

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Expense added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // Display the edit form
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
            return NotFound();

        return View(expense);
    }

    // Save expense changes
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Expense expense)
    {
        if (id != expense.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(expense);

        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Expense updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // Display the delete confirmation
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id);

        if (expense == null)
            return NotFound();

        return View(expense);
    }

    // Delete the expense
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense != null)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        TempData["Success"] = "Expense deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}


}
