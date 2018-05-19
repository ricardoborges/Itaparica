using System.Web.Mvc;
using Itaparica.Core.Modulos.Solicitacao;
using Itaparica.Core.Modulos.Solicitacao.Repositories;
using Itaparica.Web.Infra.Controllers;

namespace Itaparica.Web.Areas.Cadastros.Controllers
{
    public class TipoSolicitacaoController: BaseController
    {
        private readonly ITipoSolicitacaoRepository _repository;

        public TipoSolicitacaoController(ITipoSolicitacaoRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetViewData()
        {
            return Json(_repository.FindAll());
        }
        
        public JsonResult Salvar(TipoSolicitacao tipoSolicitacao)
        {
            _repository.AddOrUpdate(tipoSolicitacao);

            return Done();
        }

        public JsonResult Excluir(long id)
        {
            _repository.Remove(id);

            return Done();
        }
    }
}