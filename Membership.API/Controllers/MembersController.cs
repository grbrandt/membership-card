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
    [Route("api/Members")]
    public class MembersController : Controller
    {
        private readonly MembershipRepository repo;

        public MembersController(MembershipRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<IEnumerable<Member>> GetMembers()
        {
            return await repo.GetMembers();
        }


        // GET: api/Members/5
        [HttpGet("{id}", Name = Lib.Routes.AddMember)]
        //[Route("api/members")]
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

        // PUT: api/Members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember([FromRoute] int id, [FromBody] Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != member.Id)
            {
                return BadRequest();
            }
            //_context.Entry(member).State = EntityState.Modified;
            repo.Update(member);

            try
            {
                //await _context.SaveChangesAsync();
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
        [HttpPost]
        public async Task<IActionResult> PostMember([FromBody] Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Members.Add(member);
            //await _context.SaveChangesAsync();
            
            repo.Add(member);
            await repo.SaveAllChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //var member = await _context.Members.SingleOrDefaultAsync(m => m.Id == id);
            //if (member == null)
            //{
            //    return NotFound();
            //}

            //_context.Members.Remove(member);
            //await _context.SaveChangesAsync();
            //TODO: Implement
            //return Ok(member);
            return NotFound();
        }
    }
}