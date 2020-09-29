$(document).ready(function () {
    ChangeOrdering();
    MoveScrollOrdering();
    ChangeImageProduct();
});
function ChangeImageProduct() {
    $(".img-small-wrap img").click(function () {
        var path = $(this).attr("src");
        $(".img-big-wrap img").attr("src", path);
        $(".img-big-wrap a").attr("href", path);
    });
}

function MoveScrollOrdering() {
    if (window.location.hash.length > 0) {
        var hash = window.location.hash;
        if (hash == "#positionProduct") {
            window.scrollBy(0, 473);
        }
    }
}
function ChangeOrdering() {
    $("#ordering").change(function () {
        var page = 1;
        var search = "";
        var ordering = $(this).val();
        var fragment = "#positionProduct";

        var queryString = new URLSearchParams(window.location.search);
        if (queryString.has("page")) {
            page = queryString.get("page");
        }
        if (queryString.has("search")) {
            search = queryString.get("search");
        }

        if ($("#breadcrumbs").length > 0) {
            fragment = "";
        }

        var URL = window.location.protocol + "//" + window.location.host  + window.location.pathname;

        var urlWithParameters = URL + "?page=" + page + "&search=" + search + "&ordering=" + ordering + fragment;

        window.location.href = urlWithParameters;
    });
}