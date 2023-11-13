namespace EMS.Service.ViewModels.Role
{
    public record RoleUserViewModel
    {
        public Guid UserId { get; init; }
        public string? UserName { get; init; }
        public bool IsSelected { get; set; }
    }
}
