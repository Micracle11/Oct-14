using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls.WebParts;
using Oct_14.Models;

namespace Oct_14.Controllers
{
    public class TablesController : ApiController
    {
        private tempdbEntities db = new tempdbEntities();

        // GET: api/Tables
        public IQueryable<Table> GetTable()
        {
            return db.Table;
        }

        // GET: api/Tables/5
        [ResponseType(typeof(Table))]
        public IHttpActionResult GetTable(int id)
        {
            Table table = db.Table.Find(id);
            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

        // PUT: api/Tables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTable(int id, Table table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table.Id)
            {
                return BadRequest();
            }

            db.Entry(table).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExists(id))
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

        // POST: api/Tables
        [ResponseType(typeof(Table))]
        public HttpResponseMessage PostTable(Table table)
        {
            if (!ModelState.IsValid)
            {
                var response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception("Invalid Model"));
                return response;
            }

            db.Table.Add(table);
            db.SaveChanges();
            var okresponse = Request.CreateResponse<Table>(HttpStatusCode.OK, table);
            return okresponse;        
        }

        // DELETE: api/Tables/5
        [ResponseType(typeof(Table))]
        public IHttpActionResult DeleteTable(int id)
        {
            Table table = db.Table.Find(id);
            if (table == null)
            {
                return NotFound();
            }

            db.Table.Remove(table);
            db.SaveChanges();

            return Ok(table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TableExists(int id)
        {
            return db.Table.Count(e => e.Id == id) > 0;
        }
    }
}