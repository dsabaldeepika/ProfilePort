using ProfilePort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilePort.Adapters.DataAdapters
{
    public class Notes :INotes
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        List<DataModel.Note> INotes.GetNote(String UserId)
        {
            return db.Notes.Where(m => m.UserId == UserId).ToList();
        }

        DataModel.Note INotes.GetNote(int id)
        {
            return db.Notes.Where(m => m.Id == id).FirstOrDefault();
        }

        DataModel.Note INotes.PostNewNote(string UserId, DataModel.Note newNote)
        {
            Note Note = new Note();
            Note.NoteContent = newNote.NoteContent;
            Note.Title = newNote.Title;
            Note.UserId = UserId;

            db.Notes.Add(Note);
            db.SaveChanges();
            return newNote;
        }

        DataModel.Note INotes.PutNote(int id, DataModel.Note newNote)
        {
            Note Note = new Note();
            Note = db.Notes.Where(m => m.Id == id).FirstOrDefault();

            Note.NoteContent = newNote.NoteContent;
            Note.Title = newNote.Title;

            db.SaveChanges();
            return newNote;
        }

        DataModel.Note INotes.DeleteNote(int id)
        {
            Note Note = new Note();
            Note = db.Notes.Where(m => m.Id == id).FirstOrDefault();
            db.Notes.Remove(Note);
            db.SaveChanges();
            return Note;
        }
    }
}