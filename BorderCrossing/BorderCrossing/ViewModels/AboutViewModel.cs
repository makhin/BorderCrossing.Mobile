using System;
using System.Windows.Input;
using BorderCrossing.Strings;
using BorderCrossing.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BorderCrossing.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = SharedResource.HomeTitle;
        }

        public ICommand TapCommand => new Command(Execute);

        private static async void Execute()
        {
            await Shell.Current.GoToAsync(nameof(HowPage));
        }
    }
}