using System;
using System.Collections.Generic;
using System.Text;
using BussinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace BussinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public LabelEntity AddLabel(long userId,long noteId, string labelName)
        {
            try
            {
                return labelRL.AddLabel(userId, noteId, labelName);
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
                return labelRL.DeleteLabel(labelId, labelName);
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
                return labelRL.RenameLabel(userId, labelId, newLabelName);
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
                return labelRL.GatLabelByNoteId(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
