using System;
using System.Collections.Generic;
using BorderCrossing.ViewModels;
using BorderCrossing.Views;
using Xamarin.Forms;

namespace BorderCrossing
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
