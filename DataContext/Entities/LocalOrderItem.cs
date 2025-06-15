namespace RestaurantMVC.DataContext.Entities
{
    public class LocalOrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public LocalOrder? Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public int Count {  get; set; }
    }
}

