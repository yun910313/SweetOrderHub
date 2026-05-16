using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Models;

namespace Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DessertItemsController : ControllerBase
    {
        //宣告_context變數，並在建構子中注入AppDbContext
        private readonly AppDbContext _context;
        public DessertItemsController(AppDbContext context)
        {
            // 把資料庫存進 _context
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDessertItems()
        {
            // 從資料庫中取出DessertItems表的所有資料，並轉換成List
            var items = await _context.DessertItems.ToListAsync();
            // 回傳200 OK狀態碼和dessertItems資料
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> insertDessertItem(DessertItemModel dessertItem)
        {
            dessertItem.Id = Guid.NewGuid();
            _context.DessertItems.Add(dessertItem);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDessertItem(Guid id)
        {
            var item = await _context.DessertItems.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDessertItem(Guid id, DessertItemModel dessertItem)
        {
            var item = await _context.DessertItems.FirstOrDefaultAsync(x => x.Id == id);
            if(item == null)
            {
                return NotFound();
            }

            item.Name = dessertItem.Name;
            item.Price = dessertItem.Price;
            item.StockQuantity = dessertItem.StockQuantity;
            item.ImageUrl = dessertItem.ImageUrl;

            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDessertItem(Guid id)
        {
            var item = await _context.DessertItems.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _context.DessertItems.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
