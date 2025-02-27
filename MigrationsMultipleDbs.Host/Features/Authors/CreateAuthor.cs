using Carter;
using Database.Core;
using Database.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using MigrationsMultipleDbs.Features.Authors.Shared;

namespace MigrationsMultipleDbs.Features.Authors;

public sealed record CreateAuthorRequest(string Name);

public class CreateAuthorEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/authors", Handle);
	}

	private static async Task<IResult> Handle(
		[FromBody] CreateAuthorRequest request,
		ApplicationDbContext context,
		CancellationToken cancellationToken)
	{
		var author = new Author
		{
			Id = Guid.NewGuid(),
			Name = request.Name
		};

		context.Authors.Add(author);
		await context.SaveChangesAsync(cancellationToken);

		var response = new AuthorResponse(author.Id, author.Name, []);

		return Results.Created($"/api/authors/{author.Id}", response);
	}
}
