using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entities;

namespace FundooNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
           
        }

        [HttpPost("Add")]
        public IActionResult AddNotes(NoteModel noteModel)
        {
            try
            {
                var userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = noteBL.AddNotes(noteModel, userID);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Note Added", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpDelete("Remove")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.DeleteNote(userId, noteId);
                if(result != false)
                {
                    return this.Ok(new { success = true, message = "Note Deleted" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateNote(NoteModel noteModel, long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.UpdateNote(noteModel, userId, noteId);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Note Updated", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Pin")]
        public IActionResult PinNote(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.PinNote(userId, noteId);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Done" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Trash")]
        public IActionResult TrashNote(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.TrashNote(userId, noteId);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Done" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Archive")]
        public IActionResult ArchiveNote(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.ArchiveNote(userId, noteId);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Done" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Color")]
        public IActionResult AddColor(long noteId, string color)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.AddColor(userId, noteId, color);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Done", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("ByUser")]
        public IEnumerable<NoteEntity> GatNoteByUserId(long userId)
        {
            try
            { 
                 return noteBL.GetNotesByUserId(userId);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("AllNotes")]
        public IEnumerable<NoteEntity> GatAllNotes()
        {
            try
            {
                return noteBL.GetAllNotes();
            }
                
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("UploadImage")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                var result = noteBL.UploadImage(noteId, image);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Done", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
