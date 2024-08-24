using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasConversion(userId => userId!.Value, value => new UserId(value));


            builder.Property(u => u.Nombre)
                .HasMaxLength(200)
                .HasConversion( uName => uName!.Value, value => new Nombre(value));

            builder.Property(u => u.Apellido)
                .HasMaxLength(200)
                .HasConversion(uApellido => uApellido!.Value, value => new Apellido(value));

            builder.Property(u => u.Email)
                .HasMaxLength(400)
                .HasConversion(uApellido => uApellido!.Value, value => new Domain.Users.Email(value));

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(2000)
                .HasConversion(uPassword => uPassword!.Value, value => new PasswordHash(value));


            builder.HasIndex(u => u.Email)
                .IsUnique();

            
        }
    }
}
