using Dominio.Interfaces;
using Dominio.Repositorio;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Dominio.Servicos
{
    public class ServicoCategoria : IServicoCategoria
    {
        readonly IRepositorioCategoria RepositorioCategoria;

        public ServicoCategoria(IRepositorioCategoria RepositorioCategoria)
        {
            this.RepositorioCategoria = RepositorioCategoria;
        }

        public Categoria Buscar(int id)
        {
            return RepositorioCategoria.Buscar(id);
        }

        public void Criar(Categoria categoria)
        {
            RepositorioCategoria.Criar(categoria);
        }

        public void Deletar(int id)
        {
            RepositorioCategoria.Deletar(id);
        }

        public IEnumerable<Categoria> Buscar()
        {
            return RepositorioCategoria.Buscar();
        }
    }
}
