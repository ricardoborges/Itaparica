(function () {

    tipoSolicitacaoCtrl.inject = ["$scope", "$http", "toastr"];

    function tipoSolicitacaoCtrl($scope, $http, toastr) {

        $scope.view = {};
        $scope.viewdata = {};

        $scope.setup = function () {
            $scope.getViewData();
        }
        
        $scope.validaForm = function (form) {
            //if (form.validate()) {
            //    return true;
            //}

            //return false;
        }

        $scope.getViewData = function () {

            $scope.view.loading = true;
            
            $http({
                method: "POST",
                url: "/Cadastros/TipoSolicitacao/GetViewData"
            }).then(function successCallback(response) {

                $scope.viewdata.list = response.data;

                $scope.view.loading = false;

            }, function errorCallback(response) {

                $scope.view.loading = false;
            });
        }


        $scope.salvar = function (form) {

            //if (!form.validate()) {
            //    return false;
            //}

            $scope.view.saving = true;

            $http({
                method: "POST",
                url: "/Cadastros/TipoSolicitacao/Salvar",
                data: $scope.viewdata.tipoSolicitacao
            }).then(function successCallback(response) {

                $scope.viewdata.tipoSolicitacao = {};

                toastr.success("Operação realizada com sucesso.", "Concluído.");

                $scope.view.saving = false;
                $scope.getViewData();

            }, function errorCallback(response) {

                $scope.view.saving = false;
            });
        }

        $scope.inativar = function (item) {

            bootbox.confirm({
                size: "small",
                title: "Atenção",
                message: "Confirmar inativação?",
                callback: function (result) {

                    if (!result) return;

                    $scope.viewdata.deleting = true;

                    $http({
                        method: "POST",
                        url: "/Cadastros/TipoSolicitacao/Excluir",
                        data: item
                    }).then(function successCallback(response) {

                        toastr.success("Operação realizada com sucesso.", "Concluído.");

                        $scope.getViewData();

                        $scope.viewdata.deleting = false;

                    }, function errorCallback(response) {

                        toastr.error(response.data.message, "Atenção.");

                        $scope.viewdata.deleting = false;
                    });
                }
            });
        }

        $scope.editar = function (item) {
            $scope.viewdata.model = angular.copy(item);
        }
    }

    angular
        .module("itaparica")
        .controller("tipoSolicitacaoCtrl", tipoSolicitacaoCtrl);
})();
