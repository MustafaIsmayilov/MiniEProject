using System.Net;
using Microsoft.AspNetCore.Mvc;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.FavouriteDtos;
using MiniEProject.Application.DTOs.ProductDtos;
using MiniEProject.Application.Shared.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniEProject.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        var result = await _productService.CreateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto dto)
    {
        var result = await _productService.UpdateAsync(id, dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();
        return StatusCode((int)result.StatusCode, result);
    }
}
