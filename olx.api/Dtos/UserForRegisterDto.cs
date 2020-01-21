using System.ComponentModel.DataAnnotations ;

namespace olx.api.Dto 
{
    public class UserForRegistrationDto 
    {
        [Required]
        public string Username {get; set; }
        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage="You must specify password between 4 & 8")]
        public string Password {get; set; }
    }
    
}