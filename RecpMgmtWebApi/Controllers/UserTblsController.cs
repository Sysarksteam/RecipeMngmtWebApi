using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RecpMgmtWebApi.Models;

namespace RecpMgmtWebApi.Controllers
{
    public class UserTblsController : ApiController
    {
        private RecipeManagementEntities db = new RecipeManagementEntities();

        // GET: api/UserTbls
        public IQueryable<UserTbl> GetUserTbls()
        {
            return db.UserTbls.Where(x => x.DeletedDate == null);
        }

        // GET: api/UserTbls/5
        [ResponseType(typeof(UserTbl))]
        public async Task<IHttpActionResult> GetUserTbl(int id)
        {
            UserTbl userTbl = await db.UserTbls.FindAsync(id);
            if (userTbl == null)
            {
                return NotFound();
            }

            return Ok(userTbl);
        }

        // PUT: api/UserTbls/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserTbl(int id, UserTbl userTbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTbl.UserId)
            {
                return BadRequest();
            }

            db.Entry(userTbl).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserTbls
        [ResponseType(typeof(UserTbl))]
        public async Task<IHttpActionResult> PostUserTbl(UserTbl userTbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTbls.Add(userTbl);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userTbl.UserId }, userTbl);
        }

        // DELETE: api/UserTbls/5
        [ResponseType(typeof(UserTbl))]
        public async Task<IHttpActionResult> DeleteUserTbl(int id)
        {
            UserTbl userTbl = await db.UserTbls.FindAsync(id);
            if (userTbl == null)
            {
                return NotFound();
            }

            userTbl.DeletedDate = DateTime.UtcNow;

            //db.UserTbls.Remove(userTbl);
            db.Entry(userTbl).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTblExists(int id)
        {
            return db.UserTbls.Count(e => e.UserId == id) > 0;
        }
    }
}