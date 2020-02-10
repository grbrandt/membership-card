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
using Microsoft.Extensions.Logging;

namespace Membership.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Members")]
    public class MembersController : Controller
    {
        private readonly ILogger<MembersController> logger;
        private readonly MembershipRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembersController"/> class.
        /// </summary>
        /// <param name="repo">The repository</param>
        public MembersController(ILogger<MembersController> logger, 
            MembershipRepository repo)
        {
            this.logger = logger;
            this.repo = repo;
            this.logger.LogTrace("Created members controller");
        }

        // GET: api/Members
        /// <summary>
        /// Gets all registered members.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Member&gt;&gt;.</returns>
        [HttpGet]
        public async Task<IEnumerable<Person>> GetMembers()
        {           
            logger.LogInformation("Retrieving all members", this.HttpContext.User.Identity.Name);
            return await repo.GetMembers();
        }

        [HttpGet("Find/{username}")]
        public async Task<IActionResult> GetMember([FromRoute]string username)
        {
            var member = await repo.FindMember(username);
            if (member == null)
                return NotFound(username);

            return Ok(member);
        }

        // GET: api/Members/5
        /// <summary>
        /// Gets a member.
        /// </summary>
        /// <param name="id">The member id.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = await repo.GetMember(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpGet("{id}/card")]
        public async Task<IActionResult> GetCardForMember([FromRoute] int id)
        {
            var member = await repo.GetMember(id);
            if (member == null)
            {
                return NotFound();
            }
            
            return Ok(member);
        }

        // PUT: api/Members/5
        /// <summary>
        /// Updates a member.
        /// </summary>
        /// <param name="id">The member id.</param>
        /// <param name="member">The member model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember([FromRoute] int id, [FromBody] Person member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != member.Id)
            {
                return BadRequest();
            }

            repo.Update(member);

            try
            {
                await repo.SaveAllChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repo.MemberExists(id))
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

        // POST: api/Members
        /// <summary>
        /// Creates a new member.
        /// </summary>
        /// <param name="member">The member model to insert.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostMember([FromBody] Person member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Add(member);
            await repo.SaveAllChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        // DELETE: api/Members/5
        /// <summary>
        /// Deletes a member.
        /// </summary>
        /// <param name="id">The member id.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = await repo.GetMember(id);
            if (member == null)
            {
                return NotFound();
            }
            
            repo.Delete(member);

            await repo.SaveAllChangesAsync();
            logger.LogInformation("Deleted member", member);
            return Ok(member);
        }
    }
}