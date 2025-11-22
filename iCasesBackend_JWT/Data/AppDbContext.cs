using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Headphone> Headphones { get; set; }
        public DbSet<Cable> Cables { get; set; }
        public DbSet<Watch> Watches { get; set; }
        public DbSet<Earbuds> Earbuds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure single table (TPH) for all product types
            modelBuilder.Entity<BaseProduct>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<Phone>("Phone")
                .HasValue<Case>("Case")
                .HasValue<Headphone>("Headphone")
                .HasValue<Cable>("Cable")
                .HasValue<Watch>("Watch")
                .HasValue<Earbuds>("Earbuds");

            // Use string Id as primary key
            modelBuilder.Entity<BaseProduct>()
                .HasKey(p => p.Id);

            // Fix decimal precision for Price column
            modelBuilder.Entity<BaseProduct>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Configure Cart relationships
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure decimal precision for Order amounts
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.OriginalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.FinalPrice)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}