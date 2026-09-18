using ApiKey.Shared;

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

        public async Task<PingResponseDto> PingAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PingResponseDto>("Test");

                //// Always use leading slash to avoid URL concatenation issues
                //var response = await _httpClient.GetAsync("/Test/Ping");

                //// Throw if not success (401/403 will be caught below)
                //response.EnsureSuccessStatusCode();

                //// Read JSON or text response
                //return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // Return clean error message for UI display
                return new PingResponseDto { Message = $"HTTP Error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new PingResponseDto { Message = $"Unexpected Error: {ex.Message}" };
            }
        }
    }
}

