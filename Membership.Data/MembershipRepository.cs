﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Membership.API.Models;
using Membership.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace Membership.Data
{
    public class MembershipRepository
    {
        private readonly MembershipContext context;

        public MembershipRepository(MembershipContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds the specified entity to the context. It will be added to
        /// the database when SaveAllChanges is called.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            context.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry(entity).State = EntityState.Modified;

        }

        /// <summary>
        /// Deletes the specified entity from the context. It will be deleted
        /// from the database when SaveAllChanges() is called.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            //if(typeof(TEntity).IsAssignableFrom(ISoftDeleteable)
            //if(context.Entry(entity).Properties.Any(p=>p.)
            context.Remove(entity);
        }

        /// <summary>
        /// Saves all changes.
        /// </summary>
        /// <returns>System.Boolean indicating if any rows were affected.</returns>
        public bool SaveAllChanges()
        {
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// save all changes as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt; indicating if any rows were affected.</returns>
        public async Task<bool> SaveAllChangesAsync()
        {
            return (await context.SaveChangesAsync() > 0);
        }

        /// <summary>
        /// Gets the clubs.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Club&gt;&gt;.</returns>
        public async Task<IEnumerable<Club>> GetClubs()
        {
            return await context.Clubs
                .ToListAsync();
        }

        /// <summary>
        /// Gets the club including the members.
        /// </summary>
        /// <param name="clubId">The club identifier.</param>
        /// <returns>Task&lt;Club&gt;.</returns>
        public async Task<Club> GetClubWithMembers(int clubId)
        {
            return await context.Clubs
                .Include(c => c.Members)
                .Where(c => c.Id == clubId)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Gets a member.
        /// </summary>
        /// <param name="id">The member id.</param>
        /// <returns>Task&lt;Member&gt;.</returns>
        public async Task<Member> GetMember(int id)
        {
            return await context.Members
                .SingleOrDefaultAsync(m => m.Id == id);
        }
        /// <summary>
        /// Gets all members.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Member&gt;&gt;.</returns>
        public async Task<IEnumerable<Member>> GetMembers()
        {
            return await context.Members.ToListAsync();
        }

        /// <summary>
        /// Gets the members belonging to the specified club.
        /// </summary>
        /// <param name="clubId">The club id.</param>
        /// <returns>Task&lt;IEnumerable&lt;Member&gt;&gt;.</returns>
        public async Task<IEnumerable<Member>> GetMembersInClub(int clubId)
        {
            return await context.Members
                .Where(member => member.ClubId == clubId)
                .ToListAsync();
        }

        /// <summary>
        /// Determines if a member with the specified ID exists in the database.
        /// </summary>
        /// <param name="memberId">The member identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> MemberExists(int memberId)
        {
            return await context.Members.AnyAsync(m => m.Id == memberId);
        }
    }
}
