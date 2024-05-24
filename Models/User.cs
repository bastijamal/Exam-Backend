using Microsoft.AspNetCore.Identity;

namespace Boocic.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
