namespace Notes.Infrastructure.Persistence.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags", "public");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
