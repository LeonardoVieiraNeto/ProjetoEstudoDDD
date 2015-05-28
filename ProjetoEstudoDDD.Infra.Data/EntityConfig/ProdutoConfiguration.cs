using ProjetoEstudoDDD.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEstudoDDD.Infra.Data.EntityConfig
{
  public class ProdutoConfiguration : EntityTypeConfiguration<Produto>
  {
    public ProdutoConfiguration()
    {
      HasKey(p => p.ProdutoId);

      Property(p => p.Nome)
        .IsRequired()
        .HasMaxLength(100);

      Property(p => p.Valor)
        .IsRequired();

      HasRequired(p => p.Cliente)
        .WithMany()
        .HasForeignKey(p => p.ClienteId);
    }
  }
}
