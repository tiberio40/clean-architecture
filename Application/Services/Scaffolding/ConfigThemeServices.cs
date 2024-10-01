using Application.Interfaces.Scaffolding;
using Common.Enums;
using Common.Exceptions;
using Common.Resources;
using Core.DTOs.Scaffolding.File;
using Core.DTOs.Scaffolding.Theme;
using Core.Entities.Scaffolding;
using Infrastructure.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Scaffolding
{
    public class ConfigThemeServices : IConfigThemeServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileServices _fileServices;
        #endregion

        #region Builder
        public ConfigThemeServices(IUnitOfWork unitOfWork, IFileServices fileServices)
        {
            _unitOfWork = unitOfWork;
            _fileServices = fileServices;
        }
        #endregion

        #region Methods
        public ConsultConfigThemeDto GetConfigTheme()
        {
            ConfigThemeEntity theme = GetThemeEntity();

            ConsultConfigThemeDto config = new ConsultConfigThemeDto();
            if (theme != null)
            {
                config.ColorButtonsAction = theme.ColorButtonsAction;
                config.ColorButtonCancel = theme.ColorButtonCancel;
                config.ColorButtonCreate = theme.ColorButtonCreate;
                config.ColorButtonText = theme.ColorButtonText;
                config.ColorTitle = theme.ColorTitle;
                config.ColorSubtitle = theme.ColorSubtitle;
                config.ColorText = theme.ColorText;
                config.ColorTextMenu = theme.ColorTextMenu;
                config.ColorBackgroundMenu = theme.ColorBackgroundMenu;
                config.ColorHeaderTable = theme.ColorHeaderTable;
                config.ColorTextColumn = theme.ColorTextColumn;
                config.TypeTitle = theme.TypeTitle;
                config.TypeSubtitle = theme.TypeSubtitle;
                config.TypeParagraph = theme.TypeParagraph;
                config.StyleLetter = theme.StyleLetter;
                config.CompanyName = theme.CompanyName;
                config.UrllLogo = _fileServices.GetUrlFile(theme?.FileEntity);
            }

            return config;
        }

        public async Task<bool> SaveTheme(AddConfigThmeDto config)
        {
            bool result = false;
            var theme = GetThemeEntity();
            if (theme != null)
            {
                theme.ColorButtonsAction = config.ColorButtonsAction;
                theme.ColorButtonCancel = config.ColorButtonCancel;
                theme.ColorButtonCreate = config.ColorButtonCreate;
                theme.ColorButtonText = config.ColorButtonText;
                theme.ColorTitle = config.ColorTitle;
                theme.ColorSubtitle = config.ColorSubtitle;
                theme.ColorText = config.ColorText;
                theme.ColorTextMenu = config.ColorTextMenu;
                theme.ColorBackgroundMenu = config.ColorBackgroundMenu;
                theme.ColorHeaderTable = config.ColorHeaderTable;
                theme.ColorTextColumn = config.ColorTextColumn;
                theme.TypeTitle = config.TypeTitle;
                theme.TypeSubtitle = config.TypeSubtitle;
                theme.TypeParagraph = config.TypeParagraph;
                theme.StyleLetter = config.StyleLetter;
                theme.CompanyName = config.CompanyName;
                result = await Save(theme, config.ImgLogo);
            }
            else
            {
                ConfigThemeEntity themeEntity = new ConfigThemeEntity()
                {
                    ColorButtonsAction = config.ColorButtonsAction,
                    ColorButtonCancel = config.ColorButtonCancel,
                    ColorButtonCreate = config.ColorButtonCreate,
                    ColorButtonText = config.ColorButtonText,
                    ColorTitle = config.ColorTitle,
                    ColorSubtitle = config.ColorSubtitle,
                    ColorText = config.ColorText,
                    ColorTextMenu = config.ColorTextMenu,
                    ColorBackgroundMenu = config.ColorBackgroundMenu,
                    ColorHeaderTable = config.ColorHeaderTable,
                    ColorTextColumn = config.ColorTextColumn,
                    TypeTitle = config.TypeTitle,
                    TypeSubtitle = config.TypeSubtitle,
                    TypeParagraph = config.TypeParagraph,
                    StyleLetter = config.StyleLetter,
                    CompanyName = config.CompanyName,
                    Id = 0
                };
                result = await Save(themeEntity, config.ImgLogo);
            }

            return result;
        }
        #endregion


        private async Task<bool> Save(ConfigThemeEntity entity, IFormFile? file)
        {
            bool result = false;
            using (var db = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    int idFile = 0;

                    if (file != null)
                    {
                        var fileEntity = entity.IdFile == null
                            ? await _fileServices.InsertFile(new AddFileDto
                            {
                                File = file,
                                TypeFile = Enums.TypeFile.Image
                            })
                            : await _fileServices.UpdateFile(new UpdateFileDto
                            {
                                File = file,
                                TypeFile = Enums.TypeFile.Image,
                                IdFile = (int)entity.IdFile
                            });

                        idFile = fileEntity.Id;
                    }

                    entity.IdFile = idFile == 0 ? null : idFile;
                    if (entity.Id == 0)
                        result = await Inser(entity);
                    else
                        result = await Update(entity);

                    if (result)
                        await db.CommitAsync();
                    else
                        await db.RollbackAsync();


                }
                catch (BusinessException ex)
                {
                    await db.RollbackAsync();

                    throw ex;
                }
                catch (Exception ex)
                {
                    await db.RollbackAsync();
                    throw new Exception(GeneralMessages.Error500, ex);
                }
            }

            return result;
        }

        private async Task<bool> Inser(ConfigThemeEntity entity)
        {
            _unitOfWork.ConfigThemeRepository.Insert(entity);

            return await _unitOfWork.Save() > 0;
        }

        private async Task<bool> Update(ConfigThemeEntity entity)
        {
            _unitOfWork.ConfigThemeRepository.Update(entity);

            return await _unitOfWork.Save() > 0;
        }
        public ConfigThemeEntity GetThemeEntity() => _unitOfWork.ConfigThemeRepository.FirstOrDefault(x => x.Id != 0, f => f.FileEntity);
    }
}
