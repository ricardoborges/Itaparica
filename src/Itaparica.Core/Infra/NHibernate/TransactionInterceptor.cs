using System.Transactions;
using Castle.DynamicProxy;

namespace Itaparica.Core.Infra.NHibernate
{
    /// <summary>
    /// Controle de transação automático.
    /// </summary>
    public class TransactionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                try
                {
                    invocation.Proceed();

                    scope.Complete();
                }
                catch
                {
                    scope.Dispose();
                    throw;
                }
            }
        }
    }
}
