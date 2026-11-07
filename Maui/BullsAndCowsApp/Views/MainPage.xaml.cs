using BullsAndCowsApp.ViewModels;

namespace BullsAndCowsApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // The page creates its own ViewModel (no dependency injection)
            BindingContext = new GameViewModel();
        }
    }
}
