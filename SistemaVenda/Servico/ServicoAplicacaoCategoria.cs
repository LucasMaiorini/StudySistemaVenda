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
    public class ServicoAplicacaoCategoria : IServicoAplicacaoCategoria
    {
        private readonly IServicoCategoria ServicoDominioCategoria;

        //construtor
        public ServicoAplicacaoCategoria(IServicoCategoria ServicoDominioCategoria)
        {
            this.ServicoDominioCategoria = ServicoDominioCategoria;
        }

        public CategoriaViewModel Buscar(int codigoCategoria)
        {

            var categoria = ServicoDominioCategoria.Buscar(codigoCategoria);

            //mapeamento do tipo Entidade para o ViewModel.
            var categoriaVM = new CategoriaViewModel()
            {
                Codigo = categoria.Codigo,
                Descricao = categoria.Descricao
            };
            return categoriaVM;
        }

        public void Criar(CategoriaViewModel categoriaVM)
        {
            var categoria = new Categoria()
            {
                Codigo = categoriaVM.Codigo,
                Descricao = categoriaVM.Descricao,
            };
            ServicoDominioCategoria.Criar(categoria);
        }

        public void Deletar(int id)
        {
            ServicoDominioCategoria.Deletar(id);
        }

        public IEnumerable<CategoriaViewModel> Buscar()
        {
            var lista = ServicoDominioCategoria.Buscar();
            var listaCategoria = new List<CategoriaViewModel>();
            foreach (var item in lista)
            {
                var categoria = new CategoriaViewModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                };
                listaCategoria.Add(categoria);
            }
            return listaCategoria;
        }

        public IEnumerable<SelectListItem> BuscarSelectList()
        {
            var lista = new List<SelectListItem>();
            var listaProvisoria = this.Buscar();
            foreach (var item in listaProvisoria)
            {
                var categoria = new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao,
                };
                lista.Add(categoria);
            }
            return lista;
        }
    }
}
