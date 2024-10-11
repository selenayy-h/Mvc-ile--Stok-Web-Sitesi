using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Controllers;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri

        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBLMUSTERİLER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p));
                    }
            return View(degerler.ToList());  
            //var degerler = db.TBLMUSTERİLER.ToList();
            //return View(degerler);
        }


        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }



        [HttpPost]//sayfaya herhangi bir post işlemi yapıldığı zaman  burayı çalıştır aksi durumda yukarıdkai get i çalıştır
        //eğer bunu koymazsan veri eklemezsen bile null olarak boş veri ekler her sen sayfaya bastığında ve bunu istemeyiz

        public ActionResult YeniMusteri(TBLMUSTERİLER p1)//buranın isminin yenikategori olması index kısmında öyle tanımlanıyor ondan veya tam tersi
        {



            if (!ModelState.IsValid)//doğrulanma işlemi yapılmadıysa
            {

                return View("YeniMusteri");
            }
            //buradaki müşterinin üstüne basıp add view diyip yenimusteri view ekleik
            db.TBLMUSTERİLER.Add(p1);//tblkategorilerin içerisine ekle demek
            db.SaveChanges();//değişiklikleri kaydet
            return View();

        }

        public ActionResult SIL(int id)
        {
            var musteri = db.TBLMUSTERİLER.Find(id);// Veritabanında TBLMUSTERİLER tablosunda verilen id ile eşleşen kaydı bulur.
                                                    // Bulunan müşteriyi (musteri) silmek üzere veritabanından kaldırır.  
            db.TBLMUSTERİLER.Remove(musteri);//buranın içerisindeki urun var urunden geliyor bir üstteki kod
            db.SaveChanges();// Yapılan değişiklikleri (silme işlemini) veritabanına kaydeder.

            return RedirectToAction("Index");  // İşlem tamamlandıktan sonra kullanıcıyı "Index" sayfasına yönlendirir.
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERİLER.Find(id);
            return View("MusteriGetir", mus);     // Bulunan müşteri kaydını "MusteriGetir" isimli görünüme (view) gönderir.
        }

        public ActionResult Guncelle(TBLMUSTERİLER p1)//buranın içine güncellenecek değerleri yazacaz
        {
            var ktg = db.TBLMUSTERİLER.Find(p1.MusteriId);//bana tbl kategoriler içersinden p1 den gönderdiğim kategorinin id sinibul 
            ktg.MusteriAd = p1.MusteriAd;
            ktg.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}