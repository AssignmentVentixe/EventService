using System.Diagnostics;
using System.Linq.Expressions;
using API.Data.Entities;
using API.Extensions;
using API.Factories;
using API.Interfaces;
using API.Models;

namespace API.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<EventEntity> CreateEventAsync(EventDto registrationForm)
    {
        if (registrationForm == null)
            return null!;

        await _eventRepository.BeginTransactionAsync();
        var entity = registrationForm.MapTo<EventEntity>();
        await _eventRepository.AddAsync(entity);
        var saved = await _eventRepository.SaveAsync();
        if (!saved)
        {
            await _eventRepository.RollbackTransactionAsync();
            return null!;
        }

        await _eventRepository.CommitTransactionAsync();
        return entity;
    }

    public async Task<bool> DeleteEventAsync(string id)
    {
        var entity = await _eventRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _eventRepository.BeginTransactionAsync();
        await _eventRepository.DeleteAsync(x => x.Id == id);
        var saved = await _eventRepository.SaveAsync();
        if (!saved)
        {
            await _eventRepository.RollbackTransactionAsync();
            return false;
        }

        await _eventRepository.CommitTransactionAsync();
        return true;
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        var entities = await _eventRepository.GetAllAsync();
        if (entities == null)
            return null!;
        var eventModels = entities.Select(x => x.MapTo<Event>()).ToList();
        return eventModels;
    }

    public async Task<Event> GetByExpressionAsync(Expression<Func<EventEntity, bool>> expression)
    {
        var entity = await _eventRepository.GetAsync(expression);

        var eventModel = entity.MapTo<Event>();

        return eventModel;
    }

    public async Task<bool> UpdateEventAsync(string id, EventDto updateForm)
    {
        if (updateForm == null)
            return false;

        var entityToUpdate = await _eventRepository.GetAsync(x => x.Id == id);
        if (entityToUpdate == null)
            return false;

        EventFactory.UpdateEventEntity(entityToUpdate, updateForm);

        await _eventRepository.BeginTransactionAsync();
        await _eventRepository.UpdateAsync(x => x.Id == id, entityToUpdate);
        var saved = await _eventRepository.SaveAsync();
        if (!saved)
        {
            await _eventRepository.RollbackTransactionAsync();
            return false;
        }

        await _eventRepository.CommitTransactionAsync();
        return true;
    }
}
