using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Recap.Core;
using Recap.Core.Aspects.Base;
using Recap.Core.Aspects.Secured.Extensions;

namespace BackEndShared.Core.Aspects.Secured
{
    public class SecuredOperationAspect : ExceptionInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperationAspect(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception("!Yetkiniz Yok");
        }
    }
}
