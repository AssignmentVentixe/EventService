using Event.Data.Entities;
using Event.Models;

namespace Event.Factories;

public class EventFactory
{
    public static void UpdateEventEntity(EventEntity currentEntity, EventDto updateForm)
    {
        currentEntity.EventName = updateForm.EventName!;
        currentEntity.Description = updateForm.Description!;
        currentEntity.Location = updateForm.Location!;
        currentEntity.StartDate = updateForm.StartDate!;
        currentEntity.EndDate = updateForm.EndDate!;

    }
}