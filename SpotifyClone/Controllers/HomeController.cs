using SpotifyClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpotifyClone.Controllers
{
    public class HomeController : Controller
    {
        private SpotifyEntities  db  = new  SpotifyEntities();
        public ActionResult Index()
        {
            return View();
        }
     

        public ActionResult dashboard(int? id)
        {
            SongModel o = new SongModel();

            o.cat = db.Categories.ToList();


            if (id == null)
            {
                o.son = db.Songs.ToList();
            }
            else
            {
                o.son = db.Songs.Where(z => z.Cat_fid == id).ToList();
            }

            return View(o);

        }
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Liked()
        {


            return View();
        }
        public ActionResult AddtoLiked(int id)
        {

            List<Song> list;
            if (Session["myWish"] == null)
            {
                list = new List<Song>();
            }
            else
            {
                list = (List<Song>)Session["myWish"];
            }
            list.Add(db.Songs.Where(p => p.M_Id == id).FirstOrDefault());


            Session["myWish"] = list;
            return RedirectToAction("Liked");
        }
        public ActionResult RemoveFromLiked(int RowNo)
        {
            List<Song> list = (List<Song>)Session["myWish"];
            list.RemoveAt(RowNo);
            Session["myWish"] = list;
            return RedirectToAction("Liked");
        }


    }
}