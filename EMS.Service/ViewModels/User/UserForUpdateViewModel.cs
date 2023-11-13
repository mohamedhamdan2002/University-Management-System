namespace EMS.Service.ViewModels.User
{
    public record UserForUpdateViewModel : UserForManipulationViewModel
    {
        public Guid UserId { get; init; }
        public List<string>? Roles { get; init; } 

        public List<string>? Claims { get; init; } 
    }
}
