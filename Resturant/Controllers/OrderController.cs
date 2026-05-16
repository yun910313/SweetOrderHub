using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Dtos;
using Resturant.Models;

namespace Resturant.Controllers;

[Route("api/[controller]")]
[ApiController]

public class  OrderController : ControllerBase
{
        //宣告_context變數，並在建構子中注入AppDbContext
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context)
        {
            // 把資料庫存進 _context
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var order = await _context.Orders.ToListAsync();
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItem(Guid id)
        {
            var orderItem = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }
            
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrder(CreateOrderDto orderDto)
        {
            // 確認顧客是否存在
            var userExists = await _context.User.AnyAsync(x => x.Id == orderDto.UserId);

            var totalAmount = 0;
            
            if (!userExists)
            {
                return BadRequest("顧客不存在");
            }

            // 找這個顧客的購物車的商品
            var carItem = await _context.CartItems.Where(x => x.UserId == orderDto.UserId).ToListAsync();
            if (!carItem.Any())
            {
                return BadRequest("購物車沒有商品");
            }

            var order = new OrdersModel
            {
                Id = Guid.NewGuid(),
                UserId = orderDto.UserId,
                PickUpDate = orderDto.PickUpDate,
                Note = orderDto.Note,
                CreatedAt = DateTime.Now,
                Status = "準備中"
            };

            await _context.Orders.AddAsync(order);
            
            // 把購物車的商品加入訂單明細
            foreach (var item in carItem)
            {
                // 從甜點資料表找出甜點的價格，並確認甜點存在
                var dessert = await _context.DessertItems.FirstOrDefaultAsync(x => x.Id == item.DessertItemId);
                if (dessert == null)
                {
                    continue;
                }
                
                // 檢查庫存
                if (dessert.StockQuantity < item.Quantity)
                {
                    return BadRequest($"{dessert.Name} 庫存不足");
                }

                // 扣庫存
                dessert.StockQuantity -= item.Quantity;
                
                totalAmount += dessert.Price * item.Quantity;
                
                //  建立訂單資料
                var orderItem = new OrderItemsModel
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    DessertItemId = item.DessertItemId,
                    Quantity = item.Quantity,
                    UnitPrice = dessert.Price,
                };
                
                // 把訂單加入到資料表內
                await _context.OrderItems.AddAsync(orderItem);
            }
            
            order.TotalAmount = totalAmount;
            
            // 清空購物車資料
            _context.CartItems.RemoveRange(carItem);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderSatus(Guid id, UpdateOrderStatusDto orderStatusDto)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order  == null)
            {
                return NotFound("訂單不存在");
            }
            
            order.Status = orderStatusDto.Status;
            await  _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var item = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound("訂單不存在");
            }
            
            _context.Orders.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
}