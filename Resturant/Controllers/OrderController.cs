using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Resturant.Data;

namespace Resturant.Controllers;

[Route("api/[controller]")]
[ApiController]

public class  OrderController
{
        //宣告_context變數，並在建構子中注入AppDbContext
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context)
        {
            // 把資料庫存進 _context
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            return>
        }
}