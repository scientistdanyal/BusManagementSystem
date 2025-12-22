namespace BusManagementSystem.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public int RouteId { get; set; }

        // Navigation properties
        public Bus? Bus { get; set; }
        public BusRoute? Route { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
