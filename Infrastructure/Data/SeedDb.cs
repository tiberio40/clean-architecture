using Common.Enums;
using Core.Entities.Campaign;
using Core.Entities.Securitty;
using NETCore.Encrypt;

namespace Infrastructure.Data
{
    public class SeedDb
    {
        private readonly SqlDbContext _context;


        #region Builder
        public SeedDb(SqlDbContext context)
        {
            _context = context;
        }
        #endregion

        public async Task ExecSeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await CheckPermissionAsync();
            await CheckRolAsync();
            await CheckRolPermissonAsync();
            await CheckUserAsync();
            await CheckMetaTypeServiceAsync();
        }

        private async Task CheckPermissionAsync()
        {
            if (!_context.PermissionEntity.Any())
            {
                _context.PermissionEntity.AddRange(new List<PermissionEntity>
                {
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearUsuarios,
                        Ambit="Usuarios",
                        Permission="Crear Usuarios",
                        Description="Crear usuarios en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarUsuarios,
                        Ambit="Usuarios",
                        Permission="Actualizar Usuarios",
                        Description="Actualizar datos de un usuario en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.EliminarUsuarios,
                        Ambit="Usuarios",
                        Permission="Eliminar Usuarios",
                        Description="Eliminar un usuairo del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission =(int) Enums.Permission.ConsultarUsuarios,
                        Ambit = "Usuarios",
                        Permission="Consultar Usuarios",
                        Description="Consulta todos los usuarios"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarRoles,
                        Ambit="Roles",
                        Permission="Actualizar Roles",
                        Description="Actualizar datos de un Roles en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarRoles,
                        Ambit="Roles",
                        Permission="Consultar Roles",
                        Description="Consultar Roles del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarPermisos,
                        Ambit = "Permisos",
                        Permission="Actualizar Permisos",
                        Description="Actualizar datos de un Permiso en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarPermisos,
                        Ambit = "Permisos",
                        Permission="Consultar Permisos",
                        Description="Consultar Permisos del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.DenegarPermisos,
                        Ambit = "Permisos",
                        Permission="Denegar Permisos Rol",
                        Description="Denegar permisos a un rol del sistema"
                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRolAsync()
        {
            if (!_context.RolEntity.Any())
            {
                _context.RolEntity.AddRange(new List<RolEntity>
                {
                    new RolEntity
                    {
                        IdRol=(int)Enums.RolUser.Administrador,
                        Rol="Administrador"
                    },
                    new RolEntity
                    {
                        IdRol=(int)Enums.RolUser.Estandar,
                        Rol="Estandar"
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        //Asignamos todos los permisos al rol administrador
        private async Task CheckRolPermissonAsync()
        {
            if (!_context.RolesPermissionsEntity.Where(x => x.IdRol == (int)Enums.RolUser.Administrador).Any())
            {
                var rolesPermisosAdmin = _context.PermissionEntity.Select(x => new RolesPermissionsEntity
                {
                    IdRol = (int)Enums.RolUser.Administrador,
                    IdPermission = x.IdPermission
                }).ToList();

                _context.RolesPermissionsEntity.AddRange(rolesPermisosAdmin);

                await _context.SaveChangesAsync();
            }
        }

        //Creamos un usuario por defecto
        private async Task CheckUserAsync()
        {
            if (!_context.UserEntity.Where(x => x.IdRol == (int)Enums.RolUser.Administrador).Any())
            {
                UserEntity user = new UserEntity()
                {
                    IdRol = (int)Enums.RolUser.Administrador,
                    Name = "Admin",
                    LastName = "Doe",
                    Email = "admin@admin.com",
                    VerefiedEmail = true,
                    RegisterDate = DateTime.Now,
                    Password = EncryptProvider.Sha256("123456"),
                    PasswordReset = false
                };

                _context.UserEntity.Add(user);

                await _context.SaveChangesAsync();
            }
        }


        private async Task CheckMetaTypeServiceAsync()
        {
            if (!_context.MetaTypeServiceEntity.Any())
            {
                _context.MetaTypeServiceEntity.AddRange(new List<MetaTypeServiceEntity>
                {
                    new MetaTypeServiceEntity
                    {
                        Name = "WhatsApp",
                        Code = "whatsapp",
                        IsEnabled = true,
                    },
                    new MetaTypeServiceEntity
                    {
                        Name = "Instagram",
                        Code = "instagram",
                        IsEnabled = false,
                    },
                    new MetaTypeServiceEntity
                    {
                        Name = "Facebook",
                        Code = "facebook",
                        IsEnabled = false,
                    },
                    new MetaTypeServiceEntity
                    {
                        Name = "Message",
                        Code = "message",
                        IsEnabled = false,
                    }
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.MarketingStatusEntity.Any())
            {
                _context.MarketingStatusEntity.AddRange(new List<MarketingStatusEntity>
                {
                    new MarketingStatusEntity
                    {
                        Name = "Nuevo",
                        Code = "created"
                    }
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.MarketingStatusEntity.Any(x => x.Code == "syncing"))
            {
                _context.MarketingStatusEntity.Add(
                    new MarketingStatusEntity
                    {
                        Name = "Sincronización en Proceso",
                        Code = "syncing"
                    });

                await _context.SaveChangesAsync();
            }

            if (!_context.MarketingStatusEntity.Any(x => x.Code == "active"))
            {
                _context.MarketingStatusEntity.Add(
                    new MarketingStatusEntity
                    {
                        Name = "Activo",
                        Code = "active"
                    });

                await _context.SaveChangesAsync();
            }

            if (!_context.MarketingStatusEntity.Any(x => x.Code == "badCredential"))
            {
                _context.MarketingStatusEntity.Add(
                    new MarketingStatusEntity
                    {
                        Name = "Credenciales de  NetSuite Incorrectas",
                        Code = "badCredential"
                    });

                await _context.SaveChangesAsync();
            }

            if (!_context.MarketingStatusEntity.Any(x => x.Code == "badMetaCredential"))
            {
                _context.MarketingStatusEntity.Add(
                    new MarketingStatusEntity
                    {
                        Name = "Credenciales de Meta Incorrectas",
                        Code = "badMetaCredential"
                    });

                await _context.SaveChangesAsync();
            }

            if (!_context.RecurringTypeEntity.Any()) {
                _context.RecurringTypeEntity.AddRange(new List<RecurringTypeEntity> { 
                    new RecurringTypeEntity{ 
                        Name = "Diario",
                        Code = "daily"
                    },
                    new RecurringTypeEntity{ 
                        Name = "Lunes a Viernes",
                        Code = "onweekdays"
                    },
                    new RecurringTypeEntity{
                        Name = "Mensual",
                        Code= "monthly"
                    },
                    new RecurringTypeEntity{
                        Name = "Anual",
                        Code= "annual"
                    },
                    new RecurringTypeEntity{
                        Name = "Una Vez",
                        Code= "once"
                    }
                });

                await _context.SaveChangesAsync();
            }


        }
    }
}
