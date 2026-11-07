using BullsAndCowsApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BullsAndCowsApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new MainPage());
	}
}