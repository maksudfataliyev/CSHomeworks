using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Checkout - Create order from cart and clear cart (requires cart in DB)
        /// </summary>
        [HttpPost("{username}/checkout")]
        public async Task<ActionResult<OrderResponse>> Checkout(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // Get user's cart
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null || !cart.Items.Any())
                return BadRequest(new { message = "Cart is empty" });

            var order = new Order
            {
                UserId = user.Id,
                PurchaseDate = DateTime.UtcNow
            };

            decimal totalAmount = 0;

            // Convert cart items to order items
            foreach (var cartItem in cart.Items)
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

            // Clear cart after checkout
            _context.CartItems.RemoveRange(cart.Items);

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
        /// Checkout Direct - Create order from provided cart data (for localStorage carts)
        /// </summary>
        [HttpPost("{username}/checkout-direct")]
        public async Task<ActionResult<OrderResponse>> CheckoutDirect(string username, [FromBody] CheckoutRequest request)
        {
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

            // Convert provided cart items to order items
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
        /// Get user's purchase history
        /// </summary>
        [HttpGet("{username}/history")]
        public async Task<ActionResult<PurchaseHistoryResponse>> GetPurchaseHistory(string username)
        {
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
        [HttpGet("{username}/order/{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(string username, int orderId)
        {
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