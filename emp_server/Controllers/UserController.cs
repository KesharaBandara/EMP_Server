using emp_server.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using emp_server.Dbo;
public class userModel
{
    public string name { get; set; }
    public string password { get; set; }
}
namespace emp_server.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IEmpRepository emp_repository;
        public UserController(IEmpRepository _emp_repository) => emp_repository = _emp_repository;
        [HttpPost]
        public async Task<IActionResult> verifyUser([FromBody] userModel user)
        {
            string u_name = user.name;
            string u_password = user.password;
            var user_response = await emp_repository.VerifyUser(u_name, u_password);
            //Console.WriteLine(user_response.ToString());

            return Ok(user_response);

            //var user = await emp_repository.VerifyUser(String name,String password);
            //return Ok(user);
            //return Ok(await dbContext.Products.ToListAsync());
            //return View();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreation addUser)
        {
            try
            {
                await emp_repository.CreateUser(addUser);
                return Ok();
                //return CreatedAtRoute("CompanyById", new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }
    }
}
