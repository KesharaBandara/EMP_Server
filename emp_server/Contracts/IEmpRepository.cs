using emp_server.Dbo;
using emp_server.Models;
namespace emp_server.Contracts
{
    public interface IEmpRepository
    {
        public Task<IEnumerable<Products>> GetProducts();
        public Task CreateProduct(ProductCreation product);
        public Task<Products> GetProduct(int id);
        public Task <int>DeleteProduct(int id);
        public Task<IEnumerable<Products>> SearchProduct(string keyword);
        public Task <IEnumerable<User>> VerifyUser(string name,string password);
        public Task CreateUser(UserCreation addUser);
        //Task VerifyUser(string string1, object name, string string2, object password);
    }
}
