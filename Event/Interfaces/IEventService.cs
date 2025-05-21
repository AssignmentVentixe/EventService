using System.Linq.Expressions;
using API.Data.Entities;
using API.Models;

namespace API.Interfaces
{
    public interface IEventService
    {
        Task<EventEntity> CreateEventAsync(EventDto registrationForm);
        Task<bool> DeleteEventAsync(string id);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetByExpressionAsync(Expression<Func<EventEntity, bool>> expression);
        Task<bool> UpdateEventAsync(string id, EventDto updateForm);
    }
}