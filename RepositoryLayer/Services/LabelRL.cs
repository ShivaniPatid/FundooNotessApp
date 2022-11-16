using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly Context context;
        public LabelRL(Context context)
        {
            this.context = context;
        }

        public LabelEntity AddLabel(long userId, long noteId, string labelName)
        {
            try
            {
                var note = context.Notes.Where(s => s.UserId == userId && s.NoteID == noteId).FirstOrDefault();

                if (note != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.UserId = userId;
                    labelEntity.NoteId = noteId;
                    labelEntity.LabelName = labelName;
                    context.Label.Add(labelEntity);
                    context.SaveChanges();
                    return labelEntity;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteLabel(long labelId, string labelName)
        {
            try
            {
                var label = context.Label.Where(e => e.LabelID == labelId && e.LabelName == labelName).FirstOrDefault();
                if(label != null)
                {
                    context.Label.Remove(label);
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

        public LabelEntity RenameLabel(long userId, long labelId, string newLabelName)
        {
            try
            {
                var label = context.Label.Where(e => e.UserId == userId && e.LabelID == labelId).FirstOrDefault();
                if (label != null)
                {
                    label.LabelName = newLabelName;
                    context.SaveChanges();
                    return label;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LabelEntity> GatLabelByNoteId(long userId, long noteId)
        {
            try
            {
                var label = context.Label.Where(e => e.UserId == userId && e.NoteId == noteId);
                if (label != null)
                {
                    return label;
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
