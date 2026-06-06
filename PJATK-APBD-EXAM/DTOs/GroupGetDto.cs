using PJATK_APBD_EXAM.Entities;

namespace PJATK_APBD_EXAM.DTOs;

public class GroupGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Budget { get; set; }
    public string? Description { get; set; }
    public List<ElementGetDto> Elements { get; set; } = new();
}