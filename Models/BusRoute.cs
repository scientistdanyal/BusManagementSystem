namespace BusManagementSystem.Models
{
    public class BusRoute
    {
        public int Id { get; set; }

        public required string FromCity { get; set; }
        public required string ToCity { get; set; }

        public double DistanceKm { get; set; }
    }

}
