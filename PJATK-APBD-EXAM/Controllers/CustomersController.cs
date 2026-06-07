using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJATK_APBD_EXAM.Data;
using PJATK_APBD_EXAM.DTOs;
using PJATK_APBD_EXAM.Entities;
using PJATK_APBD_EXAM.Services;

namespace PJATK_APBD_EXAM.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IPurchaseService  _purchaseService;
    
    private readonly AppDbContext _context;
    public CustomersController(AppDbContext context, IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
        _context = context;
    }

    [HttpGet("{id:int}/purchases")]
    public async Task<IActionResult> GetByIdAllPurchases(int id)
    {
        var customerDto = await _purchaseService.GetCustomerPurchase(id); 
        if (customerDto == null) return NotFound();
        return Ok(customerDto);
    }
    
}