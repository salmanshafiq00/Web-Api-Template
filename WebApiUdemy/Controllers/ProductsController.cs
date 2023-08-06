using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUdemy.DTOs;
using WebApiUdemy.Interfaces;
using WebApiUdemy.Model;

namespace WebApiUdemy.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // Get All Categories
        // GET: https://localhost:7132/api/regions
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetAll()
        //{
        //    var categories = await _productRepository.GetAllAsync();
        //    return Ok(_mapper.Map<List<ProductResponse>>(categories));
        //}


        // Get All Categories
        // GET: https://localhost:7132/api/regions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId)
        {
            var categories = await _productRepository.GetAllAsync(categoryId);
            return Ok(_mapper.Map<List<ProductResponse>>(categories));
        }

        // Get All Categories
        // GET: https://localhost:7132/api/regions
        [HttpGet("GetAllWithCategory")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetAllWithCategory()
        {
            var categories = await _productRepository.GetAllWithCategoryAsync();
            return Ok(_mapper.Map<List<ProductResponse>>(categories));
        }

        // Get Single Product
        // GET: https://localhost:7132/api/regions/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            Product? product = await _productRepository.GetAsync(id);
            if (product is null)
                return NotFound();
            return Ok(_mapper.Map<ProductResponse>(product));
        }

        // Create single Product
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Create([FromBody] CreateOrEditProductDTO createProductDTO)
        {
            if (createProductDTO is null)
                return BadRequest();

            var product = _mapper.Map<Product>(createProductDTO);

            product = await _productRepository.CreateAsync(product);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, _mapper.Map<ProductResponse>(product));
        }

        [HttpPut("Update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateOrEditProductDTO createProductDTO)
        {
            var product = _mapper.Map<Product>(createProductDTO);

            product = await _productRepository.UpdateAsync(id, product);

            if (product is null)
                return NotFound();

            return Ok(_mapper.Map<ProductResponse>(product));
        }


        // Delete Single Product
        // Delete: https://localhost:7132/api/regions/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _productRepository.DeleteAsync(id);
            if (product is null)
                return NotFound();
            return NoContent();
        }
    }
}
