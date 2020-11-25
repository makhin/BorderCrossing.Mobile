using System;
using System.Collections.ObjectModel;
using System.IO;
using BorderCrossing.Services;
using BorderCrossing.Strings;
using BorderCrossing.Views;
using Plugin.FilePicker;
using Xamarin.Forms;

namespace BorderCrossing.ViewModels
{
    public class UploadViewModel : BaseViewModel
    {
        private decimal _uploadProgress;

        public Command PickFileCommand { get; }

        public UploadViewModel()
        {
            Title = SharedResource.UploadP3;
            PickFileCommand = new Command(OnPickFile);
        }

        public decimal UploadProgress
        {
            get => _uploadProgress;

            set => SetProperty(ref _uploadProgress, value);
        }

        private async void OnPickFile(object obj)
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
                        Device.BeginInvokeOnMainThread(() => UploadProgress = (decimal)(progressChangedEventArgs.ProgressPercentage / 100.0));
                    });

                    BorderCrossingService.LocationHistory = locationHistory;
                    await Shell.Current.GoToAsync(nameof(QueryPage));
                }
            }
        }
    }
}