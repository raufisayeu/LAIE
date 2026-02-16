namespace LAIE.Domain.Entities;

public sealed class UserRole
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = default!;
}
