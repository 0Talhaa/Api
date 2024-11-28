using _2302C2CodeFirstAPI.Data;
using _2302C2CodeFirstAPI.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2302C2CodeFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventOrganizerController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public EventOrganizerController(ApplicationDbContext _db)
        {
            db= _db;
            
        }
        [HttpGet]
        public IActionResult GetEvents() {

            var events = db.Events.Include(q=>q.EventType);
            return Ok(events);
              
        }


        ///api/EventOrganizer/
        [HttpGet("EventTypes/")]
        public IActionResult GetEventTypes()
        {
            var events = db.EventTypes;
            return Ok(events);

        }

        [HttpPost]
        public IActionResult AddEvent(EventDTO ev)
        {
            if (ev != null)
            {
                Event newEvent = new Event()
                {
                    NoOfGuests = ev.NoOfGuests,
                    CustomerName = ev.CustomerName,
                    Date = ev.Date,
                    eventTypeId = ev.eventTypeId,
                };
                var addedEvent = db.Events.Add(newEvent);
                db.SaveChanges();
                return Ok(addedEvent.Entity);
            }
            else
            {
                return BadRequest("INVALID DATA");
            }
        

        }

        [HttpGet("{eventId}")]
        public IActionResult GetEvent(int eventId)
        {
            var Event = db.Events.Include(q => q.EventType).SingleOrDefault(j=>j.Id == eventId);
            return Ok(Event);

        }

        [HttpPut]

        public IActionResult EditEvent(EventDTO ev)
        {if (ev != null)
            {
                var Event = db.Events.Find(ev.Id);
                if(Event != null)
                {
                   Event.NoOfGuests = ev.NoOfGuests;
                    Event.CustomerName = ev.CustomerName;
                    Event.Date = ev.Date;
                    Event.eventTypeId = ev.eventTypeId;
                    var editedEvent = db.Events.Update(Event);
                    db.SaveChanges();
                    return Ok(editedEvent.Entity);
                }
                else
                {
                    return BadRequest("Event not found");
                }
              
            }
            else
            {
                return BadRequest("INVALID DATA");
            }


        }

        [HttpDelete]
        public IActionResult DeleteEvent(EventDTO ev)
        {
            if (ev != null)
            {
                var Event = db.Events.Find(ev.Id);
                if (Event != null)
                {
                    var deletedEvent = db.Events.Remove(Event);
                    db.SaveChanges();
                    return Ok(deletedEvent.Entity);
                }
                else
                {
                    return BadRequest("Event not found");
                }

            }
            else
            {
                return BadRequest("INVALID DATA");
            }


        }
        [HttpGet("Search/{q}")]
        public IActionResult SearchEvent(string q)
        {
            if (q != null)
            {
                //Exact match
                //var Event = db.Events.Include(t=>t.EventType).Where(t=>t.CustomerName == q || t.EventType.Type==q );

                //Partial match
                var Event = db.Events.Include(t => t.EventType).Where(t => t.CustomerName.Contains(q) || t.EventType.Type.Contains(q));

                if (Event != null)
                {

                    return Ok(Event);
                }
                else
                {
                    return Ok("Event not found");
                }

            }
            else
            {
                return BadRequest("INVALID DATA");
            }
            //return BadRequest("INVALID DATA");
        }

        [HttpGet("Pagination/{pageNo}/{pageSize}")]
        public IActionResult Pagination(int pageNo=1, int pageSize=2)
        {
            int pageno = pageNo;
            if (pageno < 1)  pageno = 1; 
            int pagesize = pageSize;
            if (pagesize < 1) pagesize = 1;
                                                                //5 -4 *5=> 3 *5=15
            var getEvents = db.Events.Include(i => i.EventType).Skip((pageno -1) * pagesize).Take(pagesize);

            if (getEvents != null)
            {
                    return Ok(getEvents);
            }
            else
            {

                return Ok("End of list");
            }
            //return BadRequest("INVALID DATA");
        }

    }
}
