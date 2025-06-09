namespace RestaurantMVC.DataContext.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public DateTime ReservationTime { get; set; }
        //public int OrderId {  get; set; }
        //public Order Order {  get; set; }
    } 
}
