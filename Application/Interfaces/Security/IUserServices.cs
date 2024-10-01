using Core.DTOs.Security.User;

namespace Application.Interfaces.Security
{
    public interface IUserServices
    {
        TokenDto Login(LoginDto loginDto);
        Task<bool> RegisterUser(AddUserDto register);
        List<ConsultUserDto> GetAllUser();
        ConsultUserDto GetUser(int idUser);
        ConsultUserDto GetUserByEmail(string email);
        Task<bool> ResetPassword(ResetPasswordDto reset);
        Task<bool> UpdatePassword(UserPasswordDto password, int idUser);
        Task<bool> ActivateAndDeactivate(ActiveUserDto activeUser);
    }
}
