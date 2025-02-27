using Carter;
using Database.Core;
using Database.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using MigrationsMultipleDbs.Features.Users.Shared;

namespace MigrationsMultipleDbs.Features.Users;

public sealed record CreateUserRequest(string Email);

public class CreateUserEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/users", Handle);
	}

	private static async Task<IResult> Handle(
		[FromBody] CreateUserRequest request,
		ApplicationDbContext context,
		CancellationToken cancellationToken)
	{
		var user = new User
		{
			Id = Guid.NewGuid(),
			Email = request.Email
		};

		context.Users.Add(user);
		await context.SaveChangesAsync(cancellationToken);

		var response = new UserResponse(user.Id, user.Email);
		return Results.Created($"/api/users/{user.Id}", response);
	}
}

