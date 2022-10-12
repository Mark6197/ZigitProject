using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class PersonalDetailsConfiguration : IEntityTypeConfiguration<PersonalDetails>
    {
        public void Configure(EntityTypeBuilder<PersonalDetails> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.Property(p => p.UserId).ValueGeneratedNever();
            builder.Property(p => p.Team).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Avatar).IsRequired().HasMaxLength(1024);
        }
    }
}
