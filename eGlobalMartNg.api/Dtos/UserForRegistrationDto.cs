using System.ComponentModel.DataAnnotations;

namespace eGlobalMartNg.api.Dtos
{
    public class UserForRegistrationDto
    {
       
        [Required]
        public string  Username { get; set; }       
        [Required]
        [StringLength(15,MinimumLength=6,ErrorMessage="You must specify password between 6 and 15 characters")]
        public string  Password { get; set; }  
        [Required]
         public string  Firstname { get; set; }  
         [Required]
         public string  Lastname { get; set; }  
       
    }
}