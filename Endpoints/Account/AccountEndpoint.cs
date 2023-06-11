using System.Security.Claims;
using CustomerFeedback.Context;
using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
using CustomerFeedback.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CustomerFeedback.Endpoints.ValidateResult;

namespace CustomerFeedback.Endpoints.Account
{
    public static class AccountEndpoint
    {
        public static void MapAccountEndpoints(this WebApplication app)
        {
            app.MapGet(
                "/api/account",
                async (
                    UserManager<AppUser> userManager,
                    TokenService tokenService,
                    ClaimsPrincipal User
                ) =>
                {
                    var user = await userManager.FindByEmailAsync(
                        User.FindFirstValue(ClaimTypes.Email)!
                    );

                    return CreateUserObject(tokenService, user!);
                }
            );
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
                            return Results.Json(CreateUserObject(tokenService, user));
                        }

                        return Results.Unauthorized();
                    }
                )
                .AllowAnonymous();

            app.MapPost(
                    "/api/account/register",
                    async (
                        AppDbContext context,
                        UserManager<AppUser> userManager,
                        TokenService tokenService,
                        IValidator<RegisterDto> validator,
                        RegisterDto registerDto
                    ) =>
                    {
                        IEnumerable<string> validatorResult = Validate<RegisterDto>(
                            validator,
                            registerDto
                        );

                        // Check for clean data
                        if (validatorResult.Any())
                            return Results.BadRequest(validatorResult);

                        // Check if username is already taken
                        if (
                            await userManager.Users.AnyAsync(
                                x => x.UserName == registerDto.Username
                            )
                        )
                            return Results.BadRequest("Username is already taken");

                        // Check if Email is unique
                        if (await userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
                            return Results.BadRequest("Email is already taken");

                        AppUser newUser = new AppUser
                        {
                            Email = registerDto.Email,
                            UserName = registerDto.Username,
                            DisplayName = registerDto.DisplayName
                        };

                        var result = await userManager.CreateAsync(newUser);

                        if (result.Succeeded)
                            return Results.Ok(CreateUserObject(tokenService, newUser));

                        return Results.BadRequest(result.Errors);
                    }
                )
                .AllowAnonymous();
        }

        private static AppUserDto CreateUserObject(TokenService tokenService, AppUser newUser)
        {
            return new AppUserDto
            {
                DisplayName = newUser.DisplayName,
                UserName = newUser.UserName!,
                Email = newUser.Email!,
                Token = tokenService.CreateToken(newUser)
            };
        }
    }
}
