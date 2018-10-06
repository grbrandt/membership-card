using System;
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
    /// <summary>
    /// Class MembershipRepository controls all data access through the repository pattern.
    /// </summary>
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

        /// <summary>
        /// Marks the specified entity as modified.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Person> FindMember(string username)
        {
            return await context.Members
                .FirstOrDefaultAsync(m => m.Name.StartsWith(username));
        }

        /// <summary>
        /// Deletes the specified entity from the context. It will be deleted
        /// from the database when SaveAllChanges() is called.
        /// If the entity implements <see cref="ISoftDeleteable"/> it will simply
        /// update the <see cref="ISoftDeleteable.IsDeleted"/> property and not
        /// physically remove the instance from the underlying data source. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (typeof(ISoftDeleteable).IsAssignableFrom(typeof(TEntity)))
            {
                ((ISoftDeleteable)entity).IsDeleted = true;
                context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                context.Remove(entity);
            }
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
                .Include(c => c.Memberships)
                .Where(c => c.Id == clubId)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Gets a member.
        /// </summary>
        /// <param name="id">The member id.</param>
        /// <returns>Task&lt;Member&gt;.</returns>
        public async Task<Person> GetMember(int id)
        {
            return await context.Members
                .SingleOrDefaultAsync(m => m.Id == id);
        }
        /// <summary>
        /// Gets all members.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Member&gt;&gt;.</returns>
        public async Task<IEnumerable<Person>> GetMembers()
        {
            return await context.Members.ToListAsync();
        }

        /// <summary>
        /// Gets the members belonging to the specified club.
        /// </summary>
        /// <param name="clubId">The club id.</param>
        /// <returns>Task&lt;IEnumerable&lt;Member&gt;&gt;.</returns>
        public async Task<IEnumerable<Person>> GetMembersInClub(int clubId)
        {
            var club = await context.Clubs
                .Include(m => m.Memberships)
                .ThenInclude(m => m.Member)
                .Where(m => m.Id == clubId)
                .SingleOrDefaultAsync();

            return club?.Memberships?.Select(t => t.Member);
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

        /// <summary>
        /// Gets the club.
        /// </summary>
        /// <param name="id">The club id.</param>
        /// <returns>Task&lt;System.Object&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Club> GetClub(int id)
        {
            return await context.Clubs.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Determines if a club with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> ClubExists(int id)
        {
            return await context.Clubs.AnyAsync(c => c.Id == id);
        }

        #region Registration

        /// <summary>
        /// Generates a validation token.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="club">The club.</param>
        /// <param name="daysValid">The number of days the token should remain valid.</param>
        /// <returns>RegistrationToken.</returns>
        public RegistrationToken GenerateValidationToken(Person member, Club club,
            int daysValid = 365)
        {
            if (member == null || club == null)
                return null;

            var vals = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            int codeLength = 6;
            var ran = new Random();
            var tokenbuilder = new StringBuilder();
            for (int i = 0; i < codeLength; i++)
            {
                tokenbuilder.Append(vals[ran.Next(0, vals.Length - 1)]);
            }

            var token = new RegistrationToken
            {
                Member = member,
                Club = club,
                IsValid = true,
                Expires = DateTime.Now.AddDays(daysValid)
            };
            return token;
        }

        /// <summary>
        /// Validates a token against a member name. Returns 
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;TokenValidationResult&gt;.</returns>
        public async Task<TokenValidationResult> ValidateToken(Person member, string token)
        {
            var result = await context.RegistrationTokens
                .FirstOrDefaultAsync(c => c.Member == member && c.TokenValue == token);

            if (result == null)
                return new InvalidTokenResult();

            if (result.Expires <= DateTime.Now)
                return new ExpiredTokenResult();

            var validationResult = new TokenValidationResult();
            validationResult.Club = result.Club.Name;
            validationResult.Valid = true;

            return validationResult;
        }
        #endregion

    }

    public class ExpiredTokenResult : TokenValidationResult
    {
        public ExpiredTokenResult()
        {
            Valid = false;
        }
    }

    public class InvalidTokenResult : TokenValidationResult
    {
        public InvalidTokenResult()
        {
            Valid = false;
        }
    }

    public class TokenValidationResult
    {
        public string Club { get; set; }
        public bool Valid { get; set; }
    }
}
