﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Entities.DomainEntities;
using Domain.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Action = Domain.Domain.Entities.Identity.Action;

namespace Domain.Persistence;
public class ApplicationDBContext: IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opt) : base(opt)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder) =>
    builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<AppUser> AppUses { get; set; }
    public DbSet<Action> Actions { get; set; }
    public DbSet<Function> Functions { get; set; }
    public DbSet<ActionInFunction> ActionInFunctions { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    public DbSet<Product> Products { get; set; }
}