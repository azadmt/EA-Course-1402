namespace Identity.Contract
{
    public class SecurityContext
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public DateTime ExpireAt { get; set; }

        public string[] Permissions { get; set; }
    }
}