using PJATK_APBD_EXAM.Entities;

namespace PJATK_APBD_EXAM.DTOs;

public class CustomersGetDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public List<PurchaseDto> Purchases { get; set; } = new();
}