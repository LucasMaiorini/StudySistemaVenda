using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoUsuario
    {
        IEnumerable<UsuarioViewModel> Buscar();
        UsuarioViewModel Buscar(int codigoUsuario);
        void Criar(UsuarioViewModel UsuarioVM);
        void Deletar(int id);
        bool ValidarLogin(string email, string senha);
        Usuario DadosUsuario(string email, string senha);
    }
}
