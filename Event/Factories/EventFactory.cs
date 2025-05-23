using API.Data.Entities;
using API.Models;

namespace API.Factories;

public class EventFactory
{
    public static void UpdateEventEntity(EventEntity currentEntity, EventDto updateForm)
    {
        currentEntity.EventName = updateForm.EventName!;
        currentEntity.Description = updateForm.Description!;
        currentEntity.Location = updateForm.Location!;
        currentEntity.StartDate = updateForm.StartDate!;
        currentEntity.Price = updateForm.Price!;
    }
}