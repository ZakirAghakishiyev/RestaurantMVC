using RestaurantMVC.Areas.Admin.Models;

public class ReservationDetailsViewModel
{
    public int Id { get; set; }
    public string GuestFirstName { get; set; } = "";
    public string GuestLastName { get; set; } = "";
    public string GuestPhoneNumber { get; set; } = "";
    public int TableNo { get; set; }
    public DateTime ReservationStartTime { get; set; }
    public DateTime? ReservationEndTime { get; set; }

    public List<LocalOrderViewModel> LocalOrders { get; set; } = new();
}
