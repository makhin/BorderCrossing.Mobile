using System;
using System.Linq;
using System.Windows.Input;
using BorderCrossing.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BorderCrossing.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        public ResultViewModel()
        {
            Title = "";
            var checkPoints = BorderCrossingService.CheckPoints;

            OpenMapCommand = new Command<CheckPoint>(OpenMap);

            if (checkPoints == null || !checkPoints.Any())
            {
                return;
            }

            Response = new BorderCrossingResponse();
            var arrivalPoint = checkPoints.First();
            Response.Periods.Add(new Period
            {
                ArrivalPoint = arrivalPoint,
                Country = arrivalPoint.CountryName
            });
            var last = Response.Periods.Last();

            foreach (var checkPoint in checkPoints.Skip(1).Take(checkPoints.Count - 2))
            {
                last.DeparturePoint = checkPoint;
                Response.Periods.Add(new Period
                {
                    ArrivalPoint = checkPoint,
                    Country = checkPoint.CountryName,
                });
                last = Response.Periods.Last();
            }

            last.DeparturePoint = checkPoints.Last();
        }

        public BorderCrossingResponse Response { get; set; }

        public ICommand OpenMapCommand { get; private set; }

        private static async void OpenMap(CheckPoint point)
        {
            await Launcher.OpenAsync(point.GoogleMapUrl);
        }
    }
}
