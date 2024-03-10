using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess;

namespace TodoApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NoteController(ApplicationDbContext applicationDbContext)
        {
            this._db = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<Note> notes = this._db.Notes.OrderBy(x => x.Id).ToList();
            return View(notes);
        }

        public IActionResult Create()
        {
                return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (note == null) { return View(); }
            else
            {
                _db.Add(note);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            Note? note = _db.Notes.Find(id);
            if(note == null) { 
               return RedirectToAction("Index");
            } else
            {
                return View("Edit", note);
            }
        }
        [HttpPost]
        public IActionResult Edit(Note note)
        {
                _db.Notes.Update(note);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Note? note = _db.Notes.Find(id);
            if (note == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Delete", note);
            }
        }
        [HttpPost]
        public IActionResult Delete(Note note)
        {
            Note? noteToDelete = _db.Notes.Find(note.Id);
            if (noteToDelete == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _db.Notes.Remove(noteToDelete);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
