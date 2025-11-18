using DAL.UoW;
using Grpc.Core;
using UserProfileApi.Protos;

namespace WebApplication1.GrpcService;

public class UserProfileGrpcService : UserProfile.UserProfileBase
{
    private readonly ILogger<UserProfileGrpcService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UserProfileGrpcService(ILogger<UserProfileGrpcService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"gRPC GetUser request for user id : {request.UserId}");
        
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(request.UserId);
        
        if(userProfile is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"User with id {request.UserId} does not exist."));

        return new UserResponse
        {
            UserId = userProfile.user_id,
            FirstName = userProfile.first_name,
            LastName = userProfile.last_name,
            Bio = userProfile.bio,
        };
    }
}