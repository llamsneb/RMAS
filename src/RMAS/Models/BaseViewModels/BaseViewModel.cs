namespace RMAS.Models.BaseViewModels
{
    public abstract class BaseViewModel
    {
        public string? EventName { get; set; }
        public string? InfoMessage { get; set; }
        //public List<Reservation>? Reservations = new List<Reservation>();
        public List<Reservation>? Reservations { get; set; } = new List<Reservation>();
        public PaginatedList<Reservation>? ReservationsPage { get; set; }

        public class Reservation
        {
            public Event? Event { get; set; }
            public bool IsChecked { get; set; }
        }
    }
}
