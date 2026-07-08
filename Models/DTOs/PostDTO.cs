namespace Tabloid.Models.DTOs;


public class PostDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public DateTime PubDate { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
}

public class CreatePostDTO
{
    public string Title { get; set; }
    public string Image { get; set; }
    public DateTime PubDate { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
}