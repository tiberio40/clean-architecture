using Application.Interfaces.Security;
using Common.Exceptions;
using Common.Resources;
using Core.DTOs.Security.Menus;
using Core.Entities.Securitty;
using Infrastructure.UnitOfWork.Interfaces;

namespace Application.Services.Security
{
    public class MenuServices : IMenuServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public MenuServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
       
        public List<MenuDto> GetAllMenuByRol(int idRol)
        {
            IEnumerable<MenuEntity> menus = _unitOfWork.MenuRepository.GetAll();
            IEnumerable<RolMenusEntity> rolMenus = _unitOfWork.RolMenusRepository.FindAll(x => x.IdRol == idRol);
            List<int> idMenus = rolMenus.Select(x => x.IdMenu).Distinct().ToList();

            List<MenuEntity> primaryMenus = menus.Where(menu => menu.IsPrincipal == true).ToList();

            List<MenuDto> result = primaryMenus.Select(x => new MenuDto()
            {
                Name = x.Name,
                Url = x.Url,
                Icon = x.Icon,
                IsPrincipal = x.IsPrincipal,
                IdMenu = x.Id,
                SubMenus = menus
                            .Where(sm => !sm.IsPrincipal && sm.IdMenuPrimary == x.Id)
                            .Select(s => new SubMenuDto
                            {
                                IdSubMenu = s.Id,
                                Name = s.Name,
                                Url = s.Url,
                                Icon = s.Icon,
                                IsAssigned = idMenus.Any(p => p == s.Id)
                            }).ToList(),
                IsAssigned = idMenus.Any(p => p == x.Id)
            }).ToList();

            return result;
        }

        public async Task<bool> UpdateMenuRol(RolMenuDto update)
        {
            bool result = true;

            using (var db = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await DeleteRolMenu(update.IdRol);

                    if (update.IdMenues.Any())
                        result = await InsertRolMenu(update);

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

        private async Task<bool> InsertRolMenu(RolMenuDto insert)
        {
            List<RolMenusEntity> newRoleMenu = insert.IdMenues.Select(x => new RolMenusEntity()
            {
                IdRol = insert.IdRol,
                IdMenu = x.IdMenu
            }).ToList();

            foreach (var item in insert.IdMenues)
            {
                if (item.IdSubMenus != null)
                {
                    foreach (var sub in item.IdSubMenus)
                    {
                        RolMenusEntity addSubMenu = new RolMenusEntity
                        {
                            IdRol = insert.IdRol,
                            IdMenu = sub
                        };

                        newRoleMenu.Add(addSubMenu);
                    }
                }
            }
            _unitOfWork.RolMenusRepository.Insert(newRoleMenu);

            return await _unitOfWork.Save() > 0;
        }

        private async Task DeleteRolMenu(int idRol)
        {
            IEnumerable<RolMenusEntity> listRolMenues = _unitOfWork.RolMenusRepository.FindAll(x => x.IdRol == idRol);

            if (listRolMenues.Any())
            {
                _unitOfWork.RolMenusRepository.Delete(listRolMenues);
                await _unitOfWork.Save();
            }
        }
        #endregion
    }
}
