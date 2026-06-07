using Microsoft.EntityFrameworkCore;
using PJATK_APBD_EXAM.Data;
using PJATK_APBD_EXAM.DTOs;
using PJATK_APBD_EXAM.Entities;

namespace PJATK_APBD_EXAM.Services;

public class PurchaseService : IPurchaseService
{
    private readonly AppDbContext _dbContext;

    public PurchaseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<CustomersGetDto>> GetCustomerPurchase(int id)
    {
        var customer = await _dbContext.Customers
            .Include(c => c.PurchaseHistories)
            .ThenInclude(ph => ph.AvailableProgram).ThenInclude(wm => wm.WashingMachine)
            .Include(c => c.PurchaseHistories)
            .ThenInclude(ph => ph.AvailableProgram).ThenInclude(wm => wm.Program).
                FirstOrDefaultAsync(c => c.CustomerId == id);

        if (customer == null) return null;

        return null;
        /*
        return new CustomersGetDto
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Purchases = customer.PurchaseHistories.Select(ph => new PurchaseDto
            {
                Date = ph.PurchaseDate,
                Rating = ph.Rating,
                Price = ph.AvailableProgram.Price,
                WashingMachine = new WashingMachineDto
                {
                    Serial = ph.AvailableProgram.WashingMachine.SerialNumber,
                    MaxWeight = ph.AvailableProgram.WashingMachine.MaxWeight
                };
                
            }),
            
        };
        */
        throw new NotImplementedException();
    }
}