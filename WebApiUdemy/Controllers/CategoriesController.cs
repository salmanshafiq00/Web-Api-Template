using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUdemy.DTOs;
using WebApiUdemy.Interfaces;
using WebApiUdemy.Model;

namespace WebApiUdemy.Controllers
{
    // https://localhost:7132/api/regions
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Get All Categories
        // GET: https://localhost:7132/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();           
            return Ok(_mapper.Map<List<CategoryResponse>>(categories));
        }

        // Get Single Category
        // GET: https://localhost:7132/api/regions/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            Category? category = await _categoryRepository.GetAsync(id);
            if (category is null)
                return NotFound();
            return Ok(_mapper.Map<CategoryResponse>(category));
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            if (createCategoryDTO is null)
                return BadRequest();

            var category = _mapper.Map<Category>(createCategoryDTO);

            category = await _categoryRepository.CreateAsync(category);

            return CreatedAtAction(nameof(Get), new { id = category.Id }, _mapper.Map<CategoryResponse>(category));
        }

        [HttpPut("Update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateCategoryDTO createCategoryDTO)
        {
            var category = _mapper.Map<Category>(createCategoryDTO);

            category = await _categoryRepository.UpdateAsync(id, category);

            if (category is null)
                return NotFound();

            return Ok(_mapper.Map<CategoryResponse>(category));
        }


        // Delete Single Category
        // Delete: https://localhost:7132/api/regions/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _categoryRepository.DeleteAsync(id);
            if (category is null)
                return NotFound();
            return NoContent();
        }
    }
}
