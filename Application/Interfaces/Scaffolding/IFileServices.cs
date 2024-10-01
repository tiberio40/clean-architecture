using Core.DTOs.Scaffolding.File;
using Core.Entities.Scaffolding;

namespace Application.Interfaces.Scaffolding
{
    public interface IFileServices
    {
        Task<bool> Delete(int idFile);
        string GetUrlFile(FileEntity? fileEntity);
        Task<FileEntity> InsertFile(AddFileDto add);
        Task<FileEntity> UpdateFile(UpdateFileDto update);
        void DeleteFilePath(string filePath);
    }
}
