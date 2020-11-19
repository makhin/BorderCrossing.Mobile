using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using BorderCrossing.Controls;
using BorderCrossing.Models;
using BorderCrossing.Models.Google;
using BorderCrossing.Services;
using Xamarin.Forms;
using Region = BorderCrossing.Models.Region;

namespace BorderCrossing.ViewModels
{
    public class QueryPageModel : BaseViewModel
    {
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private EnumSelection<IntervalType> _interval;
        private List<Region> _regions;
        private decimal _processingProgress;

        public QueryPageModel()
        {
            Title = "Query";
            var request = BorderCrossingService.GetQueryRequestAsync().Result;
            StartDateTime = request.StartDate;
            EndDateTime = request.EndDate;
            Regions = request.Regions;
            Interval = new EnumSelection<IntervalType>(IntervalType.Every12Hours);

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

        public EnumSelection<IntervalType> Interval
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
            QueryRequest request = new QueryRequest()
            {
                StartDate = StartDateTime,
                EndDate = EndDateTime,
                IntervalType = Interval.Value,
                Regions = Regions
            };
            await BorderCrossingService.ParseLocationHistoryAsync(request, ParseCallback);
            await Shell.Current.GoToAsync("..");
        }

        private void ParseCallback(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            Device.BeginInvokeOnMainThread(() => ProcessingProgress = (decimal)(progressChangedEventArgs.ProgressPercentage / 100.0));
        }
    }
}
