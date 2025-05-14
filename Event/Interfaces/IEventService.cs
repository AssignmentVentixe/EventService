using System.Linq.Expressions;
using Event.Data.Entities;
using Event.Models;

namespace Event.Interfaces
{
    public interface IEventService
    {
        Task<EventEntity> CreateEventAsync(EventDto registrationForm);
        Task<bool> DeleteEventAsync(string id);
        Task<IEnumerable<EventEntity>> GetAllEventsAsync();
        Task<EventEntity> GetByExpressionAsync(Expression<Func<EventEntity, bool>> expression);
        Task<EventEntity> UpdateEventAsync(string id, EventDto updateForm);
    }
}