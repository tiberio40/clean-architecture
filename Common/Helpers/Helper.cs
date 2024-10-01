using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Common.Helpers
{
    public static class Helper
    {
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = DateTime.UtcNow;
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Method to get value claim from JwtToken
        /// </summary>
        /// <param name="authorization"> Request.Headers["Authorization"] </param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public static string GetClaimValue(string token, string claim)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string authHeader = token.Replace("Bearer ", "").Replace("bearer ", "");
            JwtSecurityToken tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

            Claim claimData = tokenS.Claims.FirstOrDefault(cl => cl.Type.ToUpper() == claim.ToUpper());

            if (claimData == null || string.IsNullOrEmpty(claimData.Value))
                throw new UnauthorizedAccessException();

            return claimData.Value;
        }

        public static void DeleteFile(string pathFull)
        {
            if (File.Exists(pathFull))
                File.Delete(pathFull);
        }
    }
}
