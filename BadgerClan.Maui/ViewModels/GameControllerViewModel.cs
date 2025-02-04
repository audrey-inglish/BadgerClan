using BadgerClan.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Maui.ViewModels;

public partial class GameControllerViewModel : ObservableObject
{
    private readonly IApiService apiService;

    [ObservableProperty]
    private string currentStrategy;

    [ObservableProperty]
    private string selectedApi;

    public List<String> ApiNames { get; } = new List<string>
    { "Local Dev", "Azure1", "Azure2" };

    public List<string> AvailableStrategies { get; } = new List<string>
    { "RunGun", "DoNothing", "CornerRetreat", "Ambush" };


    public GameControllerViewModel(IApiService apiService)
    {
        this.apiService = apiService;
        CurrentStrategy = AvailableStrategies[1];
        SelectedApi = ApiNames[0];
        SetApiUrl(SelectedApi);
    }



    [RelayCommand]
    private async Task SetStrategy(string strategyChoice)
    {
        await apiService.SetStrategyAsync(strategyChoice);

        // the UI will update automatically
        CurrentStrategy = strategyChoice;

    }

    [RelayCommand]
    private void SetApiUrl(string apiName)
    {
        switch (apiName)
        {
            case "Local Dev":
                apiName = "http://localhost:5140";
                break;
            case "Azure1":
                apiName = "https://badgerclan-api1-gtapbpbha4apgtew.westus2-01.azurewebsites.net";
                break;
            case "Azure2":
                apiName = "https://badgerclan-api-2-d8b9dferdvhbbtdz.westus2-01.azurewebsites.net/";
                break;
        }
        Preferences.Set("CurrentApiUrl", apiName);
    }
}
