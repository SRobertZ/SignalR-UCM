using System;
using System.Linq;
using Comet.Contracts;
using Microsoft.AspNet.SignalR;

namespace Comet
{
    public class UserIdProvider : IUserIdProvider
    {
        private readonly IUserIdResolver _userIdResolver;

        public UserIdProvider(IUserIdResolver userIdResolver)
        {
            _userIdResolver = userIdResolver;
        }

        public string GetUserId(IRequest request)
        {
            bool useMobileToken = UseMobileToken(request);
            string token = string.Empty;
            if (useMobileToken)
            {
                token = GetTokenFromHeaders(request);
            }
            else
            {
                if (request.Cookies.ContainsKey("X-Token"))
                {
                    token = request.Cookies["X-Token"].Value;
                }
            }
            if (!string.IsNullOrEmpty(token))
            {
                Guid? id = _userIdResolver.GetUserId(token, false, useMobileToken);
                return (id.HasValue ? id.Value : new Guid()).ToString();
            }
            return new Guid().ToString();
        }

        protected bool UseMobileToken(IRequest request)
        {
            try
            {
                var headers = request.Headers.GetValues("MOBILE");
                return headers.Any();
            }
            catch
            {
                return false;
            }
        }

        protected string GetTokenFromHeaders(IRequest request)
        {
            try
            {
                var headers = request.Headers.GetValues("X-Token");
                return headers.First();
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
