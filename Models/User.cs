using System.ComponentModel.DataAnnotations;

namespace Blog.Data
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required, MinLength(6)]
        public string Password { get; set; } = null!; // hashed password

        [Required]
        public string Role { get; set; } = "User"; // default role
    }
}