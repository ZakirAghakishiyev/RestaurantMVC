namespace RestaurantMVC.Areas.Admin.Models;

public class LocalOrderUpdateViewModel
{
    public int Id { get; set; }
    public int TableNo { get; set; }
    public int? ReservationID { get; set; }
    public List<OrderItemInput> Items { get; set; } = new();
}

