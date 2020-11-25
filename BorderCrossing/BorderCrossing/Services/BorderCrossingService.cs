using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BorderCrossing.Extensions;
using BorderCrossing.Models;
using BorderCrossing.Models.Google;
using NetTopologySuite.Geometries;

namespace BorderCrossing.Services
{
    public interface IBorderCrossingService
    {
        LocationHistory LocationHistory { get; set; }
        List<CheckPoint> CheckPoints { get; set; }
        Task<QueryRequest> GetQueryRequestAsync();
        Task ParseLocationHistoryAsync(QueryRequest model, ProgressChangedEventHandler callback);
    }

    public class BorderCrossingService : IBorderCrossingService
    {
        public LocationHistory LocationHistory { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }

        public async Task<QueryRequest> GetQueryRequestAsync()
        {
            return await Task.FromResult(new QueryRequest
            {
                StartDate = LocationHistory.Locations.Min(l => l.TimestampMsUnix).ToDateTime(),
                EndDate = LocationHistory.Locations.Max(l => l.TimestampMsUnix).ToDateTime(),
                IntervalType = IntervalType.Every12Hours
            });
        }

        public async Task ParseLocationHistoryAsync(QueryRequest model, ProgressChangedEventHandler callback)
        {
            var locations = BorderCrossingHelper.PrepareLocations(LocationHistory, model.IntervalType);
            var filteredLocations = locations.Where(l => l.Date >= model.StartDate && l.Date <= model.EndDate).OrderBy(l => l.TimestampMsUnix).ToList();
            
            var currentLocation = filteredLocations.First();
            var currentCountry = GetCountryName(currentLocation.Point);
            
            var i = 0;
            var count = filteredLocations.Count();

            CheckPoints = new List<CheckPoint>
            {
                BorderCrossingHelper.LocationToCheckPoint(currentLocation, currentCountry)
            };

            foreach (var location in filteredLocations)
            {
                await Task.Run(() =>
                {
                    i++;
                    callback(this, new ProgressChangedEventArgs((int)(100.0 * i / count), null));

                    var countryName = GetCountryName(location.Point);
                    if (currentCountry == countryName)
                    {
                        currentLocation = location;
                        return;
                    }

                    var range = LocationHistory.Locations.Where(lh => lh.TimestampMsUnix >= currentLocation.TimestampMsUnix && lh.TimestampMsUnix <= location.TimestampMsUnix).ToList();
                    var checkPoint = BorderCrossingHelper.FindCheckPoint(range, currentLocation, currentCountry, location, countryName, GetCountryName);

                    CheckPoints.Add(checkPoint);
                    currentCountry = countryName;
                    currentLocation = location;
                });
            }

            var last = filteredLocations.Last();
            CheckPoints.Add(BorderCrossingHelper.LocationToCheckPoint(last, GetCountryName(last.Point)));
        }

        private static string GetCountryName(Geometry point)
        {
            var country = CountryStorage.GetCountryStorage().Countries.FirstOrDefault(c => point.Within(c.Geom));

            country ??= CountryStorage.GetCountryStorage().Countries
                .Select(c => new { Country = c, Distance = c.Geom.Distance(point) })
                .OrderBy(d => d.Distance)
                .FirstOrDefault(c => c.Distance * 100 < 10)?.Country;

            return country == null ? "Unknown" : country.Name;
        }
    }
}