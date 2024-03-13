using emp_server.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace emp_server.Data
{
    public class ProductsAPIDbContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public ProductsAPIDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("emp_server_connectionString");
        }
        public IDbConnection CreateConnection() => new SqlConnection(connectionString);

        
       //public ProductsAPIDbContext(DbContextOptions options) : base(options)
        //{
        //}
        //public DbSet<Products> Products { get; set; }
        //public DbSet<Staff> Staff { get; set; }
    }
}
