namespace PJATK_APBD_EXAM.Entities;

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