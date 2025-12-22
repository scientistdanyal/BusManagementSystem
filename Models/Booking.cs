namespace BusManagementSystem.Models
{
    public class Booking
    {

        public int Id { get; set; }
        public int TipId { get; set; }
        public int PassengerId { get; set; }
        public int SeatNumber { get; set;  }
        public DateTime BookingDate { get; set; }
    }
}
