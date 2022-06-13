using Castle.DynamicProxy;
using Recap.Core.Aspects.Cancelation;
using System.Reflection;

namespace Recap.Core.Aspects.Base
{

    public class InterceptorCollection : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<InterceptionBaseAttiribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<InterceptionBaseAttiribute>(true);
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new CancellationTokenAspect());
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }


    


}
