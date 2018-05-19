using Itaparica.Core.Domain.Repositories;
using NHibernate;

namespace Itaparica.Core.Modulos.Solicitacao.Repositories
{
    public interface ITipoSolicitacaoRepository : ICrudRepository<TipoSolicitacao, long>
    {
        
    }

    public class TipoSolicitacaoRepository: DefaultRepository<TipoSolicitacao, long>, ITipoSolicitacaoRepository
    {
        public TipoSolicitacaoRepository(ISession session) : base(session)
        {

        }
    }
}