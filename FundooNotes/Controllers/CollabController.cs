using System.Linq;
using System;
using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System.Collections.Generic;

namespace FundooNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;

        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }

        [HttpPost("Add")]
        public IActionResult AddCollaborator(long noteId, string email)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabBL.AddCollaborator(userId, noteId, email);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Collaborator Added", data = result });
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

        [HttpGet("View")]
        public IEnumerable<CollabEntity> GetCollaboratorsByNoteId(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return collabBL.GetCollaboratorsByNoteId(userId, noteId);  
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("Remove")]
        public IActionResult RemoveCollaborator(long noteId, long collabId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabBL.RemoveCollaborator(userId, noteId, collabId);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Collaborator Deleted" });
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
