using LAIE.Domain.Common;

namespace LAIE.Domain.Entities;

public sealed class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public ICollection<UserRole> Roles { get; private set; } = new List<UserRole>();
    public ICollection<Decision> Decisions { get; private set; } = new List<Decision>();

    private User() { }

    public User(string email, string passwordHash, string fullName)
    {
        Email = email;
        PasswordHash = passwordHash;
        FullName = fullName;
    }
}
