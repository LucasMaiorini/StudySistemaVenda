using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Dominio.Servicos;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoUsuario : IServicoAplicacaoUsuario
    {
        private readonly IServicoUsuario ServicoDominioUsuario;

        //construtor
        public ServicoAplicacaoUsuario(IServicoUsuario ServicoDominioUsuario)
        {
            this.ServicoDominioUsuario = ServicoDominioUsuario;
        }

        public UsuarioViewModel Buscar(int codigoUsuario)
        {

            var Usuario = ServicoDominioUsuario.Buscar(codigoUsuario);

            //mapeamento do tipo Entidade para o ViewModel.
            var UsuarioVM = new UsuarioViewModel()
            {
                Codigo = Usuario.Codigo,
                Email = Usuario.Email,
                Nome = Usuario.Nome,
                Senha = Usuario.Senha
            };
            return UsuarioVM;
        }

        public void Criar(UsuarioViewModel UsuarioVM)
        {
            var Usuario = new Usuario()
            {
                Codigo = UsuarioVM.Codigo,
                Email = UsuarioVM.Email,
                Nome = UsuarioVM.Nome,
                Senha = UsuarioVM.Senha
            };
            ServicoDominioUsuario.Criar(Usuario);
        }

        public void Deletar(int id)
        {
            ServicoDominioUsuario.Deletar(id);
        }

        public IEnumerable<UsuarioViewModel> Buscar()
        {
            var lista = ServicoDominioUsuario.Buscar();
            var listaUsuario = new List<UsuarioViewModel>();
            foreach (var item in lista)
            {
                var Usuario = new UsuarioViewModel()
                {
                    Codigo = item.Codigo,
                    Email = item.Email,
                    Nome = item.Nome,
                    Senha = item.Senha
                };
                listaUsuario.Add(Usuario);
            }
            return listaUsuario;
        }

        public bool ValidarLogin(string email, string senha)
        {
            return ServicoDominioUsuario.ValidarLogin(email, senha);
        }

        public Usuario DadosUsuario(string email, string senha)
        {
            return ServicoDominioUsuario.Buscar().Where(x => x.Email.ToUpper() == email.ToUpper() && x.Senha == senha).FirstOrDefault();
        }
    }
}
