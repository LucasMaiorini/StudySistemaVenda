using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoCliente
    {
        IEnumerable<ClienteViewModel> Buscar();
        IEnumerable<SelectListItem> BuscarSelectList();
        ClienteViewModel Buscar(int codigoCliente);
        void Criar(ClienteViewModel clienteVM);
        void Deletar(int id);
    }
}
