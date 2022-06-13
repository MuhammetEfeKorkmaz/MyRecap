using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Recap.Core.Aspects.Base;

namespace Recap.Core.Aspects.Cancelation
{
    public class CancellationTokenAspect : ExceptionInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            var token = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IHttpContextAccessor>().HttpContext.RequestAborted;
            Task.Run(() =>
            {
                invocation.Proceed();
            }, token).Wait(token);
        }
    }
}
