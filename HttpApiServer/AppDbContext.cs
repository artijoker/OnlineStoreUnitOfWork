using Microsoft.EntityFrameworkCore;
using HttpModels;

namespace HttpApiServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Cart> Cart => Set<Cart>();
        public DbSet<CartItem> CartItem => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Product> products = new();
            List<Category> categories = new();

            categories.Add(new()
            {
                Id = Guid.NewGuid(),
                Name = "Смарфоны"
            });
            products.Add(new()
            {
                Id = Guid.NewGuid(),
                Name = "Смартфон Google Pixel 5a",
                CategoryId = categories[^1].Id,
                Price = 560m,
                Quantity = 20,
                UrlImage = "https://cdn1.ozone.ru/s3/multimedia-3/wc250/6237328203.jpg"
            });

            categories.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "USB накопители"
                });
            products.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "USB накопитель Samsung",
                    CategoryId = categories[^1].Id,
                    Price = 30m,
                    Quantity = 35,
                    UrlImage = "https://cdn1.ozone.ru/multimedia/wc1200/1026248251.jpg"
                });

            categories.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ноутбуки"
                });
            products.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ноутбук Lenovo IdeaPad 4 15IX5P6",
                    CategoryId = categories[^1].Id,
                    Price = 830.50m,
                    Quantity = 15,
                    UrlImage = "https://cdn1.ozone.ru/s3/multimedia-7/wc1200/6166994971.jpg"
                });

            categories.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Наушники"
                });
            products.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Наушники Sony WH-CH56030NW",
                    CategoryId = categories[^1].Id,
                    Price = 130.60m,
                    Quantity = 25,
                    UrlImage = "https://cdn1.ozone.ru/s3/multimedia-r/wc1200/6179635779.jpg"
                });

            categories.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Игровые консоли"
                });
            products.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Microsoft Xbox Series X",
                    CategoryId = categories[^1].Id,
                    Price = 985m,
                    Quantity = 2,
                    UrlImage = "https://cdn1.ozone.ru/s3/multimedia-l/wc1200/6232471137.jpg"
                });

            categories.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Телевизоры"
                });
            products.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Телевизор Sony KD50X81J 50",
                    CategoryId = categories[^1].Id,
                    Price = 1000.89m,
                    Quantity = 15,
                    UrlImage = "https://cdn1.ozone.ru/s3/multimedia-n/wc1200/6068732087.jpg"
                });

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);
        }

    }
}
