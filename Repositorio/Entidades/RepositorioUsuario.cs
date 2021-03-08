using Dominio.Repositorio;
using Repositorio.Contexto;
using SistemaVenda.Dominio.Entidades;
using System.Linq;

namespace Repositorio.Entidades
{
    public class RepositorioUsuario : Repositorio<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(ApplicationDbContext dbContext) : base(dbContext) { }

        public bool ValidarLogin(string email, string senha)
        {
            var usuario = DbSetContext
                .Where(x => x.Email == email && x.Senha == senha)
                    .FirstOrDefault();
            return (usuario == null) ? false : true;
        }
    }
}
