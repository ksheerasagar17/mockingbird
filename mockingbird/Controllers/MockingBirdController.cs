using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mockingbird.Models;

namespace mockingbird.Controllers
{
    public class MockingBirdController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MockingBird
        public async Task<ActionResult> Index()
        {
            return View(await db.MockApiHttpDatas.ToListAsync());
        }

        // GET: MockingBird/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MockApiHttpData mockApiHttpData = await db.MockApiHttpDatas.FindAsync(id);
            if (mockApiHttpData == null)
            {
                return HttpNotFound();
            }
            return View(mockApiHttpData);
        }

        // GET: MockingBird/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MockingBird/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Path,Verb,Headers,ResponseStatus,ContentType,ContentEncoding,ResponseBody,ResponseDelay,ApiStatus")] MockApiHttpData mockApiHttpData)
        {
            if (ModelState.IsValid)
            {
                db.MockApiHttpDatas.Add(mockApiHttpData);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mockApiHttpData);
        }

        // GET: MockingBird/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MockApiHttpData mockApiHttpData = await db.MockApiHttpDatas.FindAsync(id);
            if (mockApiHttpData == null)
            {
                return HttpNotFound();
            }
            return View(mockApiHttpData);
        }

        // POST: MockingBird/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Path,Verb,Headers,ResponseStatus,ContentType,ContentEncoding,ResponseBody,ResponseDelay,ApiStatus")] MockApiHttpData mockApiHttpData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mockApiHttpData).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mockApiHttpData);
        }

        // GET: MockingBird/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MockApiHttpData mockApiHttpData = await db.MockApiHttpDatas.FindAsync(id);
            if (mockApiHttpData == null)
            {
                return HttpNotFound();
            }
            return View(mockApiHttpData);
        }

        // POST: MockingBird/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            MockApiHttpData mockApiHttpData = await db.MockApiHttpDatas.FindAsync(id);
            db.MockApiHttpDatas.Remove(mockApiHttpData);
            await db.SaveChangesAsync();
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
