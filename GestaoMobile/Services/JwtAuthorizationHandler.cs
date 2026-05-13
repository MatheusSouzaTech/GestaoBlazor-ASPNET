using Blazored.LocalStorage;

namespace GestaoMobile.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _storage;

        public JwtAuthorizationHandler(ILocalStorageService storage)
        {
            _storage = storage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _storage.GetItemAsStringAsync(AuthService.TokenKey);
            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
