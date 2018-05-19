using Castle.DynamicProxy;

namespace Itaparica.Core.Infra.Graphviz
{
    /// <summary>
    /// Interceptador de execução de fluxo.
    /// </summary>
    public class ExecutionFlowInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            new CallManager().Push(invocation.TargetType.Name, invocation.Method.Name);

            invocation.Proceed();
        }
    }
}
