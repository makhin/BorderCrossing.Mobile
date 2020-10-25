using System;
using System.IO;
using System.Threading.Tasks;
using BorderCrossing.Services;
using BorderCrossing.Strings;
using Plugin.FilePicker;
using Xamarin.Forms;

namespace BorderCrossing.Views
{
    public partial class UploadPage : ContentPage
    {
        public UploadPage()
        {
            InitializeComponent();
        }

        private async void PickFile_Clicked(object sender, EventArgs args)
        {
            var pickedFile = await CrossFilePicker.Current.PickFile(null);

            if (pickedFile != null)
            {
                if (!pickedFile.FileName.EndsWith("zip", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception(SharedResource.ZipWarning);
                }

                using (var fileStream = pickedFile.GetStream())
                {
                    var memoryStream = new MemoryStream();

                    await fileStream.CopyToAsync(memoryStream);

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var locationHistory = await BorderCrossingHelper.ExtractJsonAsync(memoryStream, (s, progressChangedEventArgs) =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                            LoadingProgressBar.ProgressTo(progressChangedEventArgs.ProgressPercentage / 100.0, 10,
                                Easing.Linear));
                    });
                }
            }
        }
    }
}