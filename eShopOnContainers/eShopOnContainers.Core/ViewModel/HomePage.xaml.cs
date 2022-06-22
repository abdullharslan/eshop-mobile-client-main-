using System;
using System.Collections.Generic;
using eShopOnContainers.Core.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShopOnContainers.Core.ViewModel
{
    public partial class HomePage : ContentPage
	{	
		public HomePage ()
		{
			InitializeComponent ();
            SliderKontrol();
        }
        void SliderKontrol()
        {
            List<MiniKategori> miniKategoris = new List<MiniKategori>();
            miniKategoris.Add(new MiniKategori
            {
                UrunKategori = "Giyim",
                UrunResim = "https://firebasestorage.googleapis.com/v0/b/recentram-afa1b.appspot.com/o/katmak7.png?alt=media&token=f70c782f-9639-4ab1-9504-770cab132183",
            });
            miniKategoris.Add(new MiniKategori
            {
                UrunKategori = "Ayakkabılar",
                UrunResim = "https://firebasestorage.googleapis.com/v0/b/recentram-afa1b.appspot.com/o/katmat3.png?alt=media&token=f50882a6-8f07-4b74-9926-4533dfc51679",
            });
            miniKategoris.Add(new MiniKategori
            {
                UrunKategori = "Aksesuarlar",
                UrunResim = "https://firebasestorage.googleapis.com/v0/b/recentram-afa1b.appspot.com/o/katmat4.png?alt=media&token=c16ec2f0-940b-4d4c-a374-1d09b6e5903c",
            });


            MainCarouselView.ItemsSource = miniKategoris;
            Device.StartTimer(TimeSpan.FromSeconds(5), (Func<bool>)(() =>
            {
                MainCarouselView.Position = (MainCarouselView.Position + 1) % miniKategoris.Count;
                return true;
            }));
        }
        private async void AramaButon(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PushAsync(new Search());
        }
        private async void SepetButton(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PushAsync(new Basket());
        }
        private async void KategoriButton(object sender, EventArgs e)
        {
            TappedHome.instance.CurrentPage = TappedHome.instance.Children[1];
        }
        async void SahneDegistir(string categoryName)
        {
            Category img = new Category();
            img.CategoryName = categoryName;

            Database database = Database.Instance();
            var dat = await database.GetAllCategory();
            List<Category> categories = new List<Category>();
            foreach (var item in dat)
            {
                if (item.ParentCategory != null)
                {
                    if (item.ParentCategory.CategoryName == img.CategoryName)
                    {
                        categories.Add(item);
                    }
                }
            }
            if (categories.Count == 0)
            {
                await TappedHome.instance.Navigation.PushAsync(new ProductList(img.CategoryName));
            }
            else
            {
                await TappedHome.instance.Navigation.PushAsync(new SubCategories(img.CategoryName));
            }
        }
        private void Tanitim1Button(object sender, EventArgs e)
        {
            SahneDegistir("Pantolonlar");
        }
        private void Tanitim2Button(object sender, EventArgs e)
        {
            SahneDegistir("Ayakkabılar");
        }
        private void Tanitim3Button(object sender, EventArgs e)
        {
            SahneDegistir("Elbiseler");
        }
        private void Tanitim4Button(object sender, EventArgs e)
        {
            SahneDegistir("Bluzlar");
        }
        private void Tanitim5Button(object sender, EventArgs e)
        {
            SahneDegistir("Pantolonlar");
        }
        private void MiniTanitimButton(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];
            MiniKategori img = (MiniKategori)tapGesture.CommandParameter;
            SahneDegistir(img.UrunKategori);
        }
        public class MiniKategori
        {
            public string UrunKategori { get; set; }
            public string UrunResim { get; set; }
        }
    }
}