using Dominio.Repositorio;
using Repositorio.Contexto;
using SistemaVenda.Dominio.Entidades;

namespace Repositorio.Entidades
{
    public class RepositorioCliente : Repositorio<Cliente>, IRepositorioCliente
    {
        public RepositorioCliente(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
