using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public NoteEntity AddNotes(NoteModel noteModel, long userId);
        public bool DeleteNote(long userId, long noteId);
        public NoteEntity UpdateNote(NoteModel noteModel, long userId, long noteId);
        public bool PinNote(long userId, long noteId);
        public bool TrashNote(long userId, long noteId);
        public bool ArchiveNote(long userId, long noteId);
        public NoteEntity AddColor(long userId, long noteId, string color);
        public IEnumerable<NoteEntity> GetNotesByUserId(long userId);
        public IEnumerable<NoteEntity> GetAllNotes();
        public NoteEntity UploadImage(long noteId, IFormFile image);
    }
}
