using Dapper;
using emp_server.Contracts;
using emp_server.Data;
using emp_server.Dbo;
using emp_server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace emp_server.Repository
{
    public class EmpRepository : IEmpRepository
    {
        private readonly ProductsAPIDbContext _context;

        public EmpRepository(ProductsAPIDbContext context)=> _context = context;    
        
        async Task<IEnumerable<Products>> IEmpRepository.GetProducts()
        {
            var query = "SELECT * FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<Products>(query);
                return products.ToList();
            }
        }
        async Task  IEmpRepository.CreateProduct(ProductCreation product)
        {
            var query = "INSERT INTO Products (Name,Price,Quantity) VALUES(@name,@price,@quantity)";
            var parameters = new DynamicParameters(product);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
                /*var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdProduct = new Products
                {
                    Id = id,
                    Name = product.Name,
                    Price = (float)product.Price,
                    Quantity = (int)product.Quantity
                };
                return createdProduct;*/
            }
        }

        /*Task<Products> IEmpRepository.GetProduct(int id)
        {
            var qurey = "SELECT * FROM PRODUCTS WHERE Id=@id";
            using (var connection = _context.CreateConnection())
            {
                var product = connection.QuerySingleOrDefaultAsync<Products>(qurey, new { id });
                return product;
            }
        }*/

        async Task<Products> IEmpRepository.GetProduct(int id)
        {
            var qurey = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QuerySingleOrDefaultAsync<Products>(qurey, new { id });
                //Console.WriteLine(product);
                return product;
            }
        }
        async Task<int> IEmpRepository.DeleteProduct(int id)
        {
            var qurey = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var product = await connection.ExecuteAsync(qurey, new { id });
                //Console.WriteLine(product);
                return id;
            }
        }
        async Task<IEnumerable<Products>> IEmpRepository.SearchProduct(string keyword)
        {
            var qurey = "SELECT * FROM Products WHERE Name LIKE '%' + @Keyword + '%'";
            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QueryAsync<Products>(qurey,new { keyword});
                //Console.WriteLine(product);
                return product;
            }
        }
        async Task<IEnumerable<User>> IEmpRepository.VerifyUser(string name,string password)
        {
            var query = "SELECT User_id FROM [User] WHERE email=@Name AND password=@password";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<User>(query, new {name,password});
                return user;
            }
        }
        async Task IEmpRepository.CreateUser(UserCreation addUser)
        {
            var query = "INSERT INTO [User] (name,email,password) VALUES(@name,@email,@password)";
            var parameters = new DynamicParameters(addUser);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                /*var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdProduct = new Products
                {
                    Id = id,
                    Name = product.Name,
                    Price = (float)product.Price,
                    Quantity = (int)product.Quantity
                };
                return createdProduct;*/
            }
        }
    }
}
