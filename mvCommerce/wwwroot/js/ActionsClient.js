$(document).ready(function () {
    ChangeOrdering();
});

function ChangeOrdering() {
    $("#ordering").change(function () {
        //TODO - REDIRECIONAR HOME/INDEX PASSANDO QUERYSTRING DE ORDENAÇÃO MANTENDO A PAGINA E A PESQUISA.
        var page = 1;
        var search = "";
        var ordering = $(this).val();

        var queryString = new URLSearchParams(window.location.search);
        if (queryString.has("page")) {
            page = queryString.get("page");
        }
        if (queryString.has("search")) {
            page = queryString.get("search");
        }

        var URL = window.location.protocol + "//" + window.location.host  + window.location.pathname;

        var urlWithParameters = URL + "?page=" + page + "&search=" + search + "&ordering=" + ordering + "#code_prod_complex";

        window.location.href = urlWithParameters;
    });
}