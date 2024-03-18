using Postgrest.Attributes;
using Postgrest.Models;

namespace SendTracker.Models;

[Postgrest.Attributes.Table("Users")]
public class User : BaseModel{
    [PrimaryKey("id")] public string Id { get; set; }
    [Column("username")] public string Username { get; set; }
    [Column("userId")] public string UserId { get; set; }

    public User() { }

    public User(string userId, string? username) {
        UserId = userId;
        Username = username;
    }
}