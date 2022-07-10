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
        private TodoListContext db;
        public NoteSelects()
        {
            db = new TodoListContext();
        }
        public bool CreateNote(Note noteNew)
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
        public Note GetNoteById(int id)
        {
            var note = db.Notes.FirstOrDefault(c => c.Id == id);
            return note;
        }
        public List<Note> GetNoteByCategoryId(int categoryId)
        {
            var notes = db.Notes.Where(c => c.CategoryId == categoryId).ToList();
            return notes;
        }
        public List<Note> GetNoteByUserId(int userId)
        {
            var notes = db.Notes.Include(n=>n.Category).Where(c => c.Category.UserId == userId).ToList();
            return notes;
        }
        public bool ChangeNote(Note noteNew)
        {
            try
            {
                Note note = db.Notes.FirstOrDefault(c => c.Id == noteNew.Id);
                if (note != null)
                {
                    note.Text = noteNew.Text;
                    note.Heading = noteNew.Heading;
                    //note.CreateDate = note.CreateDate; дату создания не измением
                    note.Deadline = noteNew.Deadline;
                    note.Status = noteNew.Status;
                    note.CategoryId = noteNew.CategoryId;
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
        public bool ChangeStatusNote(int id, bool status)
        {
            try
            {
                Note note = db.Notes.FirstOrDefault(c => c.Id == id);
                if (note != null)
                {
                    note.Status = status;
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
        public bool DeleteNoteById(int id)
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
