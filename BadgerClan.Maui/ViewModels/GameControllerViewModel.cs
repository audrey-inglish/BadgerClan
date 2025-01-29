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

    public List<string> AvailableStrategies { get; } = new List<string>
    { "RunGunStrategy", "DoNothingStrategy" }; // should I use "Strategy" suffix?


    public GameControllerViewModel(IApiService apiService)
    {
        this.apiService = apiService;
        CurrentStrategy = AvailableStrategies[0];
    }


    [RelayCommand]
    private async Task SetStrategy(string strategyChoice)
    {
        await apiService.SetStrategyAsync(strategyChoice);

        // the UI will update automatically
        CurrentStrategy = strategyChoice;

    }
}
