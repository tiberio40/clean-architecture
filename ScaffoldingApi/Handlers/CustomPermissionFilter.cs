using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Resources;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using static Common.Constant.Const;
using static NETCore.Encrypt.Shared.Check;
using Application.Interfaces.Security;

namespace ScaffoldingApi.Handlers
{
    public class CustomPermissionFilter : TypeFilterAttribute
    {
        public CustomPermissionFilter(Enums.RolUser rol) : base(typeof(CustomPermissionFilterImplement))
        {
            Arguments = new object[] { rol };
        }

        private class CustomPermissionFilterImplement : IActionFilter
        {
            private readonly IUserServices _userService;
            private readonly Enums.RolUser _rol;

            public CustomPermissionFilterImplement(IUserServices userService, Enums.RolUser rol)
            {
                _userService = userService;
                _rol = rol;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                string token = context.HttpContext.Request.Headers["Authorization"];
                string idUser = Helper.GetClaimValue(token, TypeClaims.IdUser);

                var user = _userService.GetUser(Convert.ToInt32(idUser));
                if (user.IdRol != (int)_rol)
                    throw new BusinessException(GeneralMessages.WithoutPermission);
            }
        }
    }
}
