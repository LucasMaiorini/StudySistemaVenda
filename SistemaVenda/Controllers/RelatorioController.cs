using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Models;
using System;
using System.Linq;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        readonly IServicoAplicacaoVenda ServicoAplicacaoVenda;

        public RelatorioController(IServicoAplicacaoVenda ServicoAplicacaoVenda)
        {
            this.ServicoAplicacaoVenda = ServicoAplicacaoVenda;
        }


        public IActionResult Grafico()
        {
            var lista = this.ServicoAplicacaoVenda.ListaGrafico().ToList();

            string valores = string.Empty;
            string labels = string.Empty;
            string cores = string.Empty;

            var random = new Random();

            foreach (var item in lista)
            {
                valores += item.TotalVendido.ToString() + ",";
                labels += $"'{item.Descricao}',";
                cores += $"'{String.Format("#{0:X6}", random.Next(0x1000000))}',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;
            return View();
        }
    }
}
