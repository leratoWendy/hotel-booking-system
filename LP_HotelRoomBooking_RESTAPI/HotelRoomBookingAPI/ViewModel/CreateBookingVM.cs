namespace HotelRoomBookingAPI.ViewModel
{
    public class CreateBookingVM
    {
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        //public double TotalPrice { get; set; }
        public string GuestUsername { get; set; } 
        public int RoomNumber { get; set; } 
    }

}
