using System.Diagnostics;
using System.Linq.Expressions;
using Event.Data.Entities;
using Event.Extensions;
using Event.Factories;
using Event.Interfaces;
using Event.Models;

namespace Event.Services;

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

    public async Task<IEnumerable<EventEntity>> GetAllEventsAsync()
    {
        var entities = await _eventRepository.GetAllAsync();
        if (entities == null)
            return null!;
        var eventModels = entities.Select(x => x.MapTo<EventEntity>()).ToList();
        return eventModels;
    }

    public async Task<EventEntity> GetByExpressionAsync(Expression<Func<EventEntity, bool>> expression)
    {
        var entity = await _eventRepository.GetAsync(expression);

        var eventModel = entity.MapTo<EventEntity>();

        return eventModel;
    }

    public async Task<EventEntity> UpdateEventAsync(string id, EventDto updateForm)
    {
        if (updateForm == null)
            return null!;

        var entityToUpdate = await _eventRepository.GetAsync(x => x.Id == id);
        if (entityToUpdate == null)
            return null!;

        EventFactory.UpdateEventEntity(entityToUpdate, updateForm);

        await _eventRepository.BeginTransactionAsync();
        await _eventRepository.UpdateAsync(x => x.Id == id, entityToUpdate);
        var saved = await _eventRepository.SaveAsync();
        if (!saved)
        {
            await _eventRepository.RollbackTransactionAsync();
            return null!;
        }

        await _eventRepository.CommitTransactionAsync();
        return entityToUpdate ?? null!;
    }
}
