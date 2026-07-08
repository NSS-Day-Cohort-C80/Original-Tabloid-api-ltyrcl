namespace Tabloid.Models.DTOs;

public class TagDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CreateTagDTO
{
    public string Name { get; set; }
}