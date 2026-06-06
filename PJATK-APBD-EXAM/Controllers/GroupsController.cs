using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJATK_APBD_EXAM.Data;
using PJATK_APBD_EXAM.DTOs;
using PJATK_APBD_EXAM.Entities;

namespace PJATK_APBD_EXAM.Controllers;

[Route("api/groups")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly AppDbContext _context;
    public GroupsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Group> groups = await _context.Groups.Include(g => g.Elements).ToListAsync();
        var result = groups.Select(g => new GroupGetDto
        {
            Id = g.Id,
            Name = g.Name,
            Budget = g.Budget,
            Description = g.Description,
            Elements = g.Elements.Select(e => new ElementGetDto
            {
                Id = e.Id,
                Name = e.Name,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt
            }).ToList()
        }).ToList();
        return Ok(result);
    }

    
}