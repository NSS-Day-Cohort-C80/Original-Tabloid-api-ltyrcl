using Tabloid.Models.DTO;

namespace Tabloid.Models.DTOs;

public class ReactionDTO
{
    public int Id { get; set; }
    public string EmojiId { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public UserProfileDTO User { get; set; } = null;
    public PostDTO Post { get; set; } = null;
    public EmojiDTO Emoji {get; set; } = null;
}

public class CreateReactionDTO
{
    public string EmojiId { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}