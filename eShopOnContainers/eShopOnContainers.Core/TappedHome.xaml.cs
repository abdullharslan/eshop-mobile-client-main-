using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShopOnContainers.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TappedHome : TabbedPage
    {
        public static TappedHome instance { get; set; }
        public TappedHome()
        {
            InitializeComponent();
            instance = this;

        }

    }
}

