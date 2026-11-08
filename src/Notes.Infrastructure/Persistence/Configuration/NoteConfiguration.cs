namespace Notes.Infrastructure.Persistence.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("notes", "public");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(n => n.Content)
            .IsRequired();

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(n => n.Tags)
            .WithMany();
    }
}
