using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Recap.Core.Aspects.Base;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Recap.Core.Aspects.Performance
{

    public class DeveloperDBPerformanceAspect : ExceptionInterception
    {
        private int _interval=0;
        private Stopwatch _stopwatch;

        public DeveloperDBPerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<Stopwatch>();
        }
        public DeveloperDBPerformanceAspect()
        { 
            _stopwatch = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_interval==0)
            {
                _stopwatch.Stop();
                 Debug.WriteLine($"Performance :[{GetMethodInfo(1)} {invocation.Method.DeclaringType.FullName.Split('`')[0]}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}]");
                _stopwatch.Reset();
            }
            else
            {
                if (_stopwatch.Elapsed.TotalSeconds > _interval)
                {
                    _stopwatch.Stop();
                    Debug.WriteLine($"Performance :[{GetMethodInfo(1)} {invocation.Method.DeclaringType.FullName.Split('`')[0]}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}]");
                }
                _stopwatch.Reset();
            }
           
        }

        public static string GetMethodInfo(int beforeFrameCount)
        {
            StringBuilder sb = new StringBuilder();
            MethodBase method = null;

            for (int i = 20; i > 1; i--)
            {
                method = new StackFrame(i - beforeFrameCount).GetMethod();
                if (method != null)
                {
                    if (method.ReflectedType == null)
                        continue;

                    var methodName = string.Format($"{method.ReflectedType.FullName}.{method.Name}");
                    var argumentsBefore = method.GetParameters().ToList();
                    var keyBefore = $"{methodName}({string.Join(",", argumentsBefore.Select(x => x?.ToString() ?? "<Null>"))})";
                    if (keyBefore.Contains("Castle."))
                        continue;

                    if (keyBefore.Contains("Invoker."))
                        continue;

                    if (keyBefore.Contains("SyncActionResultExecutor"))
                        continue;

                    if (keyBefore.Contains("AsyncVoidMethodBuilder"))
                        continue;

                    if (keyBefore.Contains("AsyncMethodBuilderCore"))
                        continue;

                    if (keyBefore.Contains("MoveNext"))
                        continue;

                    methodName = $"-->{methodName}";


                    sb.Append(methodName);
                }
            }
            string sonuc = sb.ToString();
            sonuc = sonuc.Remove(0, 3);
            sonuc = sonuc.Remove(sonuc.Length - 3, 3);
            return sb.ToString();

        }




    }
}

