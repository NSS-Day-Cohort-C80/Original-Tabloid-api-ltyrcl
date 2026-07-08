namespace Tabloid.Models.DTOs;


public class PostDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public DateTime PubDate { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public UserProfileDTO User { get; set; } = null;
    public CategoryDTO Category { get; set; } = null;
    public List<CommentDTO> Comments { get; set; } = new();
    public List<PostTagDTO> PostTags { get; set; } = new();
    public List<ReactionDTO> Reaction { get; set; } = new();
}

public class CreatePostDTO
{
    public string Title { get; set; }
    public string Image { get; set; }
    public DateTime PubDate { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
}