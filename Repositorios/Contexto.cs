using Entidades;
using Microsoft.EntityFrameworkCore;
using System;

namespace Repositorios
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
        : base(options) { }
        public DbSet<Carros> Carros { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Marcas> Marcas { get; set; }
        public DbSet<Modelos> Modelos { get; set; }
        public DbSet<Operadores> Operadores { get; set; }
        public DbSet<Alugueis> Alugueis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }


}
