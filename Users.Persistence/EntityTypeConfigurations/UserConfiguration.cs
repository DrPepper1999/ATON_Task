using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain;

namespace Users.Persistence.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> bulder)
        {
            bulder.HasKey(user => user.Id);
            bulder.HasIndex(user => user.Id).IsUnique();
            bulder.HasIndex(user => user.Login).IsUnique();
            bulder.Property(user => user.Login).IsRequired();
        }
    }
}
