using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using static System.Net.Mime.MediaTypeNames;


namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        private readonly Context context;
        public const string CLOUD_NAME = "dacnbb290";
        public const string API_KEY = "295468678434646";
        public const string API_SECERET = "CfYnJMRNf4zsZ3VVqy6iHw4h_YA";
        public static Cloudinary cloudinary;
        public NoteRL(Context context)
        {
            this.context = context;
        }

        public NoteEntity AddNotes(NoteModel noteModel, long userId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = noteModel.Title;
                noteEntity.Note = noteModel.Note;
                noteEntity.Color = noteModel.Color;
                noteEntity.Image = noteModel.Image;
                noteEntity.IsArchive = noteModel.IsArchive;
                noteEntity.IsPin = noteModel.IsPin;
                noteEntity.IsTrash = noteModel.IsTrash;
                noteEntity.UserId = userId;
                this.context.Notes.Add(noteEntity);
                int result = this.context.SaveChanges();
                if(result != 0)
                {
                    return noteEntity;
                }
                else
                    return null;
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
                var note = this.context.Notes.Where(s => s.NoteID == noteId).FirstOrDefault();
                if (note != null)
                {
                    this.context.Notes.Remove(note);
                    this.context.SaveChanges();
                    return true;
                }
                else
                    return false;
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
                var note = this.context.Notes.Where(s => s.NoteID == noteId && s.UserId == userId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = noteModel.Title != "string" ? noteModel.Title : note.Title;
                    note.Note = noteModel.Note != "string" ? noteModel.Note : note.Note;
                    note.Color = noteModel.Color != "string" ? noteModel.Color : note.Color;
                    note.Image = noteModel.Image != "string" ? noteModel.Image : note.Image;
                    note.IsArchive = noteModel.IsArchive != true ? noteModel.IsArchive : note.IsArchive;
                    note.IsPin = noteModel.IsPin != true ? noteModel.IsPin : note.IsPin;
                    note.IsTrash = noteModel.IsTrash != true ? noteModel.IsTrash : note.IsTrash;

                    this.context.Notes.Update(note);
                    this.context.SaveChanges();
                    return note;
                }
                else
                    return null;
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
                var note = this.context.Notes.Where(s => s.NoteID == noteId && s.UserId == userId).FirstOrDefault();
                if (note != null)
                {
                    note.IsPin = !note.IsPin;
                    this.context.SaveChanges();
                    return true;
                }
                else
                    return false;
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
                var note = this.context.Notes.Where(s => s.NoteID == noteId && s.UserId == userId).FirstOrDefault();
                if (note != null)
                {
                    note.IsTrash = !note.IsTrash;
                    this.context.SaveChanges();
                    return true;
                }
                else
                    return false;
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
                var note = this.context.Notes.Where(s => s.NoteID == noteId && s.UserId == userId).FirstOrDefault();
                if (note != null)
                {
                    note.IsArchive = !note.IsArchive;
                    this.context.SaveChanges();
                    return true;
                }
                else
                    return false;
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
                var note = this.context.Notes.Where(s => s.NoteID == noteId && s.UserId == userId).FirstOrDefault();
                if(note != null)
                {
                    note.Color = color;
                    this.context.SaveChanges();
                    return note;
                }
                else
                    return null;
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
                var notes = this.context.Notes.Where(s => s.UserId == userId);
                if(notes != null)
                {
                    return notes;
                }
                else
                    return null;
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
                var notes = this.context.Notes;
                if (notes != null)
                {
                    return notes;
                }
                else
                    return null;
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
                var note = this.context.Notes.Where(s => s.NoteID == noteId).FirstOrDefault();
                if (note != null)
                {
                    Account account = new Account(CLOUD_NAME, API_KEY, API_SECERET);
             
                    cloudinary = new Cloudinary(account);
                    var imagePath = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, imagePath),
                    };

                    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                    note.Image = image.FileName;
                    context.Notes.Update(note);
                    int upload = context.SaveChanges();
                    if (upload > 0)
                    {
                        return note;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}
