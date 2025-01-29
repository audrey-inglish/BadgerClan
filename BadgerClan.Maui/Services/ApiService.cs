using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
//using Java.Util.Logging;

namespace BadgerClan.Maui.Services;

public class ApiService : IApiService
{
    // uses an HTTP client to communicate with my API
    private readonly HttpClient _httpClient;

    public ApiService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("GameControllerApi");
    }

    public async Task SetStrategyAsync(string strategyChoice)
    {
        var url = $"/setstrategy/{strategyChoice}";
        var response = await _httpClient.GetAsync(url);

        try
        {
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Strategy successfully set!");
            }
            else
            {
                Console.WriteLine("Failed to set strategy.");
                throw new Exception("Failed to set strategy: " + response.ReasonPhrase);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to set strategy: " + ex.Message);
        }
    }
}
