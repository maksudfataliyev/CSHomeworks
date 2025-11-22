namespace Backend.Models
{
    public abstract class BaseProduct
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Designer { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int? Discount { get; set; }
    }

    public class Phone : BaseProduct
    {
        public int Ram { get; set; }
        public int Storage { get; set; }
        public int Year { get; set; }
        public string Processor { get; set; } = string.Empty;
        public string Camera { get; set; } = string.Empty;
        public string OperatingSystem { get; set; } = string.Empty;
    }

    public class Case : BaseProduct
    {
        public string DesignedFor { get; set; } = string.Empty;
    }

    public class Headphone : BaseProduct
    {
        // Inherits all properties from BaseProduct
    }

    public class Cable : BaseProduct
    {
        public string Type { get; set; } = string.Empty; // "USB Type-A", "Lightning", etc.
    }

    public class Watch : BaseProduct
    {
        // Inherits all properties from BaseProduct
    }

    public class Earbuds : BaseProduct
    {
        // Inherits all properties from BaseProduct
    }
}