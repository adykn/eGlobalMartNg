namespace eGlobalMartNg.api.Dtos
{
    public class UserForLoginDto
    {
         public UserForLoginDto()
        {
            Rememberme =false;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Rememberme { get; set; }
    }
}