# SweetOrderHub

甜點訂購系統 RESTful API，使用 ASP.NET Core Web API 開發，提供商品管理、購物車、訂單建立、庫存扣減與訂單狀態更新功能。

## 技術使用

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- RESTful API
- Postman / Swagger 測試

## 主要功能

### 甜點商品管理
- 新增甜點
- 查詢甜點
- 修改甜點
- 刪除甜點
- 庫存管理

### 購物車
- 加入購物車
- 查詢購物車
- 修改商品數量
- 刪除購物車商品

### 訂單
- 建立訂單
- 將購物車商品轉為訂單明細
- 扣除商品庫存
- 清空購物車
- 更新訂單狀態

## 專案特色

- 使用 DTO 控制 API 輸入資料
- 使用 LINQ Join 查詢關聯資料
- 訂單建立時會檢查庫存
- 訂單成立後自動建立訂單明細
- 訂單成立後自動清空購物車

## 資料表

- DessertItems
- CartItems
- Orders
- OrderItems
- User

## API 範例

### 新增購物車商品

```http
POST /api/CartItems

{
  "userId": "使用者Id",
  "dessertItemId": "甜點Id",
  "quantity": 2
}

```
### 開發狀態

目前已完成後端 API 基礎功能，後續預計補上前端畫面與管理介面。