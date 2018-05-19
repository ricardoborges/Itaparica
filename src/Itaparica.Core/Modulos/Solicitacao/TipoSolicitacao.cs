using Itaparica.Core.Domain.Model;

namespace Itaparica.Core.Modulos.Solicitacao
{
    public class TipoSolicitacao: IIdentifiable
    {
        public virtual long Id { get; set; }

        public virtual string Descricao { get; set; }
    }
}