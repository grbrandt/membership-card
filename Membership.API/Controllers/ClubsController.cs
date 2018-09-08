using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Membership.API.Models;
using Membership.Data;
using Membership.Data.Entities;

namespace Membership.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Clubs")]
    public class ClubsController : Controller
    {
        private readonly MembershipContext context;
        private readonly MembershipRepository repo;

        public ClubsController(MembershipContext context, MembershipRepository repo)
        {
            this.context = context;
            this.repo = repo;
        }

        // GET: api/Clubs
        [HttpGet]
        public IEnumerable<Club> GetClubs()
        {
            return context.Clubs;
        }

        // GET: api/Clubs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var club = await repo.GetClubWithMembers(id);

            if (club == null)
            {
                return NotFound();
            }

            return Ok(club);
        }

        // PUT: api/Clubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub([FromRoute] int id, [FromBody] Club club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != club.Id)
            {
                return BadRequest();
            }

            context.Entry(club).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clubs
        [HttpPost]
        public async Task<IActionResult> PostClub([FromBody] Club club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Clubs.Add(club);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetClub", new { id = club.Id }, club);
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var club = await context.Clubs.SingleOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            context.Clubs.Remove(club);
            await context.SaveChangesAsync();

            return Ok(club);
        }

        private bool ClubExists(int id)
        {
            return context.Clubs.Any(e => e.Id == id);
        }
    }
}