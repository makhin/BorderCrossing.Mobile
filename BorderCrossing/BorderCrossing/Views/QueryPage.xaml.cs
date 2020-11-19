using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BorderCrossing.Models.Google;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BorderCrossing.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueryPage : ContentPage
    {
        private LocationHistory _locationHistory;

        public QueryPage()
        {
            InitializeComponent();
        }

        public LocationHistory LocationHistory
        {
            get => _locationHistory;

            set => _locationHistory = value;
        }
    }
}