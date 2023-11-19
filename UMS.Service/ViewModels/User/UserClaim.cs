namespace UMS.Service.ViewModels.User
{
    public record UserClaim
    {
        public string? ClaimType { get; init; }
        public bool IsSelected { get; set; }
    }
}
