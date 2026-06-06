namespace PJATK_APBD_EXAM.DTOs;

public class AddElementDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int GroupId { get; set; }
}