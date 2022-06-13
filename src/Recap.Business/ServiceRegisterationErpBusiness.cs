using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Recap.Business.Abstract;
using Recap.Business.Concrete;
using Recap.Core.Aspects.Base;
using Recap.Dal.Abstract;
using Recap.Dal.Concrete.Factories.EntityFramework;

namespace Recap.Business
{
    public class ServiceRegisterationErpBusiness : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DepartmanManager>().As<IDepartmanService>().SingleInstance();
            builder.RegisterType<EfDepartmanDal>().As<IDepartmanDal>().SingleInstance();

            builder.RegisterType<UnvanManager>().As<IUnvanService>().SingleInstance();
            builder.RegisterType<EfUnvanDal>().As<IUnvanDal>().SingleInstance();

            
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();


            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();





            var LoadedAssembly = System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var CoreAssambly_ = LoadedAssembly.FirstOrDefault(x => x.FullName.Contains(".Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
            var DataAccessAssambly_ = LoadedAssembly.FirstOrDefault(x => x.FullName.Contains(".DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));


            if (CoreAssambly_ != null)
            {
                System.Reflection.Assembly CoreAssambly = System.Reflection.Assembly.Load(CoreAssambly_.ToString());
                if (CoreAssambly != null)
                {
                    builder.RegisterAssemblyTypes(CoreAssambly).AsImplementedInterfaces()
                        .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                             {
                                 Selector = new InterceptorCollection()
                              }).SingleInstance();
                }
            }

            if (DataAccessAssambly_ != null)
            {
                System.Reflection.Assembly DataAccessAssambly = System.Reflection.Assembly.Load(DataAccessAssambly_.ToString());
                if (DataAccessAssambly != null)
                {
                    builder.RegisterAssemblyTypes(DataAccessAssambly).AsImplementedInterfaces()
                        .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                        {
                            Selector = new InterceptorCollection()
                        }).SingleInstance();
                }
            }
          





           


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new InterceptorCollection()
                }).SingleInstance();


        }

    }
}
