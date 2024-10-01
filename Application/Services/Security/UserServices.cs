using Application.Interfaces.Security;
using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Resources;
using Core.DTOs.Security.User;
using Core.Entities.Securitty;
using Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Common.Constant.Const;

namespace Application.Services.Security
{
    public class UserServices : IUserServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        #endregion

        #region Builder
        public UserServices(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        #endregion

        #region Authentication
        public TokenDto Login(LoginDto loginDto)
        {
            UserEntity user = _unitOfWork.UserRepository.FirstOrDefault(x => x.Email.ToLower() == loginDto.Email.ToLower());
            if (!user.IsActive)
                throw new BusinessException(GeneralMessages.InactiveUser);

            if (user.Password != EncryptProvider.Sha256(loginDto.Password))
                throw new BusinessException(GeneralMessages.BadCredentials);

            //TOKEN
            TokenDto token = GenerateTokenJWT(user);
            token.ResetPassword = user.PasswordReset;

            return token;
        }

        private TokenDto GenerateTokenJWT(UserEntity userEntity)
        {
            IConfigurationSection tokenAppSetting = _configuration.GetSection("Tokens");

            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenAppSetting.GetSection("Key").Value));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(TypeClaims.IdUser,userEntity.IdUser.ToString()),
                 new Claim(TypeClaims.FullName,userEntity.FullName),
                 new Claim(TypeClaims.Email,userEntity.Email),
                 new Claim(TypeClaims.IdRol,userEntity.IdRol.ToString()),
             };
            var _payload = new JwtPayload(
                    issuer: tokenAppSetting.GetSection("Issuer").Value,
                    audience: tokenAppSetting.GetSection("Audience").Value,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddDays(30)
                );
            var _token = new JwtSecurityToken(
                    _header,
                    _payload
                );
            TokenDto token = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(_token),
                Expiration = Helper.ConvertToUnixTimestamp(_token.ValidTo),
            };


            return token;
        }

        #endregion

        #region Methods

        public async Task<bool> RegisterUser(AddUserDto register)
        {
            if (ExistEmail(register.Email))
                throw new BusinessException(GeneralMessages.RegisteredEmail);

            UserEntity user = new UserEntity()
            {
                Email = register.Email,
                Password = EncryptProvider.Sha256(register.Password),
                Name = register.Name,
                LastName = register.LastName,
                RegisterDate = DateTime.Now,
                IdRol = (int)Enums.RolUser.Estandar,
                PasswordReset = true
            };
            _unitOfWork.UserRepository.Insert(user);

            return await _unitOfWork.Save() > 0;
        }

        public List<ConsultUserDto> GetAllUser()
        {
            IEnumerable<UserEntity> users = _unitOfWork.UserRepository.GetAll(x => x.RolEntity);
            List<ConsultUserDto> list = users.Select(x => new ConsultUserDto()
            {
                Email = x.Email,
                FullName = x.FullName,
                IdRol = x.IdRol,
                IdUser = x.IdUser,
                RegisterDate = x.RegisterDate,
                VerefiedEmail = x.VerefiedEmail,
                Rol = x.RolEntity.Rol,
                Password = string.Empty,
                IsActive = x.IsActive
            }).ToList();

            return list;
        }

        public ConsultUserDto GetUser(int idUser)
        {
            UserEntity user = GetUserById(idUser);

            return new ConsultUserDto()
            {
                Email = user.Email,
                FullName = user.FullName,
                IdRol = user.IdRol,
                IdUser = user.IdUser,
                RegisterDate = user.RegisterDate,
                VerefiedEmail = user.VerefiedEmail,
                Rol = user.RolEntity.Rol,
                Password = string.Empty,
                IsActive = user.IsActive
            };
        }

        public ConsultUserDto GetUserByEmail(string email)
        {
            UserEntity user = _unitOfWork.UserRepository.FirstOrDefault(u => u.Email.ToLower() == email.ToLower(),
                                                                        r => r.RolEntity);
            if (user == null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Usuario");
                throw new BusinessException(message);
            }

            return new ConsultUserDto()
            {
                Email = user.Email,
                FullName = user.FullName,
                IdRol = user.IdRol,
                IdUser = user.IdUser,
                RegisterDate = user.RegisterDate,
                VerefiedEmail = user.VerefiedEmail,
                Rol = user.RolEntity.Rol,
                Password = string.Empty,
                IsActive = user.IsActive
            };
        }

        public async Task<bool> UpdatePassword(UserPasswordDto password, int idUser)
        {
            UserEntity user = GetUserById(idUser);

            if (user.Password != EncryptProvider.Sha256(password.OldPassword))
                throw new BusinessException(GeneralMessages.BadCredentials);

            if (password.OldPassword == password.Password)
                throw new BusinessException(GeneralMessages.EqualCredentials);

            user.Password = EncryptProvider.Sha256(password.Password);
            user.PasswordReset = false;

            return await UpdateUser(user);
        }

        public async Task<bool> ResetPassword(ResetPasswordDto reset)
        {
            UserEntity user = GetUserById(reset.IdUser);
            user.PasswordReset = true;
            user.Password = EncryptProvider.Sha256(reset.Password);


            return await UpdateUser(user);
        }

        public async Task<bool> ActivateAndDeactivate(ActiveUserDto activeUser)
        {
            UserEntity user = GetUserById(activeUser.IdUSer);
            user.IsActive = activeUser.IsActive;

            return await UpdateUser(user);
        }

        #region Privates
        private async Task<bool> UpdateUser(UserEntity userUpdate)
        {
            userUpdate.UpdateDate = DateTime.Now;
            _unitOfWork.UserRepository.Update(userUpdate);

            return await _unitOfWork.Save() > 0;
        }

        private UserEntity GetUserById(int idUser)
        {
            UserEntity user = _unitOfWork.UserRepository.FirstOrDefault(u => u.IdUser == idUser,
                                                                        r => r.RolEntity);
            if (user == null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Usuario");
                throw new BusinessException(message);
            }

            return user;
        }

        private bool ExistEmail(string email)
        {
            bool result = false;
            if (_unitOfWork.UserRepository.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()) != null)
                result = true;

            return result;
        }
        #endregion


        #endregion
    }
}
