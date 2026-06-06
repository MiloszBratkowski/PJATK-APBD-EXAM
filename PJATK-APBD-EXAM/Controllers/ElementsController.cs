using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJATK_APBD_EXAM.Data;
using PJATK_APBD_EXAM.DTOs;
using PJATK_APBD_EXAM.Entities;

namespace PJATK_APBD_EXAM.Controllers;

[Route("api/elements")]
[ApiController]
public class ElementsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ElementsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Element> elements = await _context.Elements.ToListAsync();
        return Ok(elements);

    }
    
    /*
     *
{
    "Id": 3,
    "Name": "LOLOL",
    "IsActive": true,
    "CreatedAt": "2026-06-06T23:46:28.23",
    "GroupId": 1
}
     */
    [HttpPost]
    public async Task<IActionResult> addElement([FromBody] AddElementDto dto)
    {
        var elementExists = await _context.Elements.AnyAsync(e => e.Id == dto.Id);
        if (elementExists)
        {
            return NotFound("Istnieje taki element");
        }

        var assign = new Element()
        {
            Name = dto.Name,
            IsActive = dto.IsActive,
            CreatedAt = dto.CreatedAt,
            GroupId = dto.GroupId,
            Group = _context.Groups.First(g => g.Id == dto.GroupId),
        };
        _context.Elements.Add(assign);
        await  _context.SaveChangesAsync();
        var resultDto = new ElementGetDto
        {
            Id = assign.Id,
            Name = assign.Name,
            IsActive = assign.IsActive,
            CreatedAt = assign.CreatedAt
        };
        return Created("/api/elements/", resultDto);
        //return Created("", new { Message = $"Pomyślnie przypisano pacjenta do łóżka o ID {availableBed.Id} w pokoju {availableBed.RoomId}." });

    }
}