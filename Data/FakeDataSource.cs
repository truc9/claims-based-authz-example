using ClaimsBasedAuthz.Models;

namespace ClaimsBasedAuthz.Data;

public static class FakeDataSource
{
    /// <summary>
    /// For the demonstration, keep it simple by this fake data
    /// In real project, we might want to fetch from DB
    /// </summary>
    public static IReadOnlyList<User> FakeUsers = new List<User>
    {
        new User{UserName = "user1", Password = "user1", Tenant="TenantA"},
        new User{UserName = "user2", Password = "user2", Tenant="TenantB"},
    };
}
