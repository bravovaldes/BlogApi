namespace BlogApi.Models;

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public string Author { get; set; } = "Anonyme";
    public string Date { get; set; } = "";
    public string ReadTime { get; set; } = "";
    public int Views { get; set; } = 0;
    public int Likes { get; set; } = 0;
    public int Dislikes { get; set; } = 0;
}
