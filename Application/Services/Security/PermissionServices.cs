using Application.Interfaces.Security;
using Common.Enums;
using Common.Exceptions;
using Common.Resources;
using Core.DTOs.Security.Permission;
using Core.Entities.Securitty;
using Infrastructure.UnitOfWork.Interfaces;

namespace Application.Services.Security
{
    public class PermissionServices : IPermissionServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public PermissionServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        public List<PermissionDto> GetAllPermission()
        {
            IEnumerable<PermissionEntity> permissions = GetAll();
            List<PermissionDto> result = permissions.Select(p => new PermissionDto
            {
                IdPermission = p.IdPermission,
                Permission = p.Permission,
                Description = p.Description,
                Ambit = p.Ambit,
            }).ToList();

            return result;
        }

        public List<PermissionRolDto> GetAllPermissionsByRol(int idRol)
        {
            IEnumerable<PermissionEntity> permissions = GetAll();
            IEnumerable<RolesPermissionsEntity> rolPermission = _unitOfWork.RolesPermissionsRepository.FindAll(x => x.IdRol == idRol);
            if (!rolPermission.Any())
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Rol");
                throw new BusinessException(message);
            }
            List<int> idPermissions = rolPermission.Select(x => x.IdPermission).Distinct().ToList();

            List<PermissionRolDto> result = permissions.Select(x => new PermissionRolDto()
            {
                IdRol = idRol,
                Ambit = x.Ambit,
                IdPermission = x.IdPermission,
                Description = x.Description,
                Permission = x.Permission,
                IsAssigned = idPermissions.Any(p => p == x.IdPermission),
            }).ToList();

            return result;
        }

        public bool ValidatePermissionByUser(Enums.Permission permission, int idUser, int idRol)
        {
            bool result = false;
            UserEntity user = _unitOfWork.UserRepository.FirstOrDefault(u => u.IdUser == idUser && u.IdRol == idRol,
                                                                        r => r.RolEntity.RolesPermissionsEntities);
            if (user != null)
                result = user.RolEntity.RolesPermissionsEntities.Any(p => p.IdPermission == permission.GetHashCode());

            return result;
        }

        public async Task<bool> UpdatePermission(UpdatePermissionDto update)
        {
            bool result = true;
            using (var db = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await DeletePermission(update.IdRol);

                    if (!update.IdPermissions.Any())
                        result = await InsertPermission(update);

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

        #region Privates
        private IEnumerable<PermissionEntity> GetAll() => _unitOfWork.PermissionRepository.GetAll();
        private async Task<bool> InsertPermission(UpdatePermissionDto update)
        {
            var newRolPermission = update.IdPermissions.Select(x => new RolesPermissionsEntity()
            {
                IdRol = update.IdRol,
                IdPermission = x
            }).ToList();
            _unitOfWork.RolesPermissionsRepository.Insert(newRolPermission);

            return await _unitOfWork.Save() > 0;
        }

        private async Task DeletePermission(int idRol)
        {
            IEnumerable<RolesPermissionsEntity> listRolPermissions = _unitOfWork.RolesPermissionsRepository
                                                                    .FindAll(x => x.IdRol == idRol);

            if (listRolPermissions.Any())
            {
                _unitOfWork.RolesPermissionsRepository.Delete(listRolPermissions);
                await _unitOfWork.Save();
            }
        }
        #endregion

        #endregion
    }
}
