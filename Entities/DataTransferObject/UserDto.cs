using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public class RegistUserDto
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        [Required (ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }
        public string Email  { get; set; }
        public string Phone { get; set; }
        [Required (ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        public System.Collections.Generic.ICollection<string> Roles { get; set; }
    }
    public class LoginUserDto
        {
            public string UserName { get; set; }    
            public string Password { get; set; }
        }
}