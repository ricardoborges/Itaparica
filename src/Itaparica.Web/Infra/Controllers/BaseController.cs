using System.Net;
using System.Web.Mvc;
using Itaparica.Core.Domain.Controller;

namespace Itaparica.Web.Infra.Controllers
{
    public class BaseController : Controller, IItaparicaController
    {
        protected JsonResult JsonError(string message = "Serviço Indisponível")
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return Json(new { message }, "text/html");
        }

        protected JsonResult Done(string message = "Operação Realizada com Sucesso")
        {
            return Json(new { message }, "text/html");
        }
    }
}