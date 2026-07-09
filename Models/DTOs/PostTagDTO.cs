namespace Tabloid.Models.DTOs;

public class PostTagDTO
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int TagId { get; set; }
    public PostDTO Post { get; set; } = null;
    public TagDTO Tag { get; set; } = null;
}

public class CreatePostTagDTO
{
    public int PostId { get; set; }
    public int TagId { get; set; }
}