namespace Database.Core.Entities;

public class User : IAuditableEntity
{
    public Guid Id { get; set; }

    public required string Email { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }
}
