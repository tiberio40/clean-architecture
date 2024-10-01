using Application.Interfaces.Scaffolding;
using Common.Exceptions;
using Common.Resources;
using Core.DTOs.Scaffolding.Projets;
using Core.Entities.Scaffolding;
using Infrastructure.UnitOfWork.Interfaces;

namespace Application.Services.Scaffolding
{
    public class NormalizeProjectNamesServices : INormalizeProjectNamesServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public NormalizeProjectNamesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public List<NormalizeProjectNamesDto> Getall()
        {
            IEnumerable<NormalizeProjectNamesEntity> list = _unitOfWork.NormalizeProjectNamesRepository.GetAll();
            List<NormalizeProjectNamesDto> result = list.Select(x => new NormalizeProjectNamesDto()
            {
                Id = x.Id,
                NameNetSuite = x.NameNetSuite,
                NameTMetric = x.NameTMetric
            }).ToList();

            return result;
        }

        public async Task<bool> Insert(AddNormalizeProjectNamesDto add)
        {
            ValidateNameNetSuite(add.NameNetSuite);
            ValidateNameTMetric(add.NameTMetric);

            NormalizeProjectNamesEntity entity = new NormalizeProjectNamesEntity()
            {
                NameNetSuite = add.NameNetSuite,
                NameTMetric = add.NameTMetric,
            };
            _unitOfWork.NormalizeProjectNamesRepository.Insert(entity);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Update(NormalizeProjectNamesDto update)
        {
            ValidateNameNetSuite(update.Id, update.NameNetSuite);
            ValidateNameTMetric(update.Id, update.NameTMetric);

            NormalizeProjectNamesEntity entity = GetById(update.Id);
            entity.NameNetSuite = update.NameNetSuite;
            entity.NameTMetric = update.NameTMetric;
            _unitOfWork.NormalizeProjectNamesRepository.Update(entity);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            NormalizeProjectNamesEntity entity = GetById(id);
            _unitOfWork.NormalizeProjectNamesRepository.Delete(entity);

            return await _unitOfWork.Save() > 0;
        }
        #endregion

        #region privates
        private NormalizeProjectNamesEntity GetById(int id)
        {
            NormalizeProjectNamesEntity entity = _unitOfWork.NormalizeProjectNamesRepository.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Proyecto de normalziación");
                throw new BusinessException(message);
            }

            return entity;
        }

        private void ValidateNameNetSuite(int id, string nameNetSuite)
        {
            NormalizeProjectNamesEntity entity = _unitOfWork.NormalizeProjectNamesRepository
                                                .FirstOrDefault(x => x.NameNetSuite.ToLower() == nameNetSuite.ToLower()
                                                                  && x.Id != id);
            if (entity != null)
            {
                string message = string.Format(GeneralMessages.ExistingProjectName, $"NetSuite: {entity.NameNetSuite}", $"TMetric: {entity.NameTMetric}");
                throw new BusinessException(message);
            }
        }

        private void ValidateNameNetSuite(string nameNetSuite)
        {
            NormalizeProjectNamesEntity entity = _unitOfWork.NormalizeProjectNamesRepository
                                                .FirstOrDefault(x => x.NameNetSuite.ToLower() == nameNetSuite.ToLower());
            if (entity != null)
            {
                string message = string.Format(GeneralMessages.ExistingProjectName, $"NetSuite: {entity.NameNetSuite}", $"TMetric: {entity.NameTMetric}");
                throw new BusinessException(message);
            }
        }

        private void ValidateNameTMetric(int id, string nameTMetric)
        {
            NormalizeProjectNamesEntity entity = _unitOfWork.NormalizeProjectNamesRepository
                                                .FirstOrDefault(x => x.NameTMetric.ToLower() == nameTMetric.ToLower()
                                                                  && x.Id != id);
            if (entity != null)
            {
                string message = string.Format(GeneralMessages.ExistingProjectName, $"TMetric: {entity.NameTMetric}", $"NetSuite: {entity.NameNetSuite}");
                throw new BusinessException(message);
            }
        }

        private void ValidateNameTMetric(string nameTMetric)
        {
            NormalizeProjectNamesEntity entity = _unitOfWork.NormalizeProjectNamesRepository
                                                .FirstOrDefault(x => x.NameTMetric.ToLower() == nameTMetric.ToLower());
            if (entity != null)
            {
                string message = string.Format(GeneralMessages.ExistingProjectName, $"TMetric: {entity.NameTMetric}", $"NetSuite: {entity.NameNetSuite}");
                throw new BusinessException(message);
            }
        }
        #endregion
    }
}
