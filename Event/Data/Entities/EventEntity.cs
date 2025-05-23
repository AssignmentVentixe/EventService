using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Entities;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? ImageUrl { get; set; }
    public string EventName { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime? StartDate { get; set; } 

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }
}