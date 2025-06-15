namespace RestaurantMVC.Areas.Admin.Models;

public class LocalOrderItemViewModel
{
    public int Id { get; set; }
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; } = "";
    public int Count {  get; set; }
}
