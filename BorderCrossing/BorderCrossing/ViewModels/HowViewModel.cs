using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BorderCrossing.ViewModels
{
    public class HowViewModel : BaseViewModel
    {
        public HowViewModel()
        {
            Title = "How?";
        }

        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    }
}