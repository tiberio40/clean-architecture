using Core.DTOs.Scaffolding.Projets;

namespace Application.Interfaces.Scaffolding
{
    public interface INormalizeProjectNamesServices
    {
        List<NormalizeProjectNamesDto> Getall();
        Task<bool> Insert(AddNormalizeProjectNamesDto add);
        Task<bool> Update(NormalizeProjectNamesDto update);
        Task<bool> Delete(int id);
    }
}
