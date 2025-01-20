using ASPMVC.DTO;
using ASPMVC.MySQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {

        private readonly SqlService _sqlService;

        public ProductoController(SqlService sqlService)
        { 
            this._sqlService = sqlService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(new { products = _sqlService.getProducts()} );
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var result = _sqlService.getProduct(id);
            // If product doesn't exist
            if(result == null)
            {
                return BadRequest(new { message = "PRODUCT_NOT_FOUND" });
                
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateProductoDto dto)
        {
            // If no body is sent
            if(dto == null)
            {
                return BadRequest(new { message = "PRODUCT_INVALID_DATA" });
            }
            var result = _sqlService.createProduct(dto);
            // If product was not created
            if(!result)
            {
                return BadRequest(new { message = "CANT_CREATE" });
            }
            return Ok(new { message = "PRODUCT_CREATED" });
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
