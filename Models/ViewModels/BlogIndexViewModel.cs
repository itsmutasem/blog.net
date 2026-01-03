using Blog.Models;
using System.Collections.Generic;

namespace Blog.Models.ViewModels
{
    public class BlogIndexViewModel
    {
        public List<BlogPost> Blogs { get; set; } = new();
        public int TotalBlogs { get; set; }
        public int TotalUsers { get; set; }
        
        public string? SelectedCategory { get; set; }
    }
}