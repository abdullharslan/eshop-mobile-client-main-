using eShopOnContainers.Core;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eShopOnContainers
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TappedHome());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}