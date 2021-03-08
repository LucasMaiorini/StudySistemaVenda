using Aplicacao.Servico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Helpers;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        readonly IServicoAplicacaoUsuario ServicoAplicacaoUsuario;
        protected IHttpContextAccessor HttpContextAccesssor;

        Criptografar cripto = new Criptografar();

        public LoginController(IServicoAplicacaoUsuario ServicoAplicacaoUsuario, IHttpContextAccessor httpContext)
        {
            this.ServicoAplicacaoUsuario = ServicoAplicacaoUsuario;
            this.HttpContextAccesssor = httpContext;
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                if (id == 0)
                {
                    HttpContextAccesssor.HttpContext.Session.Clear();
                }
            }
            UsuarioViewModel viewModel = new UsuarioViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(UsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {                
                if (viewModel.Codigo == null)
                {
                    viewModel.Senha = cripto.Encrypt( viewModel.Senha);
                    ServicoAplicacaoUsuario.Criar(viewModel);
                }                
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult ValidaLogin(UsuarioViewModel viewModel)
        {
            //Verifica se o formulário está preenchido de forma correta
            if (ModelState.IsValid)
            {
                bool login = ServicoAplicacaoUsuario.ValidarLogin(viewModel.Email, cripto.Encrypt(viewModel.Senha.Trim()));               
                if (!login)
                {
                    ViewData["ErroLogin"] = "E-mail ou senha inválido";
                    //return Ok(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    var usuario = ServicoAplicacaoUsuario.DadosUsuario(viewModel.Email, cripto.Encrypt(viewModel.Senha.Trim()));
                    HttpContextAccesssor.HttpContext.Session.SetString(Sessao.NOME_USUARIO, usuario.Nome);
                    HttpContextAccesssor.HttpContext.Session.SetString(Sessao.EMAIL_USUARIO, usuario.Email);
                    HttpContextAccesssor.HttpContext.Session.SetInt32(Sessao.CODIGO_USUARIO, (int)usuario.Codigo);
                    HttpContextAccesssor.HttpContext.Session.SetInt32(Sessao.LOGADO, 1);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
