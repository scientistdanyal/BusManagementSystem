namespace BusManagementSystem.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string? Description { get; set; }
    }
}