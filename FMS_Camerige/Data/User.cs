namespace FMS_Camerige.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Avoid storing plaintext passwords in production
    }

}
