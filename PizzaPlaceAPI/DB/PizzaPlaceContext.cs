using Microsoft.EntityFrameworkCore;
using PizzaPlaceAPI.DB.Models;

namespace PizzaPlaceAPI.DB
{
    public class PizzaPlaceContext : DbContext
    {
        public IConfiguration config { get; set; }
        public DbSet<OrderStamp> OrderStamp { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }

        public PizzaPlaceContext(IConfiguration config)
        {
            this.config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string? conn_string = config.GetConnectionString("Default");
            builder.UseSqlServer(conn_string);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderStamp>().ToTable("order_stamp");
            builder.Entity<OrderDetail>().ToTable("order_detail");
            builder.Entity<Pizza>().ToTable("pizza");
            builder.Entity<Recipe>().ToTable("recipe");
            builder.Entity<Category>().ToTable("category");
            builder.Entity<RecipeIngredient>().ToTable("recipe_ingredient");
            builder.Entity<Ingredient>().ToTable("ingredient");
        }
    }
}
