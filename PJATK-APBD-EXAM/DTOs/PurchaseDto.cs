namespace PJATK_APBD_EXAM.DTOs;

public class PurchaseDto
{
    public DateTime Date { get; set; }
    public int Rating {get; set;}
    public double Price {get; set;}
    public List<WashingMachineDto> WashingMachine { get; set; } = new();
    public List<ProgramDto> Program { get; set; } = new();
}