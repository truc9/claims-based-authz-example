namespace ClaimsBasedAuthz.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Tenant { get; set; }
    }

}
