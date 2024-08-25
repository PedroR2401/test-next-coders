using Microsoft.EntityFrameworkCore;


public class gerenciadorDbContext : DbContext
{

    public DbSet<Tarefa> Tarefas { get; set; }

    public gerenciadorDbContext(DbContextOptions<gerenciadorDbContext> options)
            : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarefa>()
            .Property(t => t.Status)
            .HasConversion<string>();

    }
}
