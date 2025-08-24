using StudentApp.Services.JwtService;

namespace StudentApp.Services.AuthenicationManager
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<JwtAuthorizationMessageHandler> _logger;

        public JwtAuthorizationMessageHandler(IJwtTokenService jwtTokenService, ILogger<JwtAuthorizationMessageHandler> logger)
        {
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _jwtTokenService.GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to add authorization header");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
