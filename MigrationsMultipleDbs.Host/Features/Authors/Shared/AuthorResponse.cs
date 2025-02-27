using MigrationsMultipleDbs.Features.Books.Shared;

namespace MigrationsMultipleDbs.Features.Authors.Shared;

public sealed record AuthorResponse(Guid Id, string Name, List<BookResponse> Books);
