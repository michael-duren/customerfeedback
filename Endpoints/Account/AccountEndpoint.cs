using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using AutoMapper;
using CustomerFeedback.Context;
using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
using CustomerFeedback.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace CustomerFeedback.Endpoints.Account
{
    public static class AccountEndpoint
    {
        public static void MapAccountEndpoints(this WebApplication app)
        {
            app.MapGet("/api/account", GetLoggedInUser)
                .WithName("GetLoggedInUser")
                .Produces<AppUserDto>(200, "application/json");

            app.MapGet("/api/admin/users", GetAllUsers)
                .WithName("GetAllUsers")
                .Produces<List<AppUserDisplayDto>>(contentType: "application/json")
                .Produces(StatusCodes.Status401Unauthorized)
                .AllowAnonymous();
                // .RequireAuthorization("admin_access");

            app.MapPost("/api/account/login", LoginUser)
                .WithName("Login")
                .Accepts<LoginDto>("application/json")
                .Produces<AppUserDto>(200, "application/json")
                .Produces(StatusCodes.Status401Unauthorized)
                .AllowAnonymous();

            app.MapPost("/api/account/register", RegisterUser)
                .WithName("Register")
                .Accepts<RegisterDto>("application/json")
                .Produces<AppUserDto>(200, "application/json")
                .Produces(StatusCodes.Status401Unauthorized)
                .AllowAnonymous();
        }


        private static async Task<IResult> GetAllUsers(UserManager<AppUser> userManager, AppDbContext context, IMapper mapper)
        {
            var users = await context.AppUsers.Include(u=>u.Feedbacks).ToListAsync();
            var userDisplayDto = new List<AppUserDisplayDto>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var newUserDisplayDto = new AppUserDisplayDto()
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Roles = roles,
                    Feedbacks = mapper.Map<List<UserFeedbackDto>>(user.Feedbacks)
                };
                userDisplayDto.Add(newUserDisplayDto);
            }

            return Results.Ok(userDisplayDto);
        }

        private static async Task<IResult> GetLoggedInUser(
            UserManager<AppUser> userManager,
            TokenService tokenService,
            ClaimsPrincipal claimsPrincipal
        )
        {
            var user = await userManager.FindByEmailAsync(
                claimsPrincipal.FindFirstValue(ClaimTypes.Email)!
            );


            if (user is null) return Results.BadRequest();
            var userObj = await CreateUserObject(tokenService, user, userManager);
            return Results.Ok(userObj);
        }

        private static async Task<IResult> LoginUser(
            UserManager<AppUser> userManager,
            TokenService tokenService,
            LoginDto loginDto
        )
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                return Results.Unauthorized();

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

            var userObj = await CreateUserObject(tokenService, user, userManager);
            return result ? Results.Ok(userObj) : Results.Unauthorized();
        }

        private static async Task<IResult> RegisterUser(
            UserManager<AppUser> userManager,
            TokenService tokenService,
            IValidator<RegisterDto> validator,
            RegisterDto registerDto
        )
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

        private static async Task<AppUserDto> CreateUserObject(TokenService tokenService, AppUser newUser,
            UserManager<AppUser> userManager)
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