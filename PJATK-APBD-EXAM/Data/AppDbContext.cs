using Microsoft.EntityFrameworkCore;
using PJATK_APBD_EXAM.Entities;

namespace PJATK_APBD_EXAM.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext() { }
    
    public AppDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Element> Elements { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<GroupSection> GroupSections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================================================
        // 1. KLUCZE GŁÓWNE
        // =========================================================
        modelBuilder.Entity<Element>().HasKey(e => e.Id);
        modelBuilder.Entity<Group>().HasKey(g => g.Id);
        modelBuilder.Entity<Section>().HasKey(s => s.Id);

        // Klucz złożony dla tabeli pośredniej (para GroupId + SectionId tworzy unikalny klucz)
        modelBuilder.Entity<GroupSection>()
            .HasKey(gs => new { gs.GroupId, gs.SectionId });

        // =========================================================
        // 2. RELACJA: Entity (M) <---> (1) Group
        // =========================================================
        modelBuilder.Entity<Element>()
            .HasOne(e => e.Group)          // Encja ma JEDNĄ grupę
            .WithMany(g => g.Elements)     // Grupa ma WIELE encji
            .HasForeignKey(e => e.GroupId) // Klucz obcy jest w tabeli Entity
            .OnDelete(DeleteBehavior.Cascade);

        // =========================================================
        // 3. RELACJA WIELE DO WIELE (Group <---> Section przez GroupSection)
        // =========================================================
    
        // Połączenie od strony grupy do tabeli pośredniej
        modelBuilder.Entity<GroupSection>()
            .HasOne(gs => gs.Group)
            .WithMany(g => g.GroupSections)
            .HasForeignKey(gs => gs.GroupId);

        // Połączenie od strony sekcji do tabeli pośredniej
        modelBuilder.Entity<GroupSection>()
            .HasOne(gs => gs.Section)
            .WithMany(s => s.GroupSections)
            .HasForeignKey(gs => gs.SectionId);
        
        // =========================================================
        // 4. Przykładowe pola
        // =========================================================
        
        modelBuilder.Entity<Element>(eb =>
        {
            // 1. Zmiana nazwy kolumny w bazie danych (jeśli ma być inna niż nazwa właściwości w C#)
            eb.Property(e => e.Name)
                .HasColumnName("EntityName")
                .IsRequired()
                .HasMaxLength(150);

            // 2. Wartość domyślna (Default Value) dla nowo dodawanych rekordów
            eb.Property(e => e.IsActive)
                .HasDefaultValue(true);

            // 3. Wartość domyślna generowana przez funkcję SQL (np. data utworzenia rekordu)
            eb.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Group>(eb =>
        {
            // 4. Konfiguracja typu decimal (KLUCZOWE dla cen, płatności, budżetów)
            // Bez tego EF Core zgłosi ostrzeżenie o domyślnym mapowaniu
            eb.Property(g => g.Budget)
                .HasColumnType("decimal(18,2)");

            // 5. Pole opcjonalne (Nullable) - jawne wskazanie
            eb.Property(g => g.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            // 6. Nałożenie unikalnego indeksu na kolumnę (Unique Constraint)
            // Zapobiega dublowaniu się np. nazw grup, adresów e-mail czy numerów PESEL
            eb.HasIndex(g => g.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Section>(eb =>
        {
            // 7. Mapowanie dokładnego typu danych SQL (np. char zamiast nvarchar)
            // Przydatne przy stałych szerokościach, np. PESEL char(11) lub kod pocztowy
            eb.Property(s => s.Code)
                .HasColumnType("char(5)")
                .IsRequired();
            
            // 8. Ignorowanie właściwości (Property zostanie w klasie C#, ale NIE stworzy kolumny w bazie)
            eb.Ignore(s => s.TempDisplayLabel);
        });
        
        
        modelBuilder.Entity<Group>().HasData(new List<Group>
        {
            new Group { Id = 1, Name = "Kardiologia", Budget = 50000m, Description = "Oddział sercowy" }
        });
        
        
        modelBuilder.Entity<Element>().HasData(new List<Element>()
        {
            new Element() { Id = 1, Name = "Entity1", IsActive =  true , GroupId = 1}, 
            new Element() { Id = 2, Name = "ASD", IsActive =  true, GroupId = 1},
        });
    }
}