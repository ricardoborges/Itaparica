(function () {

    config.inject = ["toastrConfig"];

    function config(toastrConfig) {

        toastrConfig.positionClass = "toast-top-right";
        toastrConfig.closeButton = true;
        toastrConfig.progressBar = true;
    }

    angular
        .module("itaparica")
        .config(config);
})();
