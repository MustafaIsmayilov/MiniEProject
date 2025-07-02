using System.Net;
using Microsoft.AspNetCore.Mvc;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.CategoryDtos;
using MiniEProject.Application.Shared.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniEProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService { get; }
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        

        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<string>),(int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> Post([FromBody] CategoryCreateDto dto)
        {
            var result = await _categoryService.AddAsync(dto);
            return StatusCode((int)result.StatusCode, result);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
