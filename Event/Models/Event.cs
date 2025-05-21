using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Event
{
    public string Id { get; set; } = null!;
    public string EventName { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Price { get; set; }
}
