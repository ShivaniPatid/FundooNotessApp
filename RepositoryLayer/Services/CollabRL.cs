using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        private readonly Context context;

        public CollabRL(Context context)
        {
            this.context = context;
        }

        public CollabEntity AddCollaborator(long userId, long noteId, string email)
        {
            try
            {
                var note = context.Notes.Where(s => s.UserId == userId && s.NoteID == noteId).FirstOrDefault();
                if(note != null)
                {
                    CollabEntity collabEntity = new CollabEntity();
                    collabEntity.UserId = userId;
                    collabEntity.NoteId = noteId;
                    collabEntity.CollabEmail = email;
                    context.Collaborator.Add(collabEntity);
                    context.SaveChanges();
                    return collabEntity;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CollabEntity> GetCollaboratorsByNoteId(long userId, long noteId)
        {
            try
            {
                var collabs = this.context.Collaborator.Where(s => s.UserId == userId && s.NoteId == noteId).DefaultIfEmpty();
                if (collabs != null)
                {
                    return collabs;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RemoveCollaborator(long userId, long noteId, long collabId)
        {
            try
            {
                var collab = context.Collaborator.Where(s => s.UserId == userId && s.NoteId == noteId && s.CollabId == collabId).FirstOrDefault();
                if (collab != null)
                {
                    context.Collaborator.Remove(collab);
                    context.SaveChanges();
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

    }
}
