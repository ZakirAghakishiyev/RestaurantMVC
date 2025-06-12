namespace RestaurantMVC.DataContext.Entities
{
    public class OnlineOrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OnlineOrder? Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
