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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }

        [HttpPost("Add")]
        public IActionResult AddLabel(long noteId, string labelName)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.AddLabel(userId, noteId, labelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label Added", data = result });
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
        public IActionResult DeleteLabel(long labelId, string labelName)
        {
            try
            {
                var result = labelBL.DeleteLabel(labelId, labelName);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Label Deleted" });
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
        public IActionResult RenameLabel(long labelId, string newLabelName)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.RenameLabel(userId, labelId, newLabelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label Updated", data = result });
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

        [HttpGet("View")]
        public IEnumerable<LabelEntity> GatLabelByNoteId(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return labelBL.GatLabelByNoteId(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
