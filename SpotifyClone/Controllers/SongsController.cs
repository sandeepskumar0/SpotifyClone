using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpotifyClone.Models;

namespace SpotifyClone.Controllers
{
    public class SongsController : Controller
    {
        private SpotifyEntities db = new SpotifyEntities();

        // GET: Songs
        public ActionResult Index()
        {
            var songs = db.Songs.Include(s => s.Category);
            return View(songs.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            ViewBag.Cat_fid = new SelectList(db.Categories, "Cat_Id", "Cat_Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Song song)
        {


            if (ModelState.IsValid)
            {

                song.M_Fil.SaveAs(Server.MapPath("~/Media/" + song.M_Fil.FileName));

                if (song.M_Fil.FileName != "")
                {
                    song.M_File = "~/Media/" + song.M_Fil.FileName;
                    db.Songs.Add(song);
                    db.SaveChanges();
                }
                else
                {
                    song.M_File = null;
                }

                return RedirectToAction("Index");
            }


            ViewBag.Cat_fid = new SelectList(db.Categories, "Cat_Id", "Cat_Name", song.Cat_fid);
            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_fid = new SelectList(db.Categories, "Cat_Id", "Cat_Name", song.Cat_fid);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "M_Id,M_Name,M_File,M_Desc,Cat_fid")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_fid = new SelectList(db.Categories, "Cat_Id", "Cat_Name", song.Cat_fid);
            return View(song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
