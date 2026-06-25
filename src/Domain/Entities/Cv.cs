namespace Domain.Entities;

public class Cv
{
    public int Id { get; private set; }

    public string Url { get; private set; }

    public string PublicId { get; private set; }

    public int UserId { get; private set; }

    private Cv() 
    {
        Url = string.Empty;
        PublicId = string.Empty;
    }

    public Cv(
        string url,
        string publicId,
        int userId)
    {
        Url = url;
        PublicId = publicId;
        UserId = userId;
    }

    public void Update(
        string url,
        string publicId)
    {
        Url = url;
        PublicId = publicId;
    }
}