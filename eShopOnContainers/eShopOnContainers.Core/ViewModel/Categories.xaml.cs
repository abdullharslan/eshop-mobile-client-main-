using System;
using System.Collections.Generic;
using eShopOnContainers.Core.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShopOnContainers.Core.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categories : ContentPage
	{	
		public Categories ()
		{
			InitializeComponent ();
            GetCategories();
        }


        async void GetCategories()
        {
            //bütün kategorileri çekiyoruz ana kategori olmayan kategorileri siliyoruz böylece elimizde sadece ana kategoriler kalıyor.
            KategoriSutun.ItemsSource = null;
            KategoriSutun.BindingContext = null;
            Database database = Database.Instance();
            var dat = await database.GetAllCategory();
            List<Category> categories = new List<Category>(dat);
            foreach (var item in dat)
            {
                if (item.ParentCategory != null)
                {
                    if (item.ParentCategory.CategoryName != "AnaKategori")
                    {
                        categories.Remove(item);
                    }
                }
                else
                {
                    categories.Remove(item);
                }
            }
            KategoriSutun.ItemsSource = categories;
            KategoriSutun.BindingContext = categories;
        }
        private async void AramaButon(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PushAsync(new Search());
        }
        private async void SepetButton(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PushAsync(new Basket());
        }
        private void KategoriButton(object sender, EventArgs e)
        {
            TappedHome.instance.CurrentPage = TappedHome.instance.Children[1];
        }
        private async void KategoriAc(object sender, EventArgs e)
        {
            //eğer alt bir kategori yoksa urundetayi sayfasına, var ise bir alt kategoriler sayfasına gidiyoruz
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];
            Category img = (Category)tapGesture.CommandParameter;
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
    }
}

