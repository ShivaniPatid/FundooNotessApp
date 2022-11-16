using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface ICollabRL
    {
        public CollabEntity AddCollaborator(long userId, long noteId, string email);
        public IEnumerable<CollabEntity> GetCollaboratorsByNoteId(long userId, long noteId);
        public bool RemoveCollaborator(long userId, long noteId, long collabId);
        

    }
}
