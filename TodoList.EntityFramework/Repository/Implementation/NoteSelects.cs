using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain;
using Microsoft.EntityFrameworkCore;
using TodoList.EntityFramework.Repository.Interfaces;

namespace TodoList.EntityFramework.Repository.Implementation
{
    public class NoteSelects : INoteSelects
    {
        public bool CreateNote(Note noteNew)
        {
            using (TodoListContext db = new TodoListContext())
            {
                try
                {
                    if (noteNew.CategoryId != 0)
                    {
                        db.Notes.Add(noteNew);
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
        public Note GetNoteById(int id)
        {
            using (TodoListContext db = new TodoListContext())
            {
                var note = db.Notes.FirstOrDefault(c => c.Id == id);
                return note;
            }
        }
        public List<Note> GetNoteByCategoryId(int categoryId)
        {
            using (TodoListContext db = new TodoListContext())
            {
                var notes = db.Notes.Where(c => c.CategoryId == categoryId).ToList();
                return notes;
            }
        }
        public List<Note> GetNoteByUserId(int userId)
        {
            using (TodoListContext db = new TodoListContext())
            {
                var notes = db.Notes.Include(n=>n.Category).Where(c => c.Category.UserId == userId).ToList();
                return notes;
            }
        }
        public bool ChangeNote(Note noteNew)
        {
            using (TodoListContext db = new TodoListContext())
            {
                try
                {
                    Note note = db.Notes.FirstOrDefault(c => c.Id == noteNew.Id);
                    if (note != null)
                    {
                       note = noteNew;
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool DeleteNoteById(int id)
        {
            using (TodoListContext db = new TodoListContext())
            {
                try
                {
                    Note note = db.Notes.FirstOrDefault(c => c.Id == id);
                    if (note != null)
                    {
                        db.Notes.Remove(note);
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
