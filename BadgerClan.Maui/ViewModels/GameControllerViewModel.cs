using BadgerClan.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    [ObservableProperty]
    private string newApiName;

    [ObservableProperty]
    private string newApiUrl;

    private Dictionary<string, string> ApiUrls { get; } = new Dictionary<string, string>
        {
            { "Local Dev", "http://localhost:5140" },
            { "Azure1", "https://badgerclan-api1-gtapbpbha4apgtew.westus2-01.azurewebsites.net" },
            { "Azure2", "https://badgerclan-api-2-d8b9dferdvhbbtdz.westus2-01.azurewebsites.net/" }
        };


    public ObservableCollection<ApiSelectionModel> ApiSelections { get; set; } = new ObservableCollection<ApiSelectionModel>();

    public Dictionary<string, string> ApiStrategies { get; } = new Dictionary<string, string>();

    public List<string> AvailableStrategies { get; } = new List<string>
    { "RunGun", "DoNothing", "CornerRetreat", "Ambush" };


    public GameControllerViewModel(IApiService apiService)
    {
        this.apiService = apiService;
        CurrentStrategy = AvailableStrategies[1];
        SelectedApi = ApiUrls.Keys.First();
        SetApiUrl(SelectedApi);

        foreach (var api in ApiUrls.Keys)
        {
            ApiSelections.Add(new ApiSelectionModel(api));
        }
    }



    //[RelayCommand]
    //private async Task SetStrategy(string strategyChoice)
    //{
    //    // if it's not already in the dictionary, add it with the strategy choice
    //    if (ApiStrategies.ContainsKey(SelectedApi))
    //    {
    //        ApiStrategies[SelectedApi] = strategyChoice;
    //    }
    //    else
    //    {
    //        ApiStrategies.Add(SelectedApi, strategyChoice);
    //    }

    //    await apiService.SetStrategyAsync(strategyChoice);
    //    CurrentStrategy = strategyChoice;

    //}

    [RelayCommand]
    private void SetApiUrl(string apiName)
    {
        Preferences.Set("CurrentApiUrl", ApiUrls[apiName]);
    }

    [RelayCommand]
    private void AddNewApi()
    {
        if(NewApiName is not null && NewApiUrl is not null)
        {
            ApiUrls[NewApiName] = NewApiUrl;
            ApiSelections.Add(new ApiSelectionModel(NewApiName));

            NewApiName = string.Empty;
            NewApiUrl = string.Empty;
        }
    }

    [RelayCommand]
    private async Task ApplyStrategyToApis()
    {
        // get all of the APIs that are checked (true)
        foreach (var api in ApiSelections.Where(api => api.IsSelected))
        {
            if (ApiUrls.TryGetValue(api.ApiName, out string apiUrl))
            {
                ApiStrategies[api.ApiName] = CurrentStrategy;

                await apiService.SetStrategyAsync(apiUrl, CurrentStrategy);
            }
        }
    }

    //[RelayCommand]
    //private void ToggleSelectAllApis()
    //{
    //    foreach (var api in ApiSelection.Keys.ToList())
    //    {
    //        ApiSelection[api] = SelectAllApis;
    //    }
    //}
}


public partial class ApiSelectionModel : ObservableObject
{
    public string ApiName { get; set; }

    [ObservableProperty]
    private bool isSelected;

    public ApiSelectionModel(string apiName)
    {
        ApiName = apiName;
    }
}

