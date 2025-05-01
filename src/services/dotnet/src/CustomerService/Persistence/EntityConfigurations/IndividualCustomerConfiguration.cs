using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class IndividualCustomerConfiguration : IEntityTypeConfiguration<IndividualCustomer>
{
    public void Configure(EntityTypeBuilder<IndividualCustomer> builder)
    {
        builder.ToTable("IndividualCustomers");
        builder.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(20);
        builder.Property(x => x.MiddleName).HasColumnName("MiddleName").IsRequired().HasMaxLength(20);
        builder.Property(x => x.Gender).HasColumnName("Gender").IsRequired().HasMaxLength(6);
        builder.Property(x => x.MotherName).HasColumnName("MotherName").IsRequired().HasMaxLength(20);
        builder.Property(x => x.FatherName).HasColumnName("FatherName").IsRequired().HasMaxLength(20);
        builder.Property(x => x.NationalityIdentity).HasColumnName("NationalityIdentity").IsRequired().HasMaxLength(11);
        builder.Property(x => x.BirthDate).HasColumnName("BirthDate").IsRequired();

        
    }
}
