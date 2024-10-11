using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVCSTOK.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        //entity klasörünün içinde bylynsn modelimizide burada tanıtııyoruz
        MvcDbStokEntities db = new MvcDbStokEntities();
        // MvcDbStokEntities bu benim modelimi tutuyor modelimde de tablolar var
        public ActionResult Index(int sayfa=1)
        {
            //select işlemi yapıcaz ama bunun için önce degerler adında bir değişken tnaımlamamız alzım
            //var degerler = db.TBLKATEGORİLER.ToList();
            var degerler = db.TBLKATEGORİLER.ToList().ToPagedList(sayfa,4);
            //execute reader sql okuma vs bunları kullanmadan direkt aslında tolist metodula ile haleltmiş olduk
            return View(degerler);

        }


        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }



        [HttpPost]//sayfaya herhangi bir post işlemi yapıldığı zaman  burayı çalıştır aksi durumda yukarıdkai get i çalıştır
        //eğer bunu koymazsan veri eklemezsen bile null olarak boş veri ekler her sen sayfaya bastığında ve bunu istemeyiz
        //buradaki olay requiredden farklı olarak burada mavi lan buton yenikategori sayfasına basınca yani sayfaya gitsem bile veri ekliypr
        public ActionResult YeniKategori(TBLKATEGORİLER p1)//buranın isminin yenikategori olması index kısmında öyle tanımlanıyor ondan veya tam tersi
        {

            if (!ModelState.IsValid)//doğrulanma işlemi yapılmadıysa
            {

                return View("YeniKategori");
            }
            db.TBLKATEGORİLER.Add(p1);//tblkategorilerin içerisine ekle demek
            db.SaveChanges();//değişiklikleri kaydet
            return View();

        }

        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORİLER.Find(id);//tblkategoriler içersinde bul id den gelen değeri
                                                      //id yi diğer sayfaya göndermiş olduğumuz değerle alıcaz
            db.TBLKATEGORİLER.Remove(kategori);
            db.SaveChanges();

            return RedirectToAction("Index");//beni index sayfasına yönlendir
        }



        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORİLER.Find(id);
            return View("KategoriGetir", ktgr);
        }



        public ActionResult Guncelle(TBLKATEGORİLER p1)//buranın içine güncellenecek değerleri yazacaz
        {
            var ktg = db.TBLKATEGORİLER.Find(p1.KategoriId);//bana tbl kategoriler içersinden p1 den gönderdiğim kategorinin id sinibul 
            ktg.KategoriAd = p1.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}