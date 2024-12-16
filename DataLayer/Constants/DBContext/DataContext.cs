using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model.Entity;

namespace DataLayer.Constants.DBContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> User => Set<User>();
        public DbSet<Book> Book => Set<Book>();
        public DbSet<CartItem> CartItem => Set<CartItem>();
        public DbSet<BookCategory> BookCategory => Set<BookCategory>();
        public DbSet<Cart> Cart => Set<Cart>(); 
        public DbSet<Category> Category => Set<Category>();
        public DbSet<Order> Order => Set<Order>();
        public DbSet<OrderItem> OrderItem => Set<OrderItem>();
        public DbSet<Payment> Payment => Set<Payment>();
        public DbSet<WishList> WishList => Set<WishList>();
        public DbSet<wishListItem> wishListItem => Set<wishListItem>();
        public DbSet<Shipping> Shipping => Set<Shipping>();
    }
}
