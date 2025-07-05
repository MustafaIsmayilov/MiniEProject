using Microsoft.AspNetCore.Mvc;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.RoleDtos;
using MiniEProject.Application.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniEProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAllPermission()
        {
            var permissions = PermissionHelper.GetAllPermissions();
            return Ok(permissions);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto dto)
        {
            var result = await _roleService.CreateRoleAsync(dto);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateDto dto)
        {
            var result = await _roleService.UpdateRoleAsync(dto);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}

