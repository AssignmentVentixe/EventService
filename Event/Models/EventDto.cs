namespace Event.Models;

public class EventDto
{
    public string? Id { get; set; }
    public string? EventName { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; } 
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
