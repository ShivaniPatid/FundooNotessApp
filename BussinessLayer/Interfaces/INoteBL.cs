using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entities;

namespace BussinessLayer.Interfaces
{
    public interface INoteBL
    {
        public NoteEntity AddNotes(NoteModel noteModel, long userId);
        public bool DeleteNote(long userId, long noteId);
        public NoteEntity UpdateNote(NoteModel noteModel, long userId, long noteId);
        public bool PinNote(long userId, long noteId);
        public bool TrashNote(long userId, long noteId);
        public bool ArchiveNote(long userId, long noteId);
        public NoteEntity AddColor(long userId, long noteId, string color);

    }
}
