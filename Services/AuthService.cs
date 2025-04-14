using TicketsWebMVC.Models;

namespace TicketsWebMVC.Services
{

    public interface IAuthService
    {
        Task<UsuarioModel> LoginAsync(string email, string password);
        Task LogoutAsync();
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("TicketsAPI");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UsuarioModel> LoginAsync(string email, string password)
        {
            try
            {
                // Buscar usuarios desde la API
                var usuarios = await _httpClient.GetFromJsonAsync<IEnumerable<UsuarioModel>>("api/Usuarios");

                // Filtrar el usuario correcto
                var usuario = usuarios.FirstOrDefault(u =>
                    u.us_correo.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                    u.us_clave == password &&
                    u.us_estado == "A"); // Asumiendo que A es activo

                if (usuario != null)
                {
                    // Guardar en sesión
                    var session = _httpContextAccessor.HttpContext.Session;
                    session.SetInt32("UserId", usuario.us_identificador);
                    session.SetString("UserName", usuario.us_nombre_completo);
                    session.SetInt32("RoleId", usuario.us_ro_identificador);

                    return usuario;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public Task LogoutAsync()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return Task.CompletedTask;
        }
    }
}
