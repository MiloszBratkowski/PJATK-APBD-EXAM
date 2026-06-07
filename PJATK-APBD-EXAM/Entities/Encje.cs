namespace PJATK_APBD_EXAM.Entities;
/*
public class Element
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Klucz obcy i właściwość nawigacyjna do Group (1:M)
    public int GroupId { get; set; }
    public virtual Group Group { get; set; } = null!;
}

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Budget { get; set; }
    
    public string? Description { get; set; }

    // Relacja 1:M (Grupa ma wiele elmentów)
    public virtual ICollection<Element> Elements { get; set; } = new HashSet<Element>();

    // Relacja M:M (Grupa pojawia się wiele razy w tabeli pośredniej)
    public virtual ICollection<GroupSection> GroupSections { get; set; } = new HashSet<GroupSection>();
}

public class Section
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public char Code { get; set; }
    public string TempDisplayLabel { get; set; }

    // Relacja M:M (Sekcja pojawia się wiele razy w tabeli pośredniej)
    public virtual ICollection<GroupSection> GroupSections { get; set; } = new HashSet<GroupSection>();
}

// Tabela pośrednicząca dla relacji wiele-do-wielu
public class GroupSection
{
    public int GroupId { get; set; }
    public virtual Group Group { get; set; } = null!;

    public int SectionId { get; set; }
    public virtual Section Section { get; set; } = null!;
}
*/
public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new HashSet<PurchaseHistory>();
}

public class PurchaseHistory
{
    public int AvailableProgramId { get; set; }
    public virtual AvailableProgram AvailableProgram { get; set; } = null!;
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    public DateTime PurchaseDate { get; set; }
    public int Rating { get; set; }
}

public class AvailableProgram
{
    public int AvailableProgramId { get; set; }
    public int WashingMachineId { get; set; }
    public virtual WashingMachine WashingMachine { get; set; } = null!;
    
    public int ProgramId { get; set; }
    public virtual Program Program { get; set; } = null!;
    
    public double Price { get; set; }
    
    public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new HashSet<PurchaseHistory>();
}

public class WashingMachine
{
    public int WashingMachineId { get; set; }
    
    public double MaxWeight { get; set; }
    public string SerialNumber { get; set; } = null!;
    public virtual ICollection<AvailableProgram> AvailablePrograms { get; set; } = new HashSet<AvailableProgram>();
}

public class Program
{
    public int ProgramId { get; set; }
    public string Name { get; set; } = null!;
    public int DurationMinutes { get; set; }
    public int TemperatureCelsius { get; set; }
    public virtual ICollection<AvailableProgram> AvailablePrograms { get; set; } = new HashSet<AvailableProgram>();
}