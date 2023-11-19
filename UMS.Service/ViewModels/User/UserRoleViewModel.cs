namespace UMS.Service.ViewModels.User
{
    public record UserRoleViewModel
    {
        public Guid RoleId { get; init; }
        public string? RoleName { get; init; }
        public bool IsSelected { get; set; }
    }
}
