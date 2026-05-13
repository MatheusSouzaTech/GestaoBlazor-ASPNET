using Blazored.LocalStorage;
using ModelsLibrary.Models;
using System.Net.Http.Json;

namespace GestaoMobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _storage;
        public const string TokenKey = "erp_token";
        public const string UserKey = "erp_user";

        public AuthService(HttpClient http, ILocalStorageService storage)
        {
            _http = http;
            _storage = storage;
        }

        public async Task<(bool sucesso, string mensagem)> LoginAsync(LoginRequest request)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync("api/auth/login", request);
                if (resp.IsSuccessStatusCode)
                {
                    var result = await resp.Content.ReadFromJsonAsync<LoginResponse>();
                    if (result != null)
                    {
                        await _storage.SetItemAsStringAsync(TokenKey, result.Token);
                        await _storage.SetItemAsync(UserKey, result);
                        return (true, "Login realizado com sucesso.");
                    }
                }
                var erro = await resp.Content.ReadFromJsonAsync<RespostaApi>();
                return (false, erro?.Mensagem ?? "Login ou senha inválidos.");
            }
            catch
            {
                return (false, "Erro ao conectar com o servidor.");
            }
        }

        public async Task LogoutAsync()
        {
            await _storage.RemoveItemAsync(TokenKey);
            await _storage.RemoveItemAsync(UserKey);
        }

        public async Task<string?> GetTokenAsync() =>
            await _storage.GetItemAsStringAsync(TokenKey);

        public async Task<LoginResponse?> GetUsuarioLogadoAsync() =>
            await _storage.GetItemAsync<LoginResponse>(UserKey);

        public async Task<bool> EstaLogadoAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrWhiteSpace(token);
        }

        private record RespostaApi(string Mensagem);
    }
}
