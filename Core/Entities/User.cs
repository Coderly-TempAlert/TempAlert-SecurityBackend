namespace Core.Entities;

public class User : BaseEntity
{
    public string Names { get; set; }
    public string LastName { get; set; }
    public string MotherLastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Rol> Roles { get; set; } = new HashSet<Rol>();
    //public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    public ICollection<UserRols> UserRols { get; set; }
}
