using System.ComponentModel;
using Xamarin.Forms;
using BorderCrossing.ViewModels;

namespace BorderCrossing.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}