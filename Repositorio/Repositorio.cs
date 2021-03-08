using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio
{
    public abstract class Repositorio<TEntidade> : DbContext, IRepositorio<TEntidade> where TEntidade : EntityBase, new()
    {
        protected DbContext db;
        protected DbSet<TEntidade> DbSetContext;

        public Repositorio(DbContext dbContext)
        {
            db = dbContext;
            DbSetContext = db.Set<TEntidade>();
        }

        public TEntidade Buscar(int id)
        {
            return DbSetContext.Where(x => x.Codigo == id).AsNoTracking().FirstOrDefault();
        }

        public virtual IEnumerable<TEntidade> Buscar()
        {
            return DbSetContext.AsNoTracking().ToList();
        }

        public void Criar(TEntidade Entidade)
        {
            if (Entidade.Codigo == null)
            {
                DbSetContext.Add(Entidade);
            }
            else
            {
                db.Entry(Entidade).State = EntityState.Modified;
            }
            db.SaveChanges();
        }

        public virtual void Deletar(int id)
        {
            var entidade = new TEntidade { Codigo = id };
            DbSetContext.Attach(entidade);
            DbSetContext.Remove(entidade);
            db.SaveChanges();
        }
    }
}
