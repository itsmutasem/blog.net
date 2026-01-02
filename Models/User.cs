using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty; // store hashed password

        [Required]
        public string Role { get; set; } = "User"; // Admin or User

        // Navigation property
        public ICollection<BlogPost> Blogs { get; set; } = new List<BlogPost>();
    }
}