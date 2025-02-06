using HotelRoomBookingAPI.AuthModels;
using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.Repository;
using HotelRoomBookingAPI.Services;
using HotelRoomBookingAPI.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBookingAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GuestsBookingsController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("GuestsBookingsController");

        private readonly LP_HotelRoomBooking_EFDBContext _context;
        private readonly IdentityHelper _identityHelper;

        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _authContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GuestsBookingsRepo _repo;

        public GuestsBookingsController(LP_HotelRoomBooking_EFDBContext context,
            UserManager<ApplicationUser> userManager,
            AuthenticationContext authContext, RoleManager<IdentityRole> roleManager, GuestsBookingsRepo repo)
        {
            _context = context;
            _userManager = userManager;
            _authContext = authContext;
            _roleManager = roleManager;
            _repo = repo;

            _identityHelper = new IdentityHelper(userManager, authContext, roleManager);
        }

        // GET: api/GuestsBookings/MyBookings
        [EnableCors("AllowOrigin")]
        [HttpGet("MyBookings")]
        public async Task<IActionResult> GetMyBookings([FromQuery] string username)
        {
            logger.Info("GuestsBookingsController - GET MyBookings: /gst");
            // Find the user ID based on the username
            var guest = await _context.Guests.FirstOrDefaultAsync(g => g.UserName == username);
            if (guest == null)
            {
                logger.Warn("Guest not found for the given username.");
                return NotFound();
            }

            GuestsBookingsRepo repo = new GuestsBookingsRepo(_context);
            var bookings = repo.GetGuestsBookings(guest.GuestId);

            // Transform the bookings to match GuestsBookingsVM
            var bookingsVM = bookings.Select(b => new GuestsBookingsVM
            {
                BookingId = b.BookingId,
                GuestId = b.GuestId,
                RoomId = b.RoomId,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                TotalPrice = b.TotalPrice
            }).ToList();

            return Ok(bookingsVM);
        }

        // GET: /gst
        // GET: /gst
        [EnableCors("AllowOrigin")]
        [HttpGet]
        //[EnableCors("AllowOrigin")]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            logger.Info("GuestsBookingsController - GET all Bookings: /gst");

            try
            {
                // Fetch all bookings
                var allBookings = _context.Bookings
                    .Select(b => new GuestsBookingsVM
                    {
                        BookingId = b.BookingId,
                        GuestId = b.GuestId,
                        RoomId = b.RoomId,
                        CheckinDate = b.CheckinDate,
                        CheckoutDate = b.CheckoutDate,
                        TotalPrice = b.TotalPrice
                    })
                    .ToList();

                return Ok(allBookings);
            }
            catch (Exception ex)
            {
                logger.Warn($"Error in GuestsBookingsController - GET all Bookings: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }







        // get: /gst/gstbookings/5
        [EnableCors("AllowOrigin")]
        [HttpGet("gstbookings/{id}")]
        //[Authorize]
        public async Task<IActionResult> Get(int id)
        {
            logger.Info("GuestsBookingsController - GET: gst/gstbookings/" + id);

            List<GuestsBookingsVM> guestsBookings = new List<GuestsBookingsVM>();

            // Get the UserID from claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                string userId = userIdClaim.Value;


                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    string username = user.UserName;


                    if (!string.IsNullOrEmpty(username))
                    {

                        guestsBookings = _repo.GetGuestsBookings(id).ToList();

                        return Ok(guestsBookings);
                    }
                }
            }

            logger.Warn("GuestsBookingsController - GET: gst/gstbookings/" + id + " - Logged in user not authorized.");
            return BadRequest(new { message = "Not authorized." });
        }




        //Currently throwing returning Firbidden




        // PUT: api/Bookings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookings(int id, GuestsBookingsVM updatedBooking)
        {
            logger.Info("GuestsBookingsController Update Booking by Id");
            if (id != updatedBooking.BookingId)
            {
                return BadRequest($"The Booking ID in the request body ({updatedBooking.BookingId}) does not match the ID in the URL ({id}).");
            }

            try
            {
                // Retrieve the existing booking from the database
                var existingBooking = await _context.Bookings.FindAsync(id);

                if (existingBooking == null)
                {
                    return NotFound("Booking not found.");
                }

                // Update the properties of the existing booking
                existingBooking.GuestId = updatedBooking.GuestId;
                existingBooking.RoomId = updatedBooking.RoomId;
                existingBooking.CheckinDate = updatedBooking.CheckinDate;
                existingBooking.CheckoutDate = updatedBooking.CheckoutDate;
                existingBooking.TotalPrice = updatedBooking.TotalPrice;

                // Update the modified state in the context and save changes
                _context.Entry(existingBooking).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Return the updated booking in the GuestsBookingsVM format
                return Ok(updatedBooking);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency conflict occurred. Another user may have updated the booking information.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        // DELETE: api/Bookings/5
        // [EnableCors] 
        [EnableCors("AllowOrigin")]// Default policy.
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBookings(int id)
        {

            logger.Info("GuestsBookingsController - DELETE booking by Id");
            try
            {
                var Bookings = await _context.Bookings.FindAsync(id);
                if (Bookings == null)
                {
                    return NotFound("Booking not found.");
                }

                _context.Bookings.Remove(Bookings);
                await _context.SaveChangesAsync();

                return Bookings;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Failed to delete the booking. Please try again later.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        // POST: api/GuestsBookings
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingVM bookingVM)
        {
            logger.Info("GuestsBookingsController - POST CreateBooking");

            if (bookingVM == null)
            {
                return BadRequest("Invalid booking data.");
            }

            try
            {
             
                var guest = await _context.Guests.FirstOrDefaultAsync(g => g.UserName == bookingVM.GuestUsername);
                if (guest == null)
                {
                    return NotFound("Guest not found.");
                }

               
                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == bookingVM.RoomNumber);
                if (room == null)
                {
                    return NotFound("Room not found.");
                }

                // Calculate the total price
                //var numberOfNights = (bookingVM.CheckoutDate - bookingVM.CheckinDate).TotalDays;
                //if (numberOfNights <= 0)
                //{
                //    return BadRequest("Checkout date must be after checkin date.");
                //}
                //var totalPrice = numberOfNights * room.NightPrice;


              
                var numberOfNights = (bookingVM.CheckoutDate.Date - bookingVM.CheckinDate.Date).Days;
                if (numberOfNights <= 0)
                {
                    return BadRequest("Checkout date must be after checkin date.");
                }
                //var totalPrice = numberOfNights * room.NightPrice;
                var totalPrice = Math.Round(numberOfNights * room.NightPrice, 2);

                // Map CreateBookingVM to Booking entity
                var booking = new Booking
                {
                    GuestId = guest.GuestId,
                    RoomId = room.RoomId,
                    CheckinDate = bookingVM.CheckinDate,
                    CheckoutDate = bookingVM.CheckoutDate,
                    TotalPrice = totalPrice
                };

                // Add the booking to the context
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();

                // Map the created booking back to GuestsBookingsVM
                var createdBookingVM = new GuestsBookingsVM
                {
                    BookingId = booking.BookingId,
                    GuestId = booking.GuestId,
                    RoomId = booking.RoomId,
                    CheckinDate = booking.CheckinDate,
                    CheckoutDate = booking.CheckoutDate,
                    TotalPrice = booking.TotalPrice
                };

                return CreatedAtAction(nameof(Get), new { id = booking.BookingId }, createdBookingVM);
            }
            catch (Exception ex)
            {
                logger.Error($"Error in GuestsBookingsController - POST CreateBooking: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }




        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}








