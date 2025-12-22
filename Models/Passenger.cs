namespace BusManagementSystem.Models
{
    public class Passenger
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Email { get; set; }
    }

}
