using System.Linq;
using System;
using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.AppContext;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using BussinessLayer.Services;

namespace FundooNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly Context context;
        private readonly IDistributedCache distributedCache;

        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, Context context, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
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

        [HttpGet("RedisCache")]
        public async Task<IActionResult> GetAllCollaUsingRedisCache()
        {
            var cacheKey = "collabList";
            string serializedCollabList;
            var collabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                collabList = await context.Collaborator.ToListAsync();
                //noteList = (List<NoteEntity>)noteBL.GetAllNotes();
                serializedCollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }
    }
}
