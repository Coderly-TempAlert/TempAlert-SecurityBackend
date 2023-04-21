using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser
{
    public string Names { get; set; }
    public string LastName { get; set; }
    public string MotherLastName { get; set; }

}
