namespace Resturant.Dtos;

public class CartItemDto
{
    // 前端只傳DessertItemId、Quantity兩個欄位，Dto可以決定API的資料格式
    public Guid DessertItemId { get; set; }
    
    public Guid UserId { get; set; }

    public int Quantity { get; set; }
}