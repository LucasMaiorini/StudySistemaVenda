using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico.Interfaces
{
    public interface IServicoAplicacaoVenda
    {
        IEnumerable<VendaViewModel> Buscar();
        VendaViewModel Buscar(int codigoVenda);
        void Criar(VendaViewModel VendaVM);
        void Deletar(int id);
        IEnumerable<GraficoViewModel> ListaGrafico();
    }
}
