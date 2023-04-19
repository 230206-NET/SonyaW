using Microsoft.EntityFrameworkCore;
using Models;

public class FlashcardContext : DbContext {

    public FlashcardContext() :base() { }
    public FlashcardContext(DbContextOptions<FlashcardContext> options) : base(options) { }

    public virtual DbSet<Card> Flashcards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Card>(entity => {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Question)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("question");
            entity.Property(entity => entity.Answer)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("answer");
            entity.ToTable("Flashcard");
        });
    }


}
