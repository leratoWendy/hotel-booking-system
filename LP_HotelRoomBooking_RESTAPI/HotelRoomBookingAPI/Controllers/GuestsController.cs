using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Text.Json.Serialization;
using System.Text.Json;
using log4net;
//using SecureTradeCoreRestAPI.ViewModel; 

namespace HotelRoomBookingAPI.Controllers
{
    [EnableCors("AllowOrigin")] // Enable CORS for this controller
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("GuestsController");
        private readonly LP_HotelRoomBooking_EFDBContext _context;

        public GuestsController(LP_HotelRoomBooking_EFDBContext context)
        {
            _context = context;
        }

        //read data
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestVM>>> GetGuests()
        {
            logger.Info("GuestsController - GET all Guests: /gst");
            var guests = await _context.Guests.ToListAsync();

            var guestVMs = guests.Select(guest => new GuestVM
            {
                GuestId = guest.GuestId,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                IdentityUserName = guest.UserName,
                Email = guest.Email,
                Phone = guest.Phone
            }).ToList();

            return guestVMs;
        }



        //update guest
        [EnableCors("AllowOrigin")]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuests(int id, Guest guests)
        {
            logger.Info("GuestsController - PUT Guests");
            ////if (id != guests.GuestId)
            //{
            //    logger.Warn("The Id provided is not assigned to any guest");
            //    return BadRequest("Invalid ID provided.");
            //}
            Guest guest = _context.Guests.Where(a => a.GuestId == id).FirstOrDefault();
           if (guest == null)
            {
                logger.Warn("The Id provided is not assigned to any guest");
                    return BadRequest("Invalid ID provided.");

            }
            guest.FirstName = guests.FirstName;
            guest.LastName = guests.LastName;
            guests.UserName = guest.UserName;
            guest.Email = guest.Email;
            guest.Phone = guest.Phone;

            _context.Entry(guest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (GuestsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }




        //read guest info by id
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetGuest(int id)
        {

            logger.Info("Reading guest info by Id");
            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            guest.Bookings = await GetAllBookingsByGuestIdAsync(guest.GuestId);

            // Create JsonSerializerOptions with ReferenceHandler.Preserve
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // Other options as needed
            };

            // Serialize the guest object using JsonSerializerOptions
            var serializedGuest = JsonSerializer.Serialize(guest, options);

            // Return the serialized guest object as JSON
            return Content(serializedGuest, "application/json");
        }



        [EnableCors("AllowOrigin")]
        [HttpGet("UserName/{name}")]
        public async Task<ActionResult<Guest>> GetGuestByName(string name)
        {
            logger.Info("GuestsController get guest by name");
            var guest = await _context.Guests.FirstOrDefaultAsync(x => x.UserName == name);

            if (guest == null)
            {
                return NotFound();
            }

            guest.Bookings = await GetAllBookingsByGuestIdAsync(guest.GuestId);

            // Create JsonSerializerOptions with ReferenceHandler.Preserve
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // You can also configure other options here
            };

            // Serialize the guest object using JsonSerializer and the options
            var jsonString = JsonSerializer.Serialize(guest, options);

            // Return the serialized JSON string
            return Ok(jsonString);
        }

        //create guest
        [EnableCors("AllowOrigin")]
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Guest>> PostGuests([FromBody] GuestVM guest)
        {
            logger.Info("GuestsController - POST Guests: /gst");
            var newGuest = new Guest
            {
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Email = guest.Email,
                UserName = guest.IdentityUserName??guest.FirstName + guest.LastName,
                Phone = guest.Phone
            };

            _context.Guests.Add(newGuest);
            await _context.SaveChangesAsync();

            return Ok(newGuest);
        }


        // DELETE: api/Guests/5
        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<ActionResult<Guest>> DeleteGuests(int id)
        {
            logger.Info("GuestsController - DELETE Guests");
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                logger.Warn("Guest not found");
                return NotFound();
            }

            // Load related bookings and remove them
            var bookings = await _context.Bookings.Where(b => b.GuestId == id).ToListAsync();
            _context.Bookings.RemoveRange(bookings);

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();

            return guest;
        }


        private async Task<List<Booking>> GetAllBookingsByGuestIdAsync(int guestId)
        {
            return await _context.Bookings.Where(b => b.GuestId == guestId).ToListAsync();
        }

        private bool GuestsExists(int id)
        {
            return _context.Guests.Any(e => e.GuestId == id);
        }
    }
}

