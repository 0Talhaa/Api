namespace _2302C2CodeFirstAPI.Models
{
    public class EventDTO
    {
        public int? Id { get; set; }
        public string CustomerName { get; set; }
        public int NoOfGuests { get; set; }
        public DateTime Date { get; set; }
        public int eventTypeId { get; set; }
    }
}
