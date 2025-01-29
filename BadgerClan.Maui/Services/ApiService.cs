using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace BadgerClan.Maui.Services;

public class ApiService : IApiService
{
    // uses an HTTP client to communicate with my API
    private readonly HttpClient _httpClient;

    public ApiService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("GameControllerApi");
    }

    public Task SetStrategyAsync(string strategyChoice)
    {
        throw new NotImplementedException();
    }
}
