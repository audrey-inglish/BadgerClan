﻿using BadgerClan.Maui.Services;
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
    private string currentApiUrl;

    public List<String> ApiUrls { get; } = new List<string>
    { "http://localhost:5140", "Azure1", "Azure2" };

    public List<string> AvailableStrategies { get; } = new List<string>
    { "RunGun", "DoNothing", "CornerRetreat", "Ambush" };


    public GameControllerViewModel(IApiService apiService)
    {
        this.apiService = apiService;
        CurrentStrategy = AvailableStrategies[0];
        currentApiUrl = Preferences.Get("CurrentApiUrl", "http://localhost:5140");
    }



    [RelayCommand]
    private async Task SetStrategy(string strategyChoice)
    {
        await apiService.SetStrategyAsync(strategyChoice);

        // the UI will update automatically
        CurrentStrategy = strategyChoice;

    }

    [RelayCommand]
    private async Task SetApiUrl(string apiUrl)
    {
        Preferences.Set("CurrentApiUrl", apiUrl);
    }
}
