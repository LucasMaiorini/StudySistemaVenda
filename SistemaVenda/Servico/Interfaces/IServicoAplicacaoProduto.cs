using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoProduto
    {
        IEnumerable<ProdutoViewModel> Buscar();
        IEnumerable<SelectListItem> BuscarSelectList();

        ProdutoViewModel Buscar(int codigoProduto);
        void Criar(ProdutoViewModel produtoVM);
        void Deletar(int id);
    }
}
