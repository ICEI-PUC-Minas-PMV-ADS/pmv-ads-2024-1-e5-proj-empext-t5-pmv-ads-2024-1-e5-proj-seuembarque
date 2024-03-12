using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<ISkyscannerRepository, SkyscannerRepository>();

            #endregion

            #region Services
            services.AddScoped<IAeroportoService, AeroportoService>();
            #endregion

            #region HttpClient
            services.AddHttpClient("SkyScannerApi", client =>
            {
                client.BaseAddress = new Uri("https://partners.api.skyscanner.net/apiservices/v3/autosuggest/flights");
            });
            #endregion


            return services;
        }
    }
}
