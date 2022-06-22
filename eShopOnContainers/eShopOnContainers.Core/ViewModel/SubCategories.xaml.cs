using System;
using System.Collections.Generic;
using eShopOnContainers.Core.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShopOnContainers.Core.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubCategories : ContentPage
    {
        string _categoryIsim;
        public SubCategories (string KategoriIsim)
		{
			InitializeComponent ();
            KategoriIsmiText.Text = KategoriIsim;
            _categoryIsim = KategoriIsim;
            KategorileriCek();
        }
        async void KategorileriCek()
        {
            //Listeyi boşaltıyoruz
            KategoriListe.ItemsSource = null;
            KategoriListe.BindingContext = null;

            Database db = Database.Instance();

            //Veritabanından kategorileri çekiyoruz
            var kategoriler = await db.GetAllCategory();

            List<Category> _kategoriler = new List<Category>(kategoriler);
            foreach (var kategori in kategoriler)
            {
                if (kategori.ParentCategory != null)
                {
                    if (kategori.ParentCategory.CategoryName != _categoryIsim)
                    {
                        _kategoriler.Remove(kategori);
                    }
                }
                else
                {
                    _kategoriler.Remove(kategori);
                }
            }
            foreach (var kategori in kategoriler)
            {
                if (kategori.ParentCategory != null)
                {
                    foreach (var _kategori in _kategoriler)
                    {
                        if (kategori.ParentCategory.CategoryName == _kategori.CategoryName)
                        {
                            _kategori.childvarmi = true;
                        }
                    }
                }
            }
            KategoriListe.ItemsSource = _kategoriler;
            KategoriListe.BindingContext = _kategoriler;
        }
        private async void KategoriAc(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];
            Category kategori = (Category)tapGesture.CommandParameter;
            Database db = Database.Instance();
            //Veritabanından kategorileri çekiyoruz
            var kategoriler = await db.GetAllCategory();
            List<Category> _kategoriler = new List<Category>();
            foreach (var kat in kategoriler)
            {
                if (kat.ParentCategory != null)
                {
                    if (kat.ParentCategory.CategoryName == kategori.CategoryName)
                    {
                        _kategoriler.Add(kat);
                    }
                }
            }
            //Eğer alt kategori yoksa direk ürünlerin göründüğü sayfaya, varsa ise yeniden alt kategoriye yönlendiriyoruz
            if (_kategoriler.Count == 0)
            {
                await TappedHome.instance.Navigation.PushAsync(new ProductList(kategori.CategoryName));
            }
            else
            {
                await TappedHome.instance.Navigation.PushAsync(new SubCategories(kategori.CategoryName));
            }
        }
        private async void GeriButton_Clicked(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PopAsync();
        }
        private async void SepetButton_Clicked(object sender, EventArgs e)
        {
            await TappedHome.instance.Navigation.PushAsync(new Basket());

        }
    }
}

