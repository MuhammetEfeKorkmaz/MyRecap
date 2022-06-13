using Castle.DynamicProxy;
using Recap.Core.Aspects.Base;
using System.Transactions;

namespace Recap.Core.Aspects.Transaction
{
    public class TransactionAspect : ExceptionInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
