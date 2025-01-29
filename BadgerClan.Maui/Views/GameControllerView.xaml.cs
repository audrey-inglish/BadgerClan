using BadgerClan.Maui.ViewModels;

namespace BadgerClan.Maui.Views;

public partial class GameControllerView : ContentPage
{
	public GameControllerView(GameControllerViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
}