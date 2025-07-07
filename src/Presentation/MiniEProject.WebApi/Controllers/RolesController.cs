using Microsoft.AspNetCore.Mvc;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.RoleDtos;
using MiniEProject.Application.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniEProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // POST: api/roles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreateDto dto)
        {
            var result = await _roleService.CreateRoleAsync(dto);
            return StatusCode((int)result.StatusCode, result);
        }

        // PUT: api/roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RoleUpdateDto dto)
        {
            var result = await _roleService.UpdateRoleAsync(dto);
            return StatusCode((int)result.StatusCode, result);
        }

        // GET: api/roles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _roleService.RoleGetByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        // DELETE: api/roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }

}

