using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Dominio.Entidades;
using SistemaVenda.Models;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class ServicoAplicacaoCliente : IServicoAplicacaoCliente
    {
        private readonly IServicoCliente ServicoDominioCliente;

        //construtor
        public ServicoAplicacaoCliente(IServicoCliente ServicoDominioCliente)
        {
            this.ServicoDominioCliente = ServicoDominioCliente;
        }

        public ClienteViewModel Buscar(int codigoCliente)
        {
            var cliente = ServicoDominioCliente.Buscar(codigoCliente);

            //mapeamento do tipo Entidade para o ViewModel.
            var VM = new ClienteViewModel()
            {
                Codigo = cliente.Codigo,
                Nome = cliente.Nome,
                Celular = cliente.Celular,
                CNPJ_CPF = cliente.CNPJ_CPF,
                Email = cliente.Email,
            };
            return VM;
        }

        public void Criar(ClienteViewModel VM)
        {
            var cliente = new Cliente()
            {
                Codigo = VM.Codigo,
                Nome = VM.Nome,
                Celular = VM.Celular,
                CNPJ_CPF = VM.CNPJ_CPF,
                Email = VM.Email,
            };
            ServicoDominioCliente.Criar(cliente);
        }

        public void Deletar(int id)
        {
            ServicoDominioCliente.Deletar(id);
        }

        public IEnumerable<ClienteViewModel> Buscar()
        {
            var lista = ServicoDominioCliente.Buscar();
            var listaCliente = new List<ClienteViewModel>();
            foreach (var item in lista)
            {
                var cliente = new ClienteViewModel()
                {
                    Codigo = item.Codigo,
                    Nome = item.Nome,
                    Celular = item.Celular,
                    CNPJ_CPF = item.CNPJ_CPF,
                    Email = item.Email,
                };
                listaCliente.Add(cliente);
            }
            return listaCliente;
        }

        public IEnumerable<SelectListItem> BuscarSelectList()
        {
            var lista = new List<SelectListItem>();
            var listaProvisoria = this.Buscar();
            foreach (var item in listaProvisoria)
            {
                var cliente = new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome,
                };
                lista.Add(cliente);
            }
            return lista;
        }
    }
}
