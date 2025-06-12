namespace RestaurantMVC.DataContext.Entities
{
    public class OnlineOrder
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OnlineOrderItem> OrderItems { get; set; } = [];
    }
}
