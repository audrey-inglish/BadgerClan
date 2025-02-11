using BadgerClan.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BadgerClan.Maui.Views;

public partial class GrpcTest : ContentPage
{
	public GrpcTest(GrpcTestViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
}

public partial class GrpcTestViewModel(GrpcSchedulerClient client) : ObservableObject
{
    [ObservableProperty]
    private SetStrategyResponse response;

    [RelayCommand]
    public async Task SetStrategy()
    {
        Response = await client.Client.SetStrategy(new SetStrategyRequest
        {
            StrategyName = "RunGun"
        });


    }
}