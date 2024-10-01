using Core.DTOs.Scaffolding.Theme;

namespace Application.Interfaces.Scaffolding
{
    public interface IConfigThemeServices
    {
        ConsultConfigThemeDto GetConfigTheme();

        Task<bool> SaveTheme(AddConfigThmeDto config);
    }
}
