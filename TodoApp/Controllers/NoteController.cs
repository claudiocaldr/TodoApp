using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess;
using TodoApp.Helpers;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NoteController(ApplicationDbContext applicationDbContext)
        {
            this._db = applicationDbContext;
        }
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 1;
            if (TempData["notes"] != null)
            {
                var data = TempDataExtensions.Get<List<Note>>(TempData, "notes");
                return View(PaginatedList<Note>.Create(data, pageNumber ?? 1, pageSize));
            }

            if (pageNumber > 0)
            {
                var notesList = await PaginatedList<Note>.CreateAsync(this._db.Notes.AsNoTracking(), pageNumber ?? 1, pageSize);
                if (notesList.Count > 0)
                {
                    return View(notesList);
                }
                else { return View(await PaginatedList<Note>.CreateAsync(this._db.Notes.AsNoTracking(), (pageNumber != null ? (int)pageNumber - 1 : 1), pageSize)); }
            }
            else
            {
                return View(await PaginatedList<Note>.CreateAsync(this._db.Notes.AsNoTracking(), 1, pageSize));
            }
        }
        [HttpPost]
        public async Task<IActionResult> SearchByTitle(string name)
        {
            int pageSize = 2;
            PaginatedList<Note> notes = await PaginatedList<Note>.CreateAsync(this._db.Notes.AsNoTracking().Where(x => x.Title.Contains(name)), 1, pageSize);
            TempDataExtensions.Put(TempData, "notes", notes);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SearchByDescription(string name)
        {
            int pageSize = 2;
            PaginatedList<Note> notes = await PaginatedList<Note>.CreateAsync(this._db.Notes.AsNoTracking().Where(x => x.Description.Contains(name)), 1, pageSize);
            TempDataExtensions.Put(TempData, "notes", notes);
            return RedirectToAction("Index");
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
            if (note == null)
            {
                return RedirectToAction("Index");
            }
            else
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
