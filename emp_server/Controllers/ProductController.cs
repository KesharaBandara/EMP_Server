using emp_server.Contracts;
using emp_server.Data;
using emp_server.Dbo;
using emp_server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emp_server.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IEmpRepository emp_repository;
        //private readonly ProductsAPIDbContext dbContext;
        public ProductController(IEmpRepository _emp_repository) => emp_repository = _emp_repository;
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await emp_repository.GetProducts();
            return Ok(products);
            //return Ok(await dbContext.Products.ToListAsync());
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreation addProductRequest)
        {
            try
            {
                await emp_repository.CreateProduct(addProductRequest);
                return Ok();
                //return CreatedAtRoute("CompanyById", new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await emp_repository.GetProduct(id);
                if (product == null)
                {
                    return BadRequest();
                }

                return Ok(product);
            }catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await emp_repository.GetProduct(id);
                if(product == null)
                {
                    return BadRequest();    
                }
               var deleted_product=await emp_repository.DeleteProduct(id);
                return Ok(deleted_product);
            }catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpGet]
        [Route("search/{keyword}")]
        public async Task<IActionResult> SearchProduct(string keyword)
        {
            try
            {
                var product = await emp_repository.SearchProduct(keyword);
                if (product == null)
                {
                    return BadRequest();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
