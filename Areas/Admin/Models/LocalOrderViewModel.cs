namespace RestaurantMVC.Areas.Admin.Models;

public class LocalOrderViewModel
{
    public int Id { get; set; }
    public int TableNo { get; set; }
    public DateTime OrderDate { get; set; }
    public List<LocalOrderItemViewModel> OrderItems { get; set; } = new();
}
