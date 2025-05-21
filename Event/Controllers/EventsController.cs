using Event.Interfaces;
using Event.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return (events != null) 
            ? Ok(events) 
            : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(string id)
    {
        var eventModel = await _eventService.GetByExpressionAsync(x => x.Id == id);

        return (eventModel != null)
            ? Ok(eventModel)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(EventDto eventDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdEvent = await _eventService.CreateEventAsync(eventDto);

        return (createdEvent != null) 
            ? Ok(createdEvent) 
            : BadRequest("Failed to create event");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(string id, EventDto updateForm)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var entityToUpdate = await _eventService.UpdateEventAsync(id, updateForm);
        return (entityToUpdate != null)
            ? Ok() 
            : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        bool isDeleted = await _eventService.DeleteEventAsync(id);
        return (isDeleted)
            ? Ok() 
            : BadRequest();
    }
}
