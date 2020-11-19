using System;
using System.Collections.Generic;
using System.Text;
using BorderCrossing.Models.Google;
using BorderCrossing.Services;
using Xamarin.Forms;

namespace BorderCrossing.ViewModels
{
    public class QueryPageModel : BaseViewModel
    {
        public QueryPageModel()
        {
            Title = "Query";
            var request = BorderCrossingService.GetQueryRequestAsync().Result;
        }
    }
}
