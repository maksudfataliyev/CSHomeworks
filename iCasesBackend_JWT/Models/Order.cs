namespace Backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string ProductId { get; set; } = string.Empty;
        public string ProductTitle { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class CheckoutRequest
    {
        public List<CheckoutItemRequest> Items { get; set; } = new();
    }

    public class CheckoutItemRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
    }

    public class OrderResponse
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();
    }

    public class OrderItemResponse
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductTitle { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class PurchaseHistoryResponse
    {
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public List<OrderResponse> Orders { get; set; } = new();
    }
}