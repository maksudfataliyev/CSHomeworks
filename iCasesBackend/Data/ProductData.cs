using Backend.Models;

namespace Backend.Data
{
    public static class ProductData
    {
        public static List<Phone> Phones => new()
        {
            new Phone
            {
                Id = "1",
                Title = "iPhone 15 Pro Max (256 GB) - Natural Titanium",
                Price = 999.99m,
                Ram = 6,
                Storage = 256,
                Year = 2023,
                Image = "img1.png",
                Processor = "A17 Pro",
                Camera = "Triple 12MP",
                OperatingSystem = "iOS",
                Color = "Natural Titanium",
                Designer = "Apple",
                Category = "phones",
                Discount = null
            },
            new Phone
            {
                Id = "2",
                Title = "iPhone 13 (128GB) - Midnight",
                Price = 399.99m,
                Ram = 4,
                Storage = 128,
                Year = 2021,
                Image = "img2.png",
                Processor = "A15 Bionic",
                Camera = "Dual 12MP",
                OperatingSystem = "iOS",
                Color = "Midnight",
                Designer = "Apple",
                Category = "phones",
                Discount = 10
            },
            new Phone
            {
                Id = "3",
                Title = "iPhone 15 (128GB) - Blue",
                Price = 699.99m,
                Ram = 6,
                Storage = 128,
                Year = 2023,
                Image = "img3.png",
                Processor = "A16 Bionic",
                Camera = "Dual 12MP",
                OperatingSystem = "iOS",
                Color = "Blue",
                Designer = "Apple",
                Category = "phones",
                Discount = null
            },
            new Phone
            {
                Id = "4",
                Title = "iPhone 15 (128GB) - Black",
                Price = 699.99m,
                Ram = 6,
                Storage = 128,
                Year = 2023,
                Image = "img4.png",
                Processor = "A16 Bionic",
                Camera = "Dual 12MP",
                OperatingSystem = "iOS",
                Color = "Black",
                Designer = "Apple",
                Category = "phones",
                Discount = null
            },
            new Phone
            {
                Id = "5",
                Title = "iPhone 12 (128GB) - Green",
                Price = 299.99m,
                Ram = 4,
                Storage = 128,
                Year = 2020,
                Image = "img5.png",
                Processor = "A14 Bionic",
                Camera = "Dual 12MP",
                OperatingSystem = "iOS",
                Color = "Green",
                Designer = "Apple",
                Category = "phones",
                Discount = 30
            },
            new Phone
            {
                Id = "6",
                Title = "iPhone 14 (128GB) - Starlight",
                Price = 499.99m,
                Ram = 6,
                Storage = 128,
                Year = 2022,
                Image = "img6.png",
                Processor = "A15 Bionic",
                Camera = "Dual 12MP",
                OperatingSystem = "iOS",
                Color = "Starlight",
                Designer = "Apple",
                Category = "phones",
                Discount = 10
            },
            new Phone
            {
                Id = "9",
                Title = "iPhone 16 Pro Max (256GB) - Desert Titanium",
                Price = 1499.99m,
                Ram = 8,
                Storage = 256,
                Year = 2024,
                Image = "img9.png",
                Processor = "A18 Pro",
                Camera = "Triple 12MP",
                OperatingSystem = "iOS",
                Color = "Desert Titanium",
                Designer = "Apple",
                Category = "phones",
                Discount = null
            }
        };

        public static List<Earbuds> Earbuds => new()
        {
            new Earbuds
            {
                Id = "7",
                Title = "Apple AirPods 2",
                Price = 99.99m,
                Image = "img7.png",
                Color = "White",
                Designer = "Apple",
                Category = "earbuds",
                Discount = 25
            }
        };

        public static List<Watch> Watches => new()
        {
            new Watch
            {
                Id = "8",
                Title = "Galaxy Watch Ultra (LTE, 47mm) - Titanium Gray",
                Price = 599.99m,
                Image = "img8.png",
                Color = "Titanium Gray",
                Designer = "Samsung",
                Category = "watches",
                Discount = 15
            }
        };

        public static List<Case> Cases => new()
        {
            new Case
            {
                Id = "10",
                Title = "iPhone 15 Clear Case with MagSafe",
                Designer = "Apple",
                DesignedFor = "iPhone 15",
                Color = "Transparent",
                Image = "img10.png",
                Price = 50m,
                Category = "cases",
                Discount = 10
            }
        };

        public static List<Headphone> Headphones => new()
        {
            new Headphone
            {
                Id = "11",
                Title = "AirPods Max - Purple",
                Designer = "Apple",
                Color = "Purple",
                Image = "img11.png",
                Price = 549.99m,
                Category = "headphones",
                Discount = null
            }
        };

        public static List<Cable> Cables => new()
        {
            new Cable
            {
                Id = "12",
                Title = "Apple Lightning to USB Cable (1 m)",
                Designer = "Apple",
                Type = "Lightning",
                Color = "White",
                Image = "img12.png",
                Price = 10m,
                Category = "cables",
                Discount = null
            }
        };

        public static List<BaseProduct> GetAllProducts()
        {
            var allProducts = new List<BaseProduct>();
            allProducts.AddRange(Phones);
            allProducts.AddRange(Earbuds);
            allProducts.AddRange(Watches);
            allProducts.AddRange(Cases);
            allProducts.AddRange(Headphones);
            allProducts.AddRange(Cables);
            return allProducts;
        }
    }
}