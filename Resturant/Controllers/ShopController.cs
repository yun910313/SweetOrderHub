using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;

namespace Resturant.Controllers;

public class ShopController : Controller
{
    //宣告_context變數，並在建構子中注入AppDbContext
    private readonly AppDbContext _context;

    public ShopController(AppDbContext context)
    {
        // 把資料庫存進 _context
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var desserts = await _context.DessertItems.ToListAsync();

        return View(desserts);
    }
}