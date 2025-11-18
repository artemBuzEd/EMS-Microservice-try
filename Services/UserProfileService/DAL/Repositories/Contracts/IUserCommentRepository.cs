using DAL.DTO;
using DAL.Entities;
using DAL.Entities.HelpModels;
using DAL.Helpers;

namespace DAL.Repositories.Contracts;

public interface IUserCommentRepository : IGenericRepository<UserComment>
{
    Task<IEnumerable<UserComment?>> GetAllByEventId(string id);
    Task<IEnumerable<UserComment?>> GetAllByUserId(string id);

    Task<UserInfoAndComment?> GetUserInfoByCommentId(int id);
    Task<PagedList<UserComment>> GetAllPaginatedAsync(UserCommentParameters parameters, ISortHelper<UserComment> sortHelper);
}