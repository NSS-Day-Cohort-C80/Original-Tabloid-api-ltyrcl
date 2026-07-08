namespace Tabloid.Models.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
}

public class CreateCommentDTO
{
    public int PostId { get; set; }

    public string Subject { get; set; }

    public string Content { get; set; }
}