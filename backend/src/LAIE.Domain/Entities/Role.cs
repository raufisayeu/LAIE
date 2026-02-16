using LAIE.Domain.Common;

namespace LAIE.Domain.Entities;

public sealed class Role : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public ICollection<UserRole> Users { get; private set; } = new List<UserRole>();
}
