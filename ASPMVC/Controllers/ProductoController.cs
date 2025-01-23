using ASPMVC.DTO;
using ASPMVC.MySQL;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(new { products = _sqlService.GetProducts()} );
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var result = _sqlService.GetProduct(id);
            // If product doesn't exist
            if(result == null)
            {
                return BadRequest(new { message = "PRODUCT_NOT_FOUND" });
                
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create([FromBody] CreateProductoDto dto)
        {
            // If no body is sent
            if(dto == null)
            {
                return BadRequest(new { message = "PRODUCT_INVALID_DATA" });
            }
            var result = _sqlService.CreateProduct(dto);
            // If product was not created
            if(!result)
            {
                return BadRequest(new { message = "PRODUCT_CANT_CREATE" });
            }
            return Created();
        }

        [Authorize]
        [HttpPatch]
        public ActionResult Update(int id, [FromBody] CreateProductoDto dto)
        {
            // If no body is sent
            if (dto == null)
            {
                return BadRequest(new { message = "PRODUCT_INVALID_DATA" });
            }
            var result = _sqlService.UpdateProduct(id,dto);
            // If product was not updated
            if (!result)
            {
                return BadRequest(new { message = "PRODUCT_CANT_UPDATE" });
            }
            return Ok(new { message = "PRODUCT_UPDATED" });
        }

        [Authorize]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _sqlService.DeleteProduct(id);
            // If product was not deleted, return bad request
            if (!result)
            {
                return BadRequest(new { message = "PRODUCT_CANT_DELETE" });
            }
            return Ok(new { message = "PRODUCT_DELETED" });
        }

    }
}
