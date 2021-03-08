using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoCategoria
    {
        IEnumerable<CategoriaViewModel> Buscar();
        IEnumerable<SelectListItem> BuscarSelectList();
        CategoriaViewModel Buscar(int codigoCategoria);
        void Criar(CategoriaViewModel categoriaVM);
        void Deletar(int id);
    }
}
