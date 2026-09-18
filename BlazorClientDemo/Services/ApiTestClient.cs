using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace BlazorClientDemo.Services
{
    public class ApiTestClient
    {
        //GET http://localhost:7001/api/test/ping
        //Header: X - API - KEY: SuperSecretApiKey-12345
        private readonly HttpClient _httpClient;
        public ApiTestClient(HttpClient http)
        {
            _httpClient = http;
            // Attach API Key header for every request
            if (!_httpClient.DefaultRequestHeaders.Contains("X-API-KEY"))
            {
                _httpClient.DefaultRequestHeaders.Add("X-API-KEY", "SuperSecretApiKey-12345");
            }
        }

        // Ping API
        public async Task<string> PingAsync()
        {
            try
            {
                // Always use leading slash to avoid URL concatenation issues
                var response = await _httpClient.GetAsync("/Test/Ping");

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

