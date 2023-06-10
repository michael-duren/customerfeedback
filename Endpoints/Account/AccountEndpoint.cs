using CustomerFeedback.Context;
using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
using CustomerFeedback.Services;
using Microsoft.AspNetCore.Identity;

namespace CustomerFeedback.Endpoints.Account
{
    public static class AccountEndpoint
    {
        public static void MapAccountEndpoints(this WebApplication app)
        {
            app.MapPost(
                    "/api/account/login",
                    async (
                        AppDbContext context,
                        UserManager<AppUser> userManager,
                        TokenService tokenService,
                        LoginDto loginDto
                    ) =>
                    {
                        var user = await userManager.FindByEmailAsync(loginDto.Email);
                        if (user is null)
                            return Results.Unauthorized();

                        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

                        if (result)
                        {
                            var userDto = new AppUserDto
                            {
                                DisplayName = user.DisplayName,
                                Email = user.Email!,
                                UserName = user.UserName!,
                                Token = tokenService.CreateToken(user)
                            };

                            return Results.Json(userDto);
                        }

                        return Results.Unauthorized();
                    }
                )
                .AllowAnonymous();
        }
    }
}
