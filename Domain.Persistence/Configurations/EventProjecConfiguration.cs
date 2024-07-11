using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Entities.DomainEntities;
using Domain.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Domain.Entities.Idempotence;

namespace Domain.Persistence.Configurations;
internal class EventProjecConfiguration : IEntityTypeConfiguration<EventProject>
{
    public void Configure(EntityTypeBuilder<EventProject> builder)
    {
        builder.ToTable(TableNames.EventProject);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EventId).HasMaxLength(200).IsRequired(true);
    }
}

