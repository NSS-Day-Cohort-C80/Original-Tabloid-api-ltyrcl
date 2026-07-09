namespace Tabloid.Models;

public class Reaction
{
    public int Id { get; set; }
    public int EmojiId { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public UserProfile User { get; set; } = null;
    public Post Post { get; set; } = null;
    public Emoji Emoji { get; set; } = null;
}