using Blazored.LocalStorage;
using MiApiTP.Web.Auth;
using MiApiTP.Web.DTOs;
using MiApiTP.Web.Servicios.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MiApiTP.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<bool> LoginAsync(LoginDTO loginDTO)
        {

            var response = await _http.PostAsJsonAsync("api/Usuario/Login", loginDTO);

            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResultDTO>();

            if (result == null || string.IsNullOrEmpty(result.Token)) return false;


            await _localStorage.SetItemAsync("authToken", result.Token);

            ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);

            return true;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
        }
    }
}