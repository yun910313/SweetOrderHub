using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Models;
using Resturant.Dtos;

namespace Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        //宣告_context變數，並在建構子中注入AppDbContext
        private readonly AppDbContext _context;
        public CartItemsController(AppDbContext context)
        {
            // 把資料庫存進 _context
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var caritems = await (
                from cartItem in _context.CartItems
                join dessertItem in _context.DessertItems on cartItem.DessertItemId equals dessertItem.Id
                select new
                {
                    cartItem.Id,
                    cartItem.DessertItemId,
                    DessertName = dessertItem.Name,
                    DessertPrice = dessertItem.Price,
                    cartItem.Quantity,
                    Subtotal = dessertItem.Price * cartItem.Quantity,
                    cartItem.CreatedAt
                }
            ).ToListAsync();
            
            // var cartItems = await _context.CartItems.ToListAsync();

            return Ok(caritems);
        }
        
        // 前端傳Dto資料格式(用model的話，會自己新增不存在的甜點id，這樣資料庫就找不到這筆甜點資料)
        [HttpPost]
        public async Task<IActionResult> InsertCartItem(CartItemDto caritemDto)
        {
            // 確認這筆甜點存在，回傳true/false，會去找資料庫有沒有相同ID的甜點
            var dessertExists = await _context.DessertItems.AnyAsync(x => x.Id == caritemDto.DessertItemId);
            if (!dessertExists)
            {
                return BadRequest("甜點不存在");
            }
        
            // 建立真正要存進 DB 的 Entity
            var cartItem = new CartItemModel
            {
                Id = Guid.NewGuid(),
                DessertItemId = caritemDto.DessertItemId,
                Quantity = caritemDto.Quantity,
                CreatedAt = DateTime.Now
            };
        
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        
            return Ok(cartItem);
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> InsertCartItem(Guid id, CartItemModel cartItem)
        {
            return Ok();
        }
    }
}
