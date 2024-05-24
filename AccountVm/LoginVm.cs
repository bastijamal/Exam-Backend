using System.ComponentModel.DataAnnotations;

namespace Boocic.AccountVm
{
    public class LoginVm
    {
        
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; internal set; }
    }
}
