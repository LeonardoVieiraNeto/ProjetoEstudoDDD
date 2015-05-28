using ProjetoEstudoDDD.Dominio.Entidades;
using ProjetoEstudoDDD.Infra.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEstudoDDD.Infra.Data.Contexto
{
  public class PrjetoEstudoDDDContexto : DbContext 
  {
    public PrjetoEstudoDDDContexto()
      : base("ProjetoEstudoDDD")
    {

    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
      modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

      //Configura para o Entity considere que todas as propriedades com id no fim sejam consideradas a chave da tabela
      modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey());

      //Configura para que o Entity crie todas as string como varchar de 100
      modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
      modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

      //Configura para que o Entity sete sempre a os campos decimal com tamanho 15,2
      modelBuilder.Properties<decimal>().Configure(p => p.HasPrecision(15, 2));

      modelBuilder.Configurations.Add(new ClienteConfiguration());
      modelBuilder.Configurations.Add(new ProdutoConfiguration());

      base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {

      //Para todos os registros adicionados preencher a DataCadastro
      foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
      {
        if (entry.State == EntityState.Added)
        {
          entry.Property("DataCadastro").CurrentValue = DateTime.Now;
        }

        if(entry.State == EntityState.Modified)
        {
          entry.Property("DataCadastro").IsModified = false;
        }
      }

      return base.SaveChanges();
    }
  }
}
