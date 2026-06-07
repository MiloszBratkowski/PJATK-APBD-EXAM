using PJATK_APBD_EXAM.DTOs;

namespace PJATK_APBD_EXAM.Services;

public interface IPurchaseService
{
    Task<IEnumerable<CustomersGetDto>> GetCustomerPurchase(int id);
}