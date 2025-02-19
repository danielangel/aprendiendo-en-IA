using System;
using System.Collections.Generic;
using FactoringAI.Model;
using Microsoft.EntityFrameworkCore;

namespace FactoringAI.Data;

public partial class FactoringContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }

    public FactoringContext(DbContextOptions<FactoringContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar la clave primaria manualmente
        modelBuilder.Entity<Cliente>()
            .HasKey(c => c.Id); // Aquí defines que `Id` es la clave primaria
    }




}
