using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {

            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }



        [HttpGet]
        public ActionResult UrunEkle()
        {

            //selectlistitem listeden öğe seç gibi bir anlamı olan yapıdır 
            //burada parantez içersindeki yapı linq yapısıdır 
            List<SelectListItem> degerler = (from i in db.TBLKATEGORİLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd ?? "Kategori Adı Yok",  // Null kontrolü ekleniyor
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;//controller tarafındaki  ifadeyi diğer tarafa taşıyor
            //viewbagden dgr türettim ve o da degerlerdeki değeri içine alacak
            //değerler yukarıdaki sorgudan gelen değerleri tutuyor
            return View();
        }

        private void KategoriListesiDoldur()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORİLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd ?? "Kategori Adı Yok",
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
        }



        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {

            KategoriListesiDoldur();


            var ktg = db.TBLKATEGORİLER.Where(m => m.KategoriId == p1.UrunKategori).FirstOrDefault();


            p1.TBLKATEGORİLER = ktg;



            db.TBLURUNLER.Add(p1);
            db.SaveChanges();//executenonquerye karşılık geliyor 
                             // Kaydettikten sonra Index sayfasına yönlendir
            return RedirectToAction("Index");


        }


        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);//tblkategoriler içersinde bul id den gelen değeri
                                              //id yi diğer sayfaya göndermiş olduğumuz değerle alıcaz
            db.TBLURUNLER.Remove(urun);//buranın içerisindeki urun var urunden geliyor bir üstteki kod
            db.SaveChanges();
            
            return RedirectToAction("Index");//beni index sayfasına yönlendir
        }
        public ActionResult UrunGetir(int id)
        {
            var urun  = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORİLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd ?? "Kategori Adı Yok",  // Null kontrolü ekleniyor
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);     // Bulunan müşteri kaydını "MusteriGetir" isimli görünüme (view) gönderir.
        }

        public ActionResult Guncelle(TBLURUNLER p1)//buranın içine güncellenecek değerleri yazacaz
        {
            var urn = db.TBLURUNLER.Find(p1.UrunId);//bana tbl kategoriler içersinden p1 den gönderdiğim kategorinin id sinibul 
           urn.UrunAd = p1.UrunAd;
            urn.Marka = p1.Marka;
            urn.Stok = p1.Stok;
            urn.Fiyat = p1.Fiyat;
            //urn.UrunKategori= p1.UrunKategori;

            var ktg = db.TBLKATEGORİLER.Where(m => m.KategoriId == p1.UrunKategori).FirstOrDefault();


            urn.UrunKategori = p1.UrunKategori; // Assign the selected category

            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}