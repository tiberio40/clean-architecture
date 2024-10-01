using Application.Interfaces.Security;
using Common.Exceptions;
using Common.Resources;
using Core.DTOs.Security.Rol;
using Core.Entities.Securitty;
using Infrastructure.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services.Security
{
    public class RolServices : IRolServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public RolServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        #endregion

        #region Methods

        public List<RolDto> GetAll()
        {
            IEnumerable<RolEntity> list = _unitOfWork.RolRepository.GetAll();

            List<RolDto> roles = list.Select(x => new RolDto()
            {
                IdRol = x.IdRol,
                Rol = x.Rol
            }).ToList();

            return roles;
        }

        public async Task<bool> Update(RolDto update)
        {
            ValidateByName(update);
            RolEntity role = GetRol(update.IdRol);
            role.Rol = update.Rol;

            _unitOfWork.RolRepository.Update(role);

            return await _unitOfWork.Save() > 0;
        }


        #region Privates
        private RolEntity GetRol(int id)
        {
            RolEntity entity = _unitOfWork.RolRepository.FirstOrDefault(x => x.IdRol == id);
            if (entity == null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Rol");
                throw new BusinessException(message);
            }

            return entity;
        }
        private void ValidateByName(RolDto update)
        {
            RolEntity entity = _unitOfWork.RolRepository.FirstOrDefault(x => x.Rol.ToLower() == update.Rol.ToLower()
                                                                          && x.IdRol != update.IdRol);
            if (entity != null)
            {
                string message = string.Format(GeneralMessages.ItemDuplicate, "Rol");
                throw new BusinessException(message);
            }
        }
        #endregion

        #endregion
    }
}
