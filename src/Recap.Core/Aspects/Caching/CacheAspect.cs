using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Recap.Core.Aspects.Base;

namespace Recap.Core.Aspects.Caching
{
    public class CacheAspect : ExceptionInterception
    {
        private int duration;
        private ICacheManager _cacheManager;
        public CacheAspect(int _duration = 60)
        {
            duration = _duration;
            _cacheManager = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override async void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, duration);
        }


    }
}
