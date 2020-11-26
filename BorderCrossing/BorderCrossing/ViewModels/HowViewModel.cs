using System.Windows.Input;
using BorderCrossing.Strings;
using BorderCrossing.Views;
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

        public ICommand TapCommand => new Command(Execute);

        private static async void Execute()
        {
            await Shell.Current.GoToAsync(nameof(UploadPage));
        }
    }
}