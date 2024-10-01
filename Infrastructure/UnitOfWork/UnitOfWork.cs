using Core.Entities.Campaign;
using Core.Entities.Scaffolding;
using Core.Entities.Securitty;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Attributes
        private readonly SqlDbContext _context;
        private bool disposed = false;
        #endregion Attributes

        #region builder
        public UnitOfWork(SqlDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Properties  
        private IRepository<PermissionEntity> permissionRepository;
        private IRepository<RolEntity> rolRepository;
        private IRepository<RolesPermissionsEntity> rolesPermissionsRepository;
        private IRepository<UserEntity> userRepository;
        private IRepository<RolMenusEntity> rolMenusRepository;
        private IRepository<MenuEntity> menuRepository;
        private IRepository<NormalizeProjectNamesEntity> normalizeProjectNamesRepository;
        private IRepository<ConfigThemeEntity> configThemeRepository;
        private IRepository<FileEntity> fileRepository;
        private IRepository<MetaTypeServiceEntity> metaTypeServiceRepository;
        private IRepository<MarketingStatusEntity> marketingStatusRepository;
        private IRepository<MarketingEntity> marketingRepository;
        private IRepository<MarketingUserEntity> marketingUserRepository;
        private IRepository<MarketingCampaignEntity> marketingCampaignRepository;
        private IRepository<MarketingTemplateEntity> marketingTemplateRepository;
        private IRepository<MetaConfigurationEntity> metaConfigurationRepository;
        private IRepository<RecurringTypeEntity> recurringTypeRepository;
        #endregion


        #region Members

        //Security
        public IRepository<PermissionEntity> PermissionRepository
        {
            get
            {
                if (this.permissionRepository == null)
                    this.permissionRepository = new Repository<PermissionEntity>(_context);

                return permissionRepository;
            }
        }

        public IRepository<RolEntity> RolRepository
        {
            get
            {
                if (this.rolRepository == null)
                    this.rolRepository = new Repository<RolEntity>(_context);

                return rolRepository;
            }
        }

        public IRepository<RolesPermissionsEntity> RolesPermissionsRepository
        {
            get
            {
                if (this.rolesPermissionsRepository == null)
                    this.rolesPermissionsRepository = new Repository<RolesPermissionsEntity>(_context);

                return rolesPermissionsRepository;
            }
        }

        public IRepository<UserEntity> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new Repository<UserEntity>(_context);

                return userRepository;
            }
        }

        public IRepository<MenuEntity> MenuRepository
        {
            get
            {
                if (this.menuRepository == null)
                    this.menuRepository = new Repository<MenuEntity>(_context);

                return menuRepository;
            }
        }

        public IRepository<RolMenusEntity> RolMenusRepository
        {
            get
            {
                if (this.rolMenusRepository == null)
                    this.rolMenusRepository = new Repository<RolMenusEntity>(_context);

                return rolMenusRepository;
            }
        }

        //Scaffolding
        public IRepository<NormalizeProjectNamesEntity> NormalizeProjectNamesRepository
        {
            get
            {
                if (this.normalizeProjectNamesRepository == null)
                    this.normalizeProjectNamesRepository = new Repository<NormalizeProjectNamesEntity>(_context);

                return normalizeProjectNamesRepository;
            }
        }

        public IRepository<ConfigThemeEntity> ConfigThemeRepository
        {
            get
            {
                if (this.configThemeRepository == null)
                    this.configThemeRepository = new Repository<ConfigThemeEntity>(_context);

                return configThemeRepository;
            }
        }

        public IRepository<FileEntity> FileRepository
        {
            get
            {
                if (this.fileRepository == null)
                    this.fileRepository = new Repository<FileEntity>(_context);

                return fileRepository;
            }
        }

        public IRepository<MetaTypeServiceEntity> MetaTypeServiceRepository
        {
            get
            {
                if (this.metaTypeServiceRepository == null)
                    this.metaTypeServiceRepository = new Repository<MetaTypeServiceEntity>(_context);

                return metaTypeServiceRepository;
            }
        }

        public IRepository<MarketingStatusEntity> MarketingStatusRepository
        {
            get
            {
                if (this.marketingStatusRepository == null)
                    this.marketingStatusRepository = new Repository<MarketingStatusEntity>(_context);

                return marketingStatusRepository;
            }
        }

        public IRepository<MarketingEntity> MarketingRepository
        {
            get
            {
                if (this.marketingRepository == null)
                    this.marketingRepository = new Repository<MarketingEntity>(_context);

                return marketingRepository;
            }
        }

        public IRepository<MarketingUserEntity> MarketingUserRepository
        {
            get
            {
                if (this.marketingUserRepository == null)
                    this.marketingUserRepository = new Repository<MarketingUserEntity>(_context);

                return marketingUserRepository;
            }
        }

        public IRepository<MarketingCampaignEntity> MarketingCampaignRepository
        {
            get
            {
                if (this.marketingCampaignRepository == null)
                    this.marketingCampaignRepository = new Repository<MarketingCampaignEntity>(_context);

                return marketingCampaignRepository;
            }
        }

        public IRepository<MarketingTemplateEntity> MarketingTemplateRepository
        {
            get
            {
                if (this.marketingTemplateRepository == null)
                    this.marketingTemplateRepository = new Repository<MarketingTemplateEntity>(_context);

                return marketingTemplateRepository;
            }
        }

        public IRepository<MetaConfigurationEntity> MetaConfigurationRepository
        {
            get
            {
                if (this.metaConfigurationRepository == null)
                    this.metaConfigurationRepository = new Repository<MetaConfigurationEntity>(_context);

                return metaConfigurationRepository;
            }
        }

        public IRepository<RecurringTypeEntity> RecurringTypeRepository
        {
            get
            {
                if (this.recurringTypeRepository == null)
                    this.recurringTypeRepository = new Repository<RecurringTypeEntity>(_context);

                return recurringTypeRepository;
            }
        }
        #endregion

        #region Base
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();
        #endregion
    }
}
