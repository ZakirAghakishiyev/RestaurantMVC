namespace RestaurantMVC.Areas.Admin.Models;

public class ReservationCreateViewModel
{
    public string GuestFirstName { get; set; }
    public string GuestLastName { get; set; }
    public string GuestPhoneNumber { get; set; }
    public int TableId { get; set; }
    public DateTime ReservationStartTime { get; set; }
}
