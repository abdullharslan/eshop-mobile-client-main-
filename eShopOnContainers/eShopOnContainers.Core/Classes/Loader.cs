using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace eShopOnContainers.Core.Classes
{
    internal class Loader
    {

        public static void FavoriKaydet(List<Urun> urun)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "favori.xml");

            try
            {

                var xml = new XmlSerializer(typeof(List<Urun>));
                //Ornek tipinde bir serileştirme yapacak nesnemizi oluşturuyorum.
                using (StreamWriter sw = new StreamWriter(folderPath))
                {
                    xml.Serialize(sw, urun);
                    //Şuanki mevcut nesneyi(this) sw'nin tuttuğu dizine xml olarak serileştiriyoruz.
                }
            }
            catch (Exception)
            {
                using (FileStream fs = File.Create(folderPath)) ;

            }
        }
        public static List<Urun> FavoriYukle()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "favori.xml");

            try
            {
                var xml = new XmlSerializer(typeof(List<Urun>));
                //Ornek tipinde bir deserialization yapacak nesnemizi oluşturuyorum.
                using (StreamReader sr = new StreamReader(folderPath))
                {
                    List<Urun> o = (List<Urun>)xml.Deserialize(sr);
                    //Verilen dizindeki xml deserialization yapılarak nihayetinde Ornek tipinde elde edilecektir.
                    //object olarak gelen nesneyi biz Ornek tipine cast ediyoruz.
                    return o;
                    //Gelen nesnenin verilerini şuandaki mevcut nesnenin veri alanlarına atamaktayız.
                }
            }
            catch
            {
                using (FileStream fs = File.Create(folderPath)) ;
                return new List<Urun>();
            }
        }

        public static void SepetKaydet(List<Urun> urun)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "sepet.xml");

            try
            {

                var xml = new XmlSerializer(typeof(List<Urun>));
                //Ornek tipinde bir serileştirme yapacak nesnemizi oluşturuyorum.
                using (StreamWriter sw = new StreamWriter(folderPath))
                {
                    xml.Serialize(sw, urun);
                    //Şuanki mevcut nesneyi(this) sw'nin tuttuğu dizine xml olarak serileştiriyoruz.
                }
            }
            catch (Exception)
            {
                using (FileStream fs = File.Create(folderPath)) ;

            }
        }
        public static List<Urun> SepetYukle()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "sepet.xml");

            try
            {
                var xml = new XmlSerializer(typeof(List<Urun>));
                //Ornek tipinde bir deserialization yapacak nesnemizi oluşturuyorum.
                using (StreamReader sr = new StreamReader(folderPath))
                {
                    List<Urun> o = (List<Urun>)xml.Deserialize(sr);
                    //Verilen dizindeki xml deserialization yapılarak nihayetinde Ornek tipinde elde edilecektir.
                    //object olarak gelen nesneyi biz Ornek tipine cast ediyoruz.
                    return o;
                    //Gelen nesnenin verilerini şuandaki mevcut nesnenin veri alanlarına atamaktayız.
                }
            }
            catch
            {
                using (FileStream fs = File.Create(folderPath)) ;
                return new List<Urun>();
            }
        }
    }
}


