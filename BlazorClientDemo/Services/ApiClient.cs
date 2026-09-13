using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace BlazorClientDemo.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(HttpClient http)
        {
            _http = http;
            // Attach API Key header for every request
            if (!_http.DefaultRequestHeaders.Contains("X-API-KEY"))
            {
                _http.DefaultRequestHeaders.Add("X-API-KEY", "SuperSecretKey123");
            }

            //GET http://localhost:5000/api/test/ping
            //Header: X - API - KEY: SuperSecretKey123
        }

        // Ping API
        public async Task<string> PingAsync()
        {
            try
            {
                // Always use leading slash to avoid URL concatenation issues
                var response = await _http.GetAsync("/api/test/ping");

                // Throw if not success (401/403 will be caught below)
                response.EnsureSuccessStatusCode();

                // Read JSON or text response
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // Return clean error message for UI display
                return $"HTTP Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Unexpected Error: {ex.Message}";
            }
        }
    }
}

