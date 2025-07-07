using AutoMapper;
using System.Net;
using System;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.ProductDtos;
using MiniEProject.Application.Shared.Responses;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
namespace MiniEProject.Persistence.Services;

public class ProductService : IProductService
{
    private readonly MiniEProjectDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(MiniEProjectDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProductGetDto>> CreateAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<ProductGetDto>(product);
        return new BaseResponse<ProductGetDto>("Product created", resultDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<ProductGetDto>> UpdateAsync(Guid id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return new BaseResponse<ProductGetDto>("Product not found", null, HttpStatusCode.NotFound);

        _mapper.Map(dto, product);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<ProductGetDto>(product);
        return new BaseResponse<ProductGetDto>("Product updated", resultDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return new BaseResponse<string>("Product not found", null, HttpStatusCode.NotFound);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return new BaseResponse<string>("Product deleted", null, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<ProductGetDto>> GetByIdAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return new BaseResponse<ProductGetDto>("Product not found", null, HttpStatusCode.NotFound);

        var dto = _mapper.Map<ProductGetDto>(product);
        return new BaseResponse<ProductGetDto>("Success", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<ProductGetDto>>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        var dtos = _mapper.Map<List<ProductGetDto>>(products);
        return new BaseResponse<List<ProductGetDto>>("Success", dtos, HttpStatusCode.OK);
    }
}

