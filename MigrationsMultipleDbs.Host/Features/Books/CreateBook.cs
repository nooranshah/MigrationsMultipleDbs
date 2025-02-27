using Carter;
using Database.Core;
using Database.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using MigrationsMultipleDbs.Features.Books.Shared;

namespace MigrationsMultipleDbs.Features.Books;

public sealed record CreateBookRequest(string Title, int Year, Guid AuthorId);

public class CreateBookEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/books", Handle);
	}

	private static async Task<IResult> Handle(
		[FromBody] CreateBookRequest request,
		ApplicationDbContext context,
		CancellationToken cancellationToken)
	{
		var author = await context.Authors.FindAsync([request.AuthorId], cancellationToken);
		if (author is null)
		{
			return Results.BadRequest("Author not found");
		}

		var book = new Book
		{
			Id = Guid.NewGuid(),
			Title = request.Title,
			Year = request.Year,
			AuthorId = request.AuthorId,
			Author = author
		};

		context.Books.Add(book);
		await context.SaveChangesAsync(cancellationToken);

		var response = new BookResponse(book.Id, book.Title, book.Year, book.AuthorId);
		return Results.Created($"/api/books/{book.Id}", response);
	}
}
