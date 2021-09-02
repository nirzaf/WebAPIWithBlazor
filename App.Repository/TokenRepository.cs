using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IJSRuntime iJSRuntime;

        public TokenRepository(IJSRuntime iJSRuntime)
        {
            this.iJSRuntime = iJSRuntime;
        }        

        public async Task SetToken(string token)
        {
            await iJSRuntime.InvokeVoidAsync("sessionStorage.setItem", "token", token);
        }

        public async Task<string> GetToken()
        {
            return await iJSRuntime.InvokeAsync<string>("sessionStorage.getItem", "token");
        }
    }
}
