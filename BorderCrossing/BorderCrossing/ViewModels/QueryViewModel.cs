using System;
using System.Collections.Generic;
using System.ComponentModel;
using BorderCrossing.Models;
using BorderCrossing.Strings;
using BorderCrossing.Views;
using Xamarin.Forms;
using Region = BorderCrossing.Models.Region;

namespace BorderCrossing.ViewModels
{
    public class QueryViewModel : BaseViewModel
    {
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private List<Region> _regions;
        private decimal _processingProgress;
        private IntervalType _interval;

        public QueryViewModel()
        {
            Title = "";
            var request = BorderCrossingService.GetQueryRequestAsync().Result;
            StartDateTime = request.StartDate;
            EndDateTime = request.EndDate;
            Regions = request.Regions;
            Interval = request.IntervalType;

            RunCommand = new Command(OnRun);
        }

        public DateTime StartDateTime
        {
            get => _startDateTime;
            set => SetProperty(ref _startDateTime, value);
        }

        public DateTime EndDateTime
        {
            get => _endDateTime;
            set => SetProperty(ref _endDateTime, value);
        }

        public IntervalType Interval
        {
            get => _interval;
            set => SetProperty(ref _interval, value);
        }

        public List<Region> Regions
        {
            get => _regions;
            set => SetProperty(ref _regions, value);
        }

        public decimal ProcessingProgress
        {
            get => _processingProgress;
            set => SetProperty(ref _processingProgress, value);
        }

        public Command RunCommand { get; }

        private async void OnRun()
        {
            var request = new QueryRequest()
            {
                StartDate = StartDateTime,
                EndDate = EndDateTime,
                IntervalType = Interval,
                Regions = Regions
            };
            await BorderCrossingService.ParseLocationHistoryAsync(request, ParseCallback);
            await Shell.Current.GoToAsync(nameof(ResultPage));
        }

        private void ParseCallback(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            Device.BeginInvokeOnMainThread(() => ProcessingProgress = (decimal)(progressChangedEventArgs.ProgressPercentage / 100.0));
        }
    }
}
