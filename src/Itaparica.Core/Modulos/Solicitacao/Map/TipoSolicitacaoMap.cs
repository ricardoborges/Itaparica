using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Itaparica.Core.Modulos.Solicitacao.Map
{
    public class TipoSolicitacaoMap: ClassMapping<TipoSolicitacao>
    {
        public TipoSolicitacaoMap()
        {
            Table("tTipoSolicitacao");

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
                m.Column("Id");
            });

            Property(x => x.Descricao, c => c.Column("DsTipoSolicitacao"));
        }
    }
}