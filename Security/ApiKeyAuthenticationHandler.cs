using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using MottuNET.Security;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace MottuNET.Security
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ApiKeyHeaderName = "Authorization";
        private readonly IConfiguration _configuration;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
                return Task.FromResult(AuthenticateResult.Fail("API Key não encontrada no cabeçalho."));

            var validApiKey = _configuration.GetValue<string>("ApiKeySettings:ValidApiKey");

            if (string.IsNullOrEmpty(validApiKey))
                return Task.FromResult(AuthenticateResult.Fail("API Key não configurada no appsettings.json."));

            if (!validApiKey.Equals(extractedApiKey))
                return Task.FromResult(AuthenticateResult.Fail("API Key inválida."));

            var claims = new[] { new Claim(ClaimTypes.Name, "APIUser") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
