using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        public ApplicationDbContext mContext { get; set; }

        public RelatorioController(ApplicationDbContext context)
        {
            mContext = context;
        }

        public IActionResult Grafico()
        {
            var lista = (from x in mContext.VendaProdutos
                         group x by new { x.CodigoProduto, x.Produto.Descricao } into g
                         select new GraficoViewModel {
                         CodigoProduto = g.Key.CodigoProduto,
                         Descricao = g.Key.Descricao,
                         TotalVendido = g.Sum(x => x.Quantidade)}).ToList();
            

            string valores = string.Empty;
            string labels = string.Empty;
            string cores = string.Empty;

            var random = new Random();

            foreach (var item in lista)
            {                
                valores += item.TotalVendido.ToString() + ",";
                labels += $"'{item.Descricao}',";
                cores +=$"'{String.Format("#{0:X6}", random.Next(0x1000000))}',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;
            return View();
        }
    }
}
