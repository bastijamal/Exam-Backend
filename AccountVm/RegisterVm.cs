using System.ComponentModel.DataAnnotations;

namespace Boocic.AccountVm
{
    public class RegisterVm
    {
        [MinLength(3)]
        [MaxLength(13)]
        public string Name { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


    }
}
