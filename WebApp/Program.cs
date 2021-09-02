using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyApp.ApplicationLogic;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddOptions();
            //builder.Services.AddAuthorizationCore();
            //builder.Services.AddSingleton<AuthenticationStateProvider, CustomTokenAuthenticationStateProvider>();            
            //builder.Services.AddSingleton<ITokenRepository, TokenRepository>();            
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("WebAPI"));

            builder.Services.AddHttpClient(
                "WebAPI",
                client => client.BaseAddress = new Uri("https://localhost:44314/"))
                .AddHttpMessageHandler<AuthorizationMessageHandler>();

            builder.Services.AddTransient<AuthorizationMessageHandler>(sp =>
            {
                // Get required services from DI.
                var provider = sp.GetRequiredService<IAccessTokenProvider>();
                var naviManager = sp.GetRequiredService<NavigationManager>();

                // Create a new "AuthorizationMessageHandler" instance,
                // and return it after configuring it.
                var handler = new AuthorizationMessageHandler(provider, naviManager);
                handler.ConfigureHandler(authorizedUrls: new[] {
                  // List up URLs which to be attached access token.
                  "https://localhost:44314/"
                });
                return handler;
            });

            builder.Services.AddSingleton<IWebApiExecuter,WebApiExecuter>();



            //builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            //builder.Services.AddTransient<IAuthenticationUseCases, AuthenticationUseCases>();
            builder.Services.AddTransient<IProjectsScreenUseCases, ProjectsScreenUseCases>();
            builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
            builder.Services.AddTransient<ITicketRepository, TicketRepository>();
            builder.Services.AddTransient<ITicketsScreenUseCases, TicketsScreenUseCases>();
            builder.Services.AddTransient<ITicketScreenUseCases, TicketScreenUseCases>();

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);

                options.ProviderOptions.DefaultScopes.Add("webapi");                
            });

            await builder.Build().RunAsync();
        }
    }
}
