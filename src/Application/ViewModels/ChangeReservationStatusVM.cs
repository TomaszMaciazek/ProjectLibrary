using System;

namespace Application.ViewModels
{
    public class ChangeReservationStatusVM
    {
        public int ReservationId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModyficationDate { get; set; }
    }
}
