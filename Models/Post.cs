namespace Tabloid.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public DateTime PubDate { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public bool Approved { get; set; }
    public UserProfile User { get; set; } = null;
    public Category Category { get; set; } = null;
    public List<Comment> Comments { get; set; } = new();
    public List<PostTag> PostTags { get; set; } = new();
    public List<Reaction> Reaction { get; set; } = new();
}