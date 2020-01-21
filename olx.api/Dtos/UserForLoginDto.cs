using System.ComponentModel.DataAnnotations ;

namespace olx.api.Dto 
{
    public class UserForLoginDto 
    {
        [Required]
        public string Username {get; set; }
        [Required]
        public string Password {get; set; }

        public override string ToString()
        {
            return "UserForLoginDto: " +  Username + ":" + Password ;
        }
    }
    
}