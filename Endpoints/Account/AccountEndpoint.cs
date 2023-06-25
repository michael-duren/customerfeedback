using System.Security.Claims;
using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
using CustomerFeedback.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

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
                    ClaimsPrincipal claimsPrincipal
                ) =>
                {
                    var user = await userManager.FindByEmailAsync(
                        claimsPrincipal.FindFirstValue(ClaimTypes.Email)!
                    );
                    

                    if (user is null) return Results.BadRequest();
                    var userObj = await CreateUserObject(tokenService, user, userManager);
                    return Results.Ok(userObj);

                }
            );
            app.MapPost(
                    "/api/account/login",
                    async (
                        UserManager<AppUser> userManager,
                        TokenService tokenService,
                        LoginDto loginDto
                    ) =>
                    {
                        var user = await userManager.FindByEmailAsync(loginDto.Email);
                        if (user is null)
                            return Results.Unauthorized();

                        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

                        var userObj = await CreateUserObject(tokenService, user, userManager);
                        return result ? Results.Ok(userObj) : Results.Unauthorized();
                    }
                )
                .AllowAnonymous();

            app.MapPost(
                    "/api/account/register",
                    async (
                        UserManager<AppUser> userManager,
                        TokenService tokenService,
                        IValidator<RegisterDto> validator,
                        RegisterDto registerDto
                    ) =>
                    {
                        var validationResult = await validator.ValidateAsync(registerDto);

                        // Check for clean data
                        if (!validationResult.IsValid)
                            return Results.ValidationProblem(
                                CreateValidationDictionary(validationResult.Errors)
                            );

                        AppUser newUser = new AppUser
                        {
                            Email = registerDto.Email,
                            UserName = registerDto.Username,
                            DisplayName = registerDto.DisplayName,
                        };

                        var result = await userManager.CreateAsync(newUser, registerDto.Password);

                        if (!result.Succeeded) return Results.BadRequest(result.Errors);
                        await userManager.AddToRoleAsync(newUser, "User");
                        var userObj = await CreateUserObject(tokenService, newUser, userManager);
                        return Results.Ok(userObj);
                    }
                )
                .AllowAnonymous();
        }

        private static async Task<AppUserDto> CreateUserObject(TokenService tokenService, AppUser newUser, UserManager<AppUser> userManager)
        {
            return new AppUserDto
            {
                Id = newUser.Id,
                DisplayName = newUser.DisplayName,
                UserName = newUser.UserName!,
                Email = newUser.Email!,
                Token = await tokenService.CreateToken(newUser),
                Roles = await userManager.GetRolesAsync(newUser)
            };
        }

        private static IDictionary<string, string[]> CreateValidationDictionary(
            IEnumerable<ValidationFailure> errors
        )
        {
            return errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    validationFailures => validationFailures.Key,
                    validationFailures => validationFailures.Select(e => e.ErrorMessage).ToArray()
                );
        }
    }
}