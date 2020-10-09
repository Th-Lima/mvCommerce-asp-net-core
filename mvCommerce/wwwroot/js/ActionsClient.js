
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
            ActionsManager("amountLess", $(this));
        }

        if ($(this).hasClass("amountLarger")) {
            ActionsManager("amountLarger", $(this));
        }
    });
}



function ActionsManager(operation, button) {
    HideErrorMessage();
    /**
     * 
     * Load Values
     * 
     * */

    var father = button.parent().parent();

    var producId = father.find(".inputProductId").val();
    var amountStore = parseInt(father.find(".inputAmountStore").val());
    var unitValue = parseFloat(father.find(".inputValueUnit").val().replace(",", "."));

    var inputAmountProductCart = father.find(".inputAmountProductCart");
    var amountProductCartOld = parseInt(inputAmountProductCart.val());

    var fieldPrice = button.parent().parent().parent().parent().parent().find(".priceProduct");

    var product = new ProductAmountAndPrice(producId, amountStore, unitValue, amountProductCartOld, 0, inputAmountProductCart, fieldPrice);

    /*
     * 
     * Call methods
     * 
     */
    ChangeScreenProductCart(product, operation);
   

    //TODO - Update subValue
}

function ChangeScreenProductCart(product, operation) {
    //TODO - Add validation
    if (operation == "amountLarger") {

        /*if (product.amountProductCartOld == product.amountStore) {
            alert("Oops, não possuimos estoque suficiente para este produto!");
        }

        else*/ {
            product.amountProductCartNew = product.amountProductCartOld + 1;

            UpdateAmountAndValue(product);

            AJAXToCommunicateUpdateAmountProduct(product);
        }

    } else if (operation == "amountLess") {
        /*if (product.amountProductCartOld == 1) {
            alert("Oops, caso não deseje mais esse produto, clique em remover")
        }
        else*/ {
            product.amountProductCartNew = product.amountProductCartOld - 1;

            UpdateAmountAndValue(product);

            AJAXToCommunicateUpdateAmountProduct(product);
        }
    }
}

function AJAXToCommunicateUpdateAmountProduct(product) {

    $.ajax({
        type: "GET",
        url: "/ShoppingCart/UpdateAmount?id=" + product.producId + "&amount=" + product.amountProductCartNew,
        error: function (data) {
            ShowErrorMesssage(data.responseJSON.message);
            //Rollback
            product.amountProductCartNew = product.amountProductCartOld;
            UpdateAmountAndValue(product);
        },
        success: function () {

        }
    });
}

function ShowErrorMesssage(message) {
    $(".alert-danger").css("display", "block");
    $(".alert-danger").text(message);

    setTimeout(HideErrorMessage, 6000);
}

function HideErrorMessage() {
    $(".alert-danger").css("display", "none");
}

function UpdateAmountAndValue(product) {

    product.inputAmountProductCart.val(product.amountProductCartNew);

    var result = product.unitValue * product.amountProductCartNew;
    product.fieldPrice.text(numberToReal(result));
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




/*
 * ---------- Classes --------------
 */

class ProductAmountAndPrice {
    constructor(producId, amountStore, unitValue, amountProductCartOld, amountProductCartNew, inputAmountProductCart, fieldPrice) {
        this.producId = producId;
        this.amountStore = amountStore;
        this.unitValue = unitValue;

        this.amountProductCartOld = amountProductCartOld;
        this.amountProductCartNew = amountProductCartNew;

        this.inputAmountProductCart = inputAmountProductCart;
        this.fieldPrice = fieldPrice;
    }
}