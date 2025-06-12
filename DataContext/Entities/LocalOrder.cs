namespace RestaurantMVC.DataContext.Entities
{
    public class LocalOrder
    {
        public int Id { get; set; }
        public int TableNo { get; set; }
        public DateTime OrderDate { get; set; }
        public List<LocalOrderItem> OrderItems { get; set; } = [];
        public int? ReservationID {  get; set; }
        public Reservation? Reservation { get; set; }
    }
}
