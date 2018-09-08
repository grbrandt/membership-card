using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Membership.Data;
using Membership.Data.Entities;

namespace Membership.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Clubs")]
    public class ClubsController : Controller
    {
        private readonly MembershipRepository repo;

        public ClubsController(MembershipRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Clubs
        /// <summary>
        /// Gets the clubs.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Club&gt;&gt;.</returns>
        [HttpGet]
        public async Task<IEnumerable<Club>> GetClubs()
        {
            return await repo.GetClubs();
        }

        // GET: api/Clubs/5
        /// <summary>
        /// Gets the club.
        /// </summary>
        /// <param name="id">The club id.</param>
        /// <param name="includeMembers">if set to <c>true</c> members belonging to the club will be inculded in the returned data.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClub([FromRoute] int id, [FromQuery]bool includeMembers = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var club = includeMembers ? await repo.GetClubWithMembers(id) : await repo.GetClub(id);

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

            repo.Update(club);

            try
            {
                await repo.SaveAllChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repo.ClubExists(id))
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
            repo.Add(club);
            await repo.SaveAllChangesAsync();

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

            var club = await repo.GetClub(id);
            if (!await repo.ClubExists(id))
            {
                return NotFound();
            }

            repo.Delete(club);
            await repo.SaveAllChangesAsync();

            return Ok(club);
        }
    }
}