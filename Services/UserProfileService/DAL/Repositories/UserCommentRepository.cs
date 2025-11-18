using DAL.DTO;
using DAL.Entities;
using DAL.Entities.HelpModels;
using DAL.EntityConfig;
using DAL.Helpers;
using DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserCommentRepository : GenericRepository<UserComment>, IUserCommentRepository 
{
    public UserCommentRepository(UserProfileDbContext dbContext) : base(dbContext)
    {
        
    }
    
    public async Task<IEnumerable<UserComment?>> GetAllByEventId(string id)
    {
        return await _context.UserComments.AsNoTracking().Where(u => u.event_id == id).ToListAsync();
    }

    public async Task<IEnumerable<UserComment?>> GetAllByUserId(string id)
    {
        return await _context.UserComments.AsNoTracking().Where(u => u.user_id == id).ToListAsync();
    }
    
    public override async Task UpdateAsync(UserComment entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity) + "Can't be null. [UserProfileGenericRepository.UpdateAsync()]");
        }

        entity.is_changed = true;
        await Task.Run(() => _table.Update(entity));
    }

    public Task<UserInfoAndComment?> GetUserInfoByCommentId(int commentId)
    {
        var comment = _context.UserComments
            .AsNoTracking()
            .Where(c => c.id == commentId)
            .Join(
                _context.UserProfiles,
                c => c.user_id,
                up => up.user_id,
                (c, up) => new UserInfoAndComment
                {
                    UserId = up.user_id,
                    FirstName = up.first_name,
                    LastName = up.last_name,
                    Comment = c.comment
                })
            .FirstOrDefaultAsync();

        return comment;
    }

    public async Task<PagedList<UserComment>> GetAllPaginatedAsync(UserCommentParameters parameters,
        ISortHelper<UserComment> sortHelper)
    {
        var query = _table.AsQueryable();
        
        if(!string.IsNullOrEmpty(parameters.user_id))
            query = query.Where(u => u.user_id.Contains(parameters.user_id));
        if(!string.IsNullOrEmpty(parameters.event_id))
            query = query.Where(u => u.event_id == parameters.event_id);
        if(parameters.rating.HasValue)
            query = query.Where(u => u.rating == parameters.rating);
        
        query = sortHelper.UseSort(query, parameters.OrderBy);
        
        return await PagedList<UserComment>.ToPagedListAsync(query.AsNoTracking(), parameters.PageNumber, parameters.PageSize);
    }
}