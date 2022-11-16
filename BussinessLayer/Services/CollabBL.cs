using System;
using System.Collections.Generic;
using System.Text;
using BussinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace BussinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public CollabEntity AddCollaborator(long userId, long noteId, string email)
        {
            try
            {
                return collabRL.AddCollaborator(userId, noteId, email);
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
                return collabRL.GetCollaboratorsByNoteId(userId, noteId);
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
                return collabRL.RemoveCollaborator(userId, noteId, collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
