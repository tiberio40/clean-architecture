using Application.Interfaces.Scaffolding;
using Common.Exceptions;
using Common.Helpers;
using Common.Resources;
using Core.DTOs.Scaffolding.File;
using Core.Entities.Scaffolding;
using Infrastructure.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using static Common.Enums.Enums;

namespace Application.Services.Scaffolding
{
    public class FileServices : IFileServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _environment;
        #endregion

        #region Builder
        public FileServices(IUnitOfWork unitOfWork, IConfiguration config, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _environment = environment;
        }
        #endregion

        #region Methods
        public string GetUrlFile(FileEntity? fileEntity)
        {
            string pathUrl = string.Empty;
            if (fileEntity != null)
            {
                string server = _config["PathFiles:PathServer"];
                pathUrl = Path.Combine(server, fileEntity.UrlFile);
            }

            return pathUrl;
        }

        public async Task<FileEntity> InsertFile(AddFileDto add)
        {
            FileEntity file = new FileEntity()
            {
                UrlFile = UploadFile(add.File, add.TypeFile),
                CreationDate = DateTime.Now,
                TypeFile = (int)add.TypeFile
            };
            _unitOfWork.FileRepository.Insert(file);
            await _unitOfWork.Save();

            return file;
        }

        public async Task<FileEntity> UpdateFile(UpdateFileDto update)
        {
            FileEntity file = Get(update.IdFile);
            string newUrl = UploadFile(update.File, update.TypeFile);
            DeleteFilePath(file.UrlFile);
            file.UrlFile = newUrl;
            file.TypeFile = (int)update.TypeFile;

            _unitOfWork.FileRepository.Update(file);
            await _unitOfWork.Save();

            return file;
        }

        public async Task<bool> Delete(int idFile)
        {
            FileEntity entity = Get(idFile);
            _unitOfWork.FileRepository.Delete(entity);
            bool result = await _unitOfWork.Save() > 0;
            if (result)
                DeleteFilePath(entity.UrlFile);

            return result;
        }

        public void DeleteFilePath(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string fullPath = Path.Combine(_environment.WebRootPath, filePath);
                Helper.DeleteFile(fullPath);
            }

        }


        #region privates

        private FileEntity Get(int idFile)
        {
            FileEntity entity = _unitOfWork.FileRepository.FirstOrDefault(x => x.Id == idFile);
            if (entity == null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Archivo");
                throw new BusinessException(message);
            }

            return entity;
        }


        private string UploadFile(IFormFile add, TypeFile typeFile)
        {
            string url = string.Empty;
            var configFile = _config.GetSection("PathFiles");
            int size = Convert.ToInt32(configFile.GetSection("SizeFile").Value);
            long maxBytes = size * 1024 * 1024;
            if (add.Length > maxBytes)
            {
                string message = string.Format(GeneralMessages.MaxSizeFile, size);
                throw new BusinessException(message);
            }

            //Comprobar que el archivo sea imagen o documento
            ValidExtension(add, typeFile);

            if (typeFile == TypeFile.Image)
                url = configFile.GetSection("PathImages").Value;
            else
                url = configFile.GetSection("PathPdfWord").Value;

            string rootPath = _environment.ContentRootPath;
            string uploads = Path.Combine(rootPath, "wwwroot", url);
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            string uniqueFileName = GetUniqueFileName(add.FileName);
            string pathFinal = $"{uploads}/{uniqueFileName}";

            using (var stream = new FileStream(pathFinal, FileMode.Create))
            {
                add.CopyTo(stream);
            }

            return $"{url}/{uniqueFileName}";
        }

        private void ValidExtension(IFormFile add, TypeFile typeFile)
        {
            string currentExtension = Path.GetExtension(add.FileName);
            bool result = false;
            string allowedExtensions = string.Empty;

            if (typeFile == TypeFile.Image)
            {
                allowedExtensions = _config["PathFiles:AllowedImgExtensions"];
                string[] validExtensions = allowedExtensions.Split(",");
                result = validExtensions.Any(extension => currentExtension.Equals(extension, StringComparison.OrdinalIgnoreCase));
            }
            else if (typeFile == TypeFile.PdfWord)
            {
                allowedExtensions = _config["PathFiles:AlloweDocumentExtensions"];
                string[] validExtensions = allowedExtensions.Split(",");
                result = validExtensions.Any(extension => currentExtension.Equals(extension, StringComparison.OrdinalIgnoreCase));
            }

            if (!result)
            {
                string denegade = string.Format(GeneralMessages.DenegadeFileExtension,
                                                Path.GetFileName(add.FileName),
                                                allowedExtensions);
                throw new BusinessException(denegade);
            }
        }

        private static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
        }
        #endregion

        #endregion
    }
}
