using System.Windows.Input;
using BorderCrossing.Strings;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BorderCrossing.ViewModels
{
    public class HowViewModel : BaseViewModel
    {
        public HowViewModel()
        {
            Title = SharedResource.HowTitle;
        }

        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    }
}