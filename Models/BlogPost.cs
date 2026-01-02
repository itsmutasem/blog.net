using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }

    public int UserId { get; set; }

    public required User Author { get; set; }

    [Required]
    public required string Category { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}