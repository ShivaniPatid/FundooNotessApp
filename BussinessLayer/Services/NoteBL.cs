using System;
using System.Collections.Generic;
using System.Text;
using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

namespace BussinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public NoteEntity AddNotes(NoteModel noteModel, long userId)
        {
            try
            {
                return noteRL.AddNotes(noteModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                return noteRL.DeleteNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity UpdateNote(NoteModel noteModel, long userId, long noteId)
        {
            try
            {
                return noteRL.UpdateNote(noteModel, userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PinNote(long userId, long noteId)
        {
            try
            {
                return noteRL.PinNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TrashNote(long userId, long noteId)
        {
            try
            {
                return noteRL.TrashNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ArchiveNote(long userId, long noteId)
        {
            try
            {
                return noteRL.ArchiveNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity AddColor(long userId, long noteId, string color)
        {
            try
            {
                return noteRL.AddColor(userId, noteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<NoteEntity> GetNotesByUserId(long userId)
        {
            try
            {
                return noteRL.GetNotesByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<NoteEntity> GetAllNotes()
        {
            try
            {
                return noteRL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity UploadImage(long noteId, IFormFile image)
        {
            try
            {
                return noteRL.UploadImage(noteId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
