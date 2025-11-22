using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Authorize] // ← Require JWT token for all endpoints
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // Helper to get current user from JWT token
        private string GetCurrentUsername()
        {
            return User.FindFirst(ClaimTypes.Name)?.Value ?? "";
        }

        /// <summary>
        /// Checkout Direct - Create order from provided cart data
        /// </summary>
        [HttpPost("checkout-direct")]
        public async Task<ActionResult<OrderResponse>> CheckoutDirect([FromBody] CheckoutRequest request)
        {
            var username = GetCurrentUsername();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            
            if (user == null)
                return NotFound(new { message = "User not found" });

            if (request.Items == null || !request.Items.Any())
                return BadRequest(new { message = "Cart is empty" });

            var order = new Order
            {
                UserId = user.Id,
                PurchaseDate = DateTime.UtcNow
            };

            decimal totalAmount = 0;

            foreach (var cartItem in request.Items)
            {
                var product = await _context.Set<BaseProduct>()
                    .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

                if (product == null) continue;

                var originalPrice = product.Price;
                var discount = product.Discount;
                var finalPrice = discount.HasValue 
                    ? originalPrice - (originalPrice * discount.Value / 100) 
                    : originalPrice;

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    ProductImage = product.Image,
                    OriginalPrice = originalPrice,
                    Discount = discount,
                    FinalPrice = finalPrice,
                    Quantity = cartItem.Quantity
                };

                order.Items.Add(orderItem);
                totalAmount += finalPrice * cartItem.Quantity;
            }

            order.TotalAmount = totalAmount;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new OrderResponse
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
                PurchaseDate = order.PurchaseDate,
                Items = order.Items.Select(oi => new OrderItemResponse
                {
                    ProductId = oi.ProductId,
                    ProductTitle = oi.ProductTitle,
                    ProductImage = oi.ProductImage,
                    OriginalPrice = oi.OriginalPrice,
                    Discount = oi.Discount,
                    FinalPrice = oi.FinalPrice,
                    Quantity = oi.Quantity
                }).ToList()
            });
        }

        /// <summary>
        /// Get current user's purchase history
        /// </summary>
        [HttpGet("history")]
        public async Task<ActionResult<PurchaseHistoryResponse>> GetMyPurchaseHistory()
        {
            var username = GetCurrentUsername();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            
            if (user == null)
                return NotFound(new { message = "User not found" });

            var orders = await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == user.Id)
                .OrderByDescending(o => o.PurchaseDate)
                .ToListAsync();

            var response = new PurchaseHistoryResponse
            {
                TotalOrders = orders.Count,
                TotalSpent = orders.Sum(o => o.TotalAmount),
                Orders = orders.Select(o => new OrderResponse
                {
                    OrderId = o.Id,
                    TotalAmount = o.TotalAmount,
                    PurchaseDate = o.PurchaseDate,
                    Items = o.Items.Select(oi => new OrderItemResponse
                    {
                        ProductId = oi.ProductId,
                        ProductTitle = oi.ProductTitle,
                        ProductImage = oi.ProductImage,
                        OriginalPrice = oi.OriginalPrice,
                        Discount = oi.Discount,
                        FinalPrice = oi.FinalPrice,
                        Quantity = oi.Quantity
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }

        /// <summary>
        /// Get specific order details
        /// </summary>
        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(int orderId)
        {
            var username = GetCurrentUsername();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            
            if (user == null)
                return NotFound(new { message = "User not found" });

            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == user.Id);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            return Ok(new OrderResponse
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
                PurchaseDate = order.PurchaseDate,
                Items = order.Items.Select(oi => new OrderItemResponse
                {
                    ProductId = oi.ProductId,
                    ProductTitle = oi.ProductTitle,
                    ProductImage = oi.ProductImage,
                    OriginalPrice = oi.OriginalPrice,
                    Discount = oi.Discount,
                    FinalPrice = oi.FinalPrice,
                    Quantity = oi.Quantity
                }).ToList()
            });
        }
    }
}