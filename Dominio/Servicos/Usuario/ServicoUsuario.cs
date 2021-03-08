using Dominio.Interfaces;
using Dominio.Repositorio;
using SistemaVenda.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Dominio.Servicos
{
    public class ServicoUsuario : IServicoUsuario
    {
        readonly IRepositorioUsuario RepositorioUsuario;

        public ServicoUsuario(IRepositorioUsuario RepositorioUsuario)
        {
            this.RepositorioUsuario = RepositorioUsuario;
        }

        public Usuario Buscar(int id)
        {
            return RepositorioUsuario.Buscar(id);
        }

        public void Criar(Usuario Usuario)
        {
            RepositorioUsuario.Criar(Usuario);
        }

        public void Deletar(int id)
        {
            RepositorioUsuario.Deletar(id);
        }

        public IEnumerable<Usuario> Buscar()
        {
            return RepositorioUsuario.Buscar();
        }

        public bool ValidarLogin(string email, string senha)
        {
            return RepositorioUsuario.ValidarLogin(email, senha);
        }
    }
}
