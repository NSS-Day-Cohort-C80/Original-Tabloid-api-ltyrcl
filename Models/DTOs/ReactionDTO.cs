namespace Tabloid.Models.DTOs;

public class ReactionDTO
{
    public int Id { get; set; }
    public string Emoji { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public UserProfileDTO User { get; set; } = null;
    public PostDTO Post { get; set; } = null;
}

public class CreateReactionDTO
{
    public string Emoji { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}