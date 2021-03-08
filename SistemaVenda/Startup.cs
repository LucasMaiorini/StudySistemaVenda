using Aplicacao.Servico;
using Aplicacao.Servico.Interfaces;
using Dominio.Interfaces;
using Dominio.Repositorio;
using Dominio.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositorio.Entidades;

namespace SistemaVenda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Definitiva
            services.AddDbContext<Repositorio.Contexto.ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MyStock")));

            //Nedded to work with session
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddControllersWithViews();

            //Serviço Aplicação
            services.AddScoped<IServicoAplicacaoCategoria, ServicoAplicacaoCategoria>();
            services.AddScoped<IServicoAplicacaoCliente, ServicoAplicacaoCliente>();
            services.AddScoped<IServicoAplicacaoProduto, ServicoAplicacaoProduto>();
            services.AddScoped<IServicoAplicacaoVenda, ServicoAplicacaoVenda>();
            services.AddScoped<IServicoAplicacaoUsuario, ServicoAplicacaoUsuario>();


            //Domínio
            services.AddScoped<IServicoCategoria, ServicoCategoria>();
            services.AddScoped<IServicoCliente, ServicoCliente>();
            services.AddScoped<IServicoProduto, ServicoProduto>();
            services.AddScoped<IServicoVenda, ServicoVenda>();
            services.AddScoped<IServicoUsuario, ServicoUsuario>();

            //Repositorio
            services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
            services.AddScoped<IRepositorioCliente, RepositorioCliente>();
            services.AddScoped<IRepositorioProduto, RepositorioProduto>();
            services.AddScoped<IRepositorioVenda, RepositorioVenda>();
            services.AddScoped<IRepositorioVendaProdutos, RepositorioVendaProdutos>();
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
