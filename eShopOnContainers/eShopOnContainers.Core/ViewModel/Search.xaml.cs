using System;
using System.Collections.Generic;
using eShopOnContainers.Core.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShopOnContainers.Core.ViewModel
{
    public partial class Search : ContentPage
	{	
		public Search ()
		{
			InitializeComponent ();
            FirstSearch();
        }
        async void FirstSearch()
        {
            Database data = Database.Instance();
            var products = await data.ButunUrunleriGetir();
            aramaListe.ItemsSource = products;
            aramaListe.BindingContext = products;
        }
        private async void GeriAl(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PopAsync();
        }
        private async void SepetButton(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PushAsync(new Basket());
        }
        private async void AramaYazisiDegisti(object sender, TextChangedEventArgs e)
        {
            // arama yazı kutusunun içeriği değiştiğinde tekrar güncel bilgileri veri tabanından çekiyoruz
            aramaListe.ItemsSource = null;
            aramaListe.BindingContext = null;
            Database data = Database.Instance();
            var products = await data.ButunUrunleriGetir();
            List<Urun> uruns = new List<Urun>();
            foreach (var item in products)
            {
                if (item.UrunIsim.Contains(searchbar.Text))
                {
                    uruns.Add(item);
                }
            }
            aramaListe.ItemsSource = uruns;
            aramaListe.BindingContext = uruns;
        }
        private async void UrunDetayAc(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];
            Urun img = (Urun)tapGesture.CommandParameter;

            await Navigation.PushAsync(new ProductDetail(img));
        }
        private async void FavorilereEkle(object sender, EventArgs e)
        {
            ImageButton frame = (ImageButton)sender;
            Urun img = (Urun)frame.CommandParameter;

            Database database = Database.Instance();
            var data = await database.FavorilereEkle(img);
            if (data)
            {
                await DisplayAlert("BAŞARILI", "ÜRÜN FAVORİLERE EKLENDİ", "TAMAM");
            }
            else
            {
                await DisplayAlert("ZATEN VAR", "ÜRÜN FAVORİLERDE ZATEN VAR", "TAMAM");
            }
        }
    }
}

