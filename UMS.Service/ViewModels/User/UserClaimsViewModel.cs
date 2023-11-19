namespace UMS.Service.ViewModels.User
{
    public record UserClaimsViewModel
    {
        public Guid UserId { get; init; }
        public List<UserClaim> Claims { get; init; } = new();
    }
}
