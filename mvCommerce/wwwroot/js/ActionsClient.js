$(document).ready(function () {
    ChangeOrdering();
    MoveScrollOrdering();
    ChangeImageProduct();
    ChangeAmountProductCart();
});
function numberToReal(numero) {
    var numero = numero.toFixed(2).split('.');
    numero[0] = "R$ " + numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}

function ChangeAmountProductCart() {
    $("#order .amount").click(function () {

        if ($(this).hasClass("amountLess")) {
            ChangeAmount("amountLess", $(this));
        }

        if ($(this).hasClass("amountLarger")) {
            ChangeAmount("amountLarger", $(this));
        }
    });
}

function ChangeAmount(operation, button) {
    var father = button.parent().parent();

    var producId = father.find(".inputProductId").val();
    var amountStore = parseInt(father.find(".inputAmountStore").val());
    var unitValue = parseFloat(father.find(".inputValueUnit").val().replace(",", "."));

    var inputAmountProductCart = father.find(".inputAmountProductCart");
    var amountProductCart = parseInt(inputAmountProductCart.val());

    var fieldPrice = button.parent().parent().parent().parent().parent().find(".priceProduct");

    //TODO - Add validation
    if (operation == "amountLarger")
    {

        if (amountProductCart == amountStore)
        {
            alert("Oops, não possuimos estoque suficiente para este produto!")
        }

        else
        {

            amountProductCart++;

            inputAmountProductCart.val(amountProductCart);

            var result = unitValue * amountProductCart;
            fieldPrice.text(numberToReal(result));

        }

    } else if (operation == "amountLess")

    {
        if (amountProductCart == 1)
        {
            alert("Oops, caso não deseje mais esse produto, clique em remover")
        }
        else
        {
            amountProductCart--;

            inputAmountProductCart.val(amountProductCart);

            var result = unitValue * amountProductCart;
            fieldPrice.text(numberToReal(result));
        }
    }

    //TODO - Update subValue

}

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