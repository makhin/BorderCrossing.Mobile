using System;
using System.Windows.Input;
using BorderCrossing.Strings;
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
    }
}