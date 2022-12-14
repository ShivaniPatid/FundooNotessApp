using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public LabelEntity AddLabel(long userId,long noteId, string labelName);
        public bool DeleteLabel(long labelId, string labelName);
        public LabelEntity RenameLabel(long userId, long labelId, string newLabelName);
        public IEnumerable<LabelEntity> GatLabelByNoteId(long userId, long noteId);
    }
}
