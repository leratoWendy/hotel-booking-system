using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBookingAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("GuestsController");
        private readonly LP_HotelRoomBooking_EFDBContext _context;

        public RoomsController(LP_HotelRoomBooking_EFDBContext context)
        {
            _context = context;
        }

        // GET: api/Rooms        
       [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomsVM>>> GetRooms()
        {
            logger.Info("RoomsController - GET rooms");
            try
            {
                var rooms = await _context.Rooms
                    .Select(r => new RoomsVM
                    {
                        RoomId = r.RoomId,
                        RoomNumber = r.RoomNumber,
                        RoomType = r.RoomType,
                        NightPrice = r.NightPrice
                    })
                    .ToListAsync();

                if (rooms == null || rooms.Count == 0)
                {
                    return NotFound(new { message = "No rooms found." });
                }

                return rooms;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error: " + ex.Message });
            }
        }



        // GET: api/Rooms/5
        [EnableCors("AllowOrigin")]      // Default policy.
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomsVM>> GetRooms(int id)
        {
            logger.Info("RoomsController - GET room by Id");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Rooms = await _context.Rooms.FindAsync(id);

            return Ok(Rooms);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRooms(int id, Room Rooms)
        {
            logger.Info("RoomsController - PUT room BY Id");
            if (id != Rooms.RoomId)
            {
                return BadRequest("Room ID in the request body does not match the ID in the URL.");
            }

            _context.Entry(Rooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomsExists(id))
                {
                    return NotFound("Room not found."); 
                }
                else
                {
                    return Conflict("Concurrency conflict occurred. Another user may have updated the room information."); 
                }
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Failed to update the room. Please try again later."); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}"); 
            }

        }

        // POST: api/Rooms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
         [EnableCors("AllowOrigin")]     // Default policy.
        [HttpPost]
        [Authorize] 
        public async Task<ActionResult<RoomsVM>> PostRooms(RoomsVM Rooms)
        {
            logger.Info("RoomsController - POST rooms");
            try
            {
                // Check if the input model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Create a new room entity and map data from the view model
                var roomEntity = new Room
                {
                    RoomNumber = Rooms.RoomNumber,
                    RoomType = Rooms.RoomType,
                    NightPrice = Rooms.NightPrice
                };

                // Add the new room to the context and save changes
                _context.Rooms.Add(roomEntity);
                await _context.SaveChangesAsync();

                // Return the created room view model with the new room's ID
                return CreatedAtAction(nameof(GetRooms), new { id = roomEntity.RoomId }, Rooms);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, new { message = "Internal Server Error: " + ex.Message });
            }
        }

        // DELETE: api/Rooms/5
        [EnableCors("AllowOrigin")]       // Default policy.
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRooms(int id)
        {
            logger.Info("RoomsController - DELETE room");
            try
            {
                var Rooms = await _context.Rooms.FindAsync(id);
                if (Rooms == null)
                {
                    return NotFound("Room not found.");
                }

                _context.Rooms.Remove(Rooms);
                await _context.SaveChangesAsync();

                return Rooms; 
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Failed to delete the room. Please try again later."); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}"); 
            }
        }

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
    

