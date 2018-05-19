using System.Web.Optimization;

namespace Itaparica.Web.Bundles
{
    public class CadastrosBundles
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/tipoSolicitacaoCtrl").Include(
                      "~/Areas/Cadastros/Scripts/Controllers/tipoSolicitacaoCtrl.js"));

        }
    }
}
