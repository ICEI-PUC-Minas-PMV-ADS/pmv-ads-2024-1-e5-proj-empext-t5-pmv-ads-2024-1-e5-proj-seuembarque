using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Puc.SeuEmbarque.Domain.ObjValor;
using Puc.SeuEmbarque.Infra.ApiData.Interface;
using Puc.SeuEmbarque.Infra.ApiData.Repository;
using Puc.SeuEmbarque.Services.Interface;
using Puc.SeuEmbarque.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.SeuEmbarque.Infra.DI.DependencyInjection
{
    public static class ResolveInjecao
    {
        public static IServiceCollection DepInjection(this IServiceCollection services, IConfiguration configuration)
        {

            #region Repositorio
            services.AddScoped<IAeroportoRepository, AeroportoRepository>();
            services.AddScoped<IPacoteRepository, PacoteRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            #endregion

            #region Services
            services.AddScoped<IAeroportoService, AeroportoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUserContractorResult, UserContractorResult>();
            services.AddScoped<IPacoteService, PacoteService>();
            services.AddScoped<IClienteService, ClienteService>();
            //services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            #endregion


            //HTTP CLIENT BASE ADRESS

            #region BaseAdress Aeroporto
            services.AddHttpClient("AeroportoApi", client =>
            {
                client.BaseAddress = new Uri("https://joseccosta.pythonanywhere.com/aeroportos/filtrar?city=");
            });
            #endregion

            #region BaseAdress Pacote
            services.AddHttpClient("PacotesApi", client =>
            {
                client.BaseAddress = new Uri("https://joseccosta.pythonanywhere.com");
            });
            #endregion

            #region BaseAdress Cliente
            services.AddHttpClient("ClientesApi", client =>
            {
                client.BaseAddress = new Uri("https://joseccosta.pythonanywhere.com/cliente");
            });
            #endregion

            #region BaseAdress Usuario
            services.AddHttpClient("UsuariosApi", client =>
            {
                client.BaseAddress = new Uri("https://joseccosta.pythonanywhere.com/usuario/admin");
            });
            #endregion


            return services;
        }
    }
}
