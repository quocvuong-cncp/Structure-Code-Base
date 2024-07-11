using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Entities.DomainEntities;
using Domain.Domain.Entities.Idempotence;
using Domain.Domain.Entities.Identity;
using Domain.Domain.Entities.Outbox;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Action = Domain.Domain.Entities.Identity.Action;

namespace Domain.Persistence;
public class ApplicationDBContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opt) : base(opt)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder) =>
    builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<AppUser> AppUses { get; set; }
    public DbSet<Action> Actions { get; set; }
    public DbSet<Function> Functions { get; set; }
    public DbSet<ActionInFunction> ActionInFunctions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OutboxMessage> Outboxs { get; set; }
    public DbSet<EventProject> EventProjects {get; set;}
}
