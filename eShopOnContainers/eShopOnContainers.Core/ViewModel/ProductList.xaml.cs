using System;
using eShopOnContainers.Core.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShopOnContainers.Core.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductList : ContentPage
    {
        string kategoriIsmi;
        public ProductList(string katismi)
        {
            kategoriIsmi = katismi;
            InitializeComponent ();
            UrunleriGetir();
        }
        private async void AramaButon(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PushAsync(new Search());
        }
        private async void SepetButton(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PushAsync(new Basket());
        }
        private async void GeriButton(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PopAsync();
        }
        async void UrunleriGetir()
        {
            //Ürünleri databaseden çekiyoruz ve listviewimize ekliyoruz
            altkatisim.Text = kategoriIsmi;
            urunListe.ItemsSource = null;
            urunListe.BindingContext = null;
            Database database = Database.Instance();
            var urunler = await database.KategoriUrunGetir(kategoriIsmi);
            urunListe.ItemsSource = urunler;
            urunListe.BindingContext = urunler;
        }
        private async void UrunDetayGoster(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];
            Urun img = (Urun)tapGesture.CommandParameter;
            await TappedHome.instance.Navigation.PushAsync(new eShopOnContainers.Core.ViewModel.ProductDetail(img));
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

