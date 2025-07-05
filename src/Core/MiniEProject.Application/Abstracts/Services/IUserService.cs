using Microsoft.AspNetCore.Identity.Data;
using MiniEProject.Application.DTOs.UserAuthenticationDto;
using MiniEProject.Application.Shared;
using MiniEProject.Application.Shared.Responses;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Application.Abstracts.Services;

public interface IUserService
{
    Task<BaseResponse<string>> Register(UserRegisterDto dto);
    Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto);
    Task<BaseResponse<string>> AddRole(UserAddRoleDto dto);
    Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<BaseResponse<string>> ForgotPassword(UserForgotPasswordDto dto);
    Task<BaseResponse<string>> ResetPassword(UserResetPasswordDto dto);
    Task<BaseResponse<string>> ConfirmEmail(string userId, string token);
  
}
