using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        protected ApplicationDbContext mContext;
        protected IHttpContextAccessor HttpContextAccesssor;

        Criptografar cripto = new Criptografar();
        public LoginController(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            this.mContext = context;
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
                var obj = new Usuario()
                {
                    Codigo = viewModel.Codigo,
                    Email = viewModel.Email.Trim(),
                    Nome = viewModel.Nome.Trim(),
                    Senha = cripto.Encrypt(viewModel.Senha.Trim())
                };
                if (viewModel.Codigo == null)
                {
                    mContext.Usuario.Add(obj);
                }
                else
                {
                    mContext.Entry(obj).State = EntityState.Modified;
                }
                mContext.SaveChanges();
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

                var usuario = mContext.Usuario.Where(x => x.Email == viewModel.Email && x.Senha == cripto.Encrypt(viewModel.Senha.Trim()))
                    .FirstOrDefault();
                if (usuario == null)
                {
                    ViewData["ErroLogin"] = "E-mail ou senha inválido";
                    //return Ok(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
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
