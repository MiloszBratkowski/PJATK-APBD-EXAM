using Microsoft.EntityFrameworkCore;
using PJATK_APBD_EXAM.Entities;

namespace PJATK_APBD_EXAM.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext() { }
    
    public AppDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<WashingMachine> WashingMachines { get; set; }
    public DbSet<AvailableProgram> AvailablePrograms { get; set; }
    public DbSet<Entities.Program> Program { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Entities.Program>().HasKey(p => p.ProgramId);
        modelBuilder.Entity<WashingMachine>().HasKey(p => p.WashingMachineId);
        modelBuilder.Entity<AvailableProgram>().HasKey(p => p.AvailableProgramId);
        modelBuilder.Entity<Customer>().HasKey(p => p.CustomerId);
        modelBuilder.Entity<PurchaseHistory>().HasKey(ph => new { ph.AvailableProgramId, ph.CustomerId });

        modelBuilder.Entity<AvailableProgram>()
            .HasOne(ap => ap.WashingMachine)
            .WithMany(ap => ap.AvailablePrograms)
            .HasForeignKey(ap => ap.WashingMachineId);
        
        modelBuilder.Entity<AvailableProgram>()
            .HasOne(ap => ap.Program)
            .WithMany(ap => ap.AvailablePrograms)
            .HasForeignKey(ap => ap.ProgramId);
        
        modelBuilder.Entity<PurchaseHistory>()
            .HasOne(ph => ph.AvailableProgram)
            .WithMany(ap => ap.PurchaseHistories)
            .HasForeignKey(ph => ph.AvailableProgramId);
        
        modelBuilder.Entity<PurchaseHistory>()
            .HasOne(ph => ph.Customer)
            .WithMany(ap => ap.PurchaseHistories)
            .HasForeignKey(ph => ph.CustomerId);

        
        modelBuilder.Entity<WashingMachine>(wm =>
        {
            wm.Property(e => e.MaxWeight)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            wm.Property(e => e.SerialNumber).
                HasMaxLength(100)
                .IsRequired();
        });
        modelBuilder.Entity<AvailableProgram>(wm =>
        {
            wm.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });
        modelBuilder.Entity<Entities.Program>(wm =>
        {
            wm.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
        });
        modelBuilder.Entity<Customer>(wm =>
        {
            wm.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            wm.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsRequired();
            wm.Property(e => e.PhoneNumber)
                .HasMaxLength(100);
        });
    }
}