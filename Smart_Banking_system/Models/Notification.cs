namespace Smart_Banking_system.Models;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public bool IsRead { get; set; } = false;  // unread by default
}