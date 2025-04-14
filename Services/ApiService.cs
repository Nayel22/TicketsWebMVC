namespace TicketsWebMVC.Services
{
    // Services/ApiService.cs
    public class ApiService<T> : IApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService<T>> _logger;

        public ApiService(IHttpClientFactory httpClientFactory, ILogger<ApiService<T>> logger)
        {
            _httpClient = httpClientFactory.CreateClient("TicketsAPI");
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<T>>(endpoint);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos de la API");
                throw;
            }
        }

        public async Task<T> GetByIdAsync(string endpoint, int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<T>($"{endpoint}/{id}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos de la API");
                throw;
            }
        }

        public async Task<T> CreateAsync(string endpoint, T item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear en la API");
                throw;
            }
        }

        public async Task UpdateAsync(string endpoint, int id, T item)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{endpoint}/{id}", item);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar en la API");
                throw;
            }
        }

        public async Task DeleteAsync(string endpoint, int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar en la API");
                throw;
            }
        }
    }
}
