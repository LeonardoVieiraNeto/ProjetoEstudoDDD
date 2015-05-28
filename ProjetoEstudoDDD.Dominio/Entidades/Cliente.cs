using System;
using System.Collections.Generic;

namespace ProjetoEstudoDDD.Dominio.Entidades
{
  public class Cliente
  {
    public int ClienteId { get; set; }
    public string Nome { get; set; }
    public string SobreNome { get; set; }
    public string Email { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Ativo { get; set; }

    public virtual IEnumerable<Produto> Produtos { get; set; }

    /// <summary>
    /// Um cliente é especial quando está ativo e possui mais de 5 anos de cadastro
    /// </summary>
    /// <param name="_cliente"></param>
    /// <returns></returns>
    public bool ClienteEspecial(Cliente _cliente)
    {
      return _cliente.Ativo && DateTime.Now.Year - _cliente.DataCadastro.Year >= 5;
    }
  }
}
