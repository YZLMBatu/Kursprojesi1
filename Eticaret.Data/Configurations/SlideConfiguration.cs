using Eticaret.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Eticaret.Data.Configurations
{
    internal class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(250);
            builder.Property(x=> x.Description).HasMaxLength(500);
            builder.Property(x=> x.Image).HasMaxLength(200);
            builder.Property(x => x.Link).HasMaxLength(200);

        }
    }
}
