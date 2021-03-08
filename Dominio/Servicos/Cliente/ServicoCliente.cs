using Dominio.Interfaces;
using Dominio.Repositorio;
using SistemaVenda.Dominio.Entidades;
using System.Collections.Generic;

namespace Dominio.Servicos
{
    public class ServicoCliente : IServicoCliente
    {
        readonly IRepositorioCliente RepositorioCliente;

        public ServicoCliente(IRepositorioCliente RepositorioCliente)
        {
            this.RepositorioCliente = RepositorioCliente;
        }

        public IEnumerable<Cliente> Buscar()
        {
            return RepositorioCliente.Buscar();
        }

        public Cliente Buscar(int id)
        {
            return RepositorioCliente.Buscar(id);
        }

        public void Criar(Cliente cliente)
        {
            RepositorioCliente.Criar(cliente);
        }

        public void Deletar(int id)
        {
            RepositorioCliente.Deletar(id);
        }
    }
}
