using Microsoft.EntityFrameworkCore;
using SistemaVenda.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.DAL
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        //Constructor defined to accept the configuration setted in Startup.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<VendaProdutos> VendaProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<VendaProdutos>().HasKey(vp => new { vp.CodigoProduto, vp.CodigoVenda });
            builder.Entity<VendaProdutos>().HasOne(vp => vp.Venda).WithMany(vp => vp.Produtos).HasForeignKey(vp => vp.CodigoVenda);
            builder.Entity<VendaProdutos>().HasOne(vp => vp.Produto).WithMany(vp => vp.Vendas).HasForeignKey(vp => vp.CodigoProduto);
        }
    }
}
