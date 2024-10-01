using Core.Entities.Campaign;
using Core.Entities.Scaffolding;
using Core.Entities.Securitty;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<PermissionEntity> PermissionRepository { get; }
        IRepository<RolEntity> RolRepository { get; }
        IRepository<RolesPermissionsEntity> RolesPermissionsRepository { get; }
        IRepository<UserEntity> UserRepository { get; }

        IRepository<MenuEntity> MenuRepository { get; }
        IRepository<RolMenusEntity> RolMenusRepository { get; }
        IRepository<NormalizeProjectNamesEntity> NormalizeProjectNamesRepository { get; }
        IRepository<ConfigThemeEntity> ConfigThemeRepository { get; }
        IRepository<FileEntity> FileRepository { get; }
        IRepository<MetaTypeServiceEntity> MetaTypeServiceRepository { get; }
        IRepository<MarketingStatusEntity> MarketingStatusRepository { get; }
        IRepository<MarketingEntity> MarketingRepository { get; }
        IRepository<MarketingUserEntity> MarketingUserRepository { get; }
        IRepository<MarketingCampaignEntity> MarketingCampaignRepository { get; }
        IRepository<MarketingTemplateEntity> MarketingTemplateRepository { get; }
        IRepository<MetaConfigurationEntity> MetaConfigurationRepository { get; }
        IRepository<RecurringTypeEntity> RecurringTypeRepository { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> Save();
    }
}
