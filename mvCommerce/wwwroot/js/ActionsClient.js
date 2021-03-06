﻿
$(document).ready(function () {
    ChangeOrdering();
    MoveScrollOrdering();
    ChangeImageProduct();
    ChangeAmountProductCart();

    MaskCEP();
    AJAXSearchCEP();
    CalculateActionBtn();
    AJAXCalculateFreight(false);
    AJAXCalculateFreightDeliveryAddress();
});

function AJAXSearchCEP() {
    $("#CEP").keyup(function () {
        HideErrorMessage();

        if ($(this).val().length == 10) {

            var cep = RemoveMask($(this).val());
            $.ajax({
                dataType: "jsonp",
                type: "GET",
                url: "https://viacep.com.br/ws/" + cep + "/json/?callback=callback_name",
                error: function () {
                    ShowErrorMesssage("Oops, Algo deu errado, tivemos um erro na busca do CEP, tente mais tarde ...");
                },
                success: function (data) {
                    console.info("Ok")
                    console.info(data)

                    if (data.erro == undefined) {
                        $("#State").val(data.uf);
                        $("#City").val(data.localidade);
                        $("#Address").val(data.logradouro);
                        $("#Neighborhood").val(data.bairro);
                    } else {
                        ShowErrorMesssage("O CEP informado não existe.");
                    }

                }
            });
        }
    });
}

function MaskCEP() {
    $(".cep").mask("00.000-000");
}

function CalculateActionBtn() {
    $(".btn-calc-freight").click(function (e) {
        AJAXCalculateFreight(true);
        e.preventDefault();
    });
}

function AJAXCalculateFreight(callByButton) {
    $(".btn-continue").addClass("disabled");
    if (!callByButton) {
        if ($.cookie('cart.cep') != undefined) {
            $(".cep").val($.cookie('cart.cep'));
        }
    }

    if ($(".cep").length > 0) {

        var cep = RemoveMask($(".cep").val());
        $.removeCookie("cart.typefreight");

        if (cep.length == 8) {

            //Add cookie in the input
            $.cookie('cart.cep', $(".cep").val());

            $(".container-freight").html("<img id='load-freight' src='\\img\\loader.gif'/>");
            $(".freight").text("R$ 0,00");
            $(".total").text("R$ 0,00");

            $.ajax({
                type: "GET",
                url: "/ShoppingCart/CalculateFreight?cepDestiny=" + cep,
                error: function (data) {
                    ShowErrorMesssage("Ooops, tivemos um erro ao obter o frete! " + data.Message);
                },
                success: function (data) {
                    console.info(data)
                    html = "";

                    for (var i = 0; i < data.listValues.length; i++) {
                        var typeFreight = data.listValues[i].typeFreight;
                        var value = data.listValues[i].value;
                        var deadline = data.listValues[i].deadline;

                        html += "<dl class=\"dlist-align\"><dt> <input type=\"radio\" name=\"frete\" value=\"" + typeFreight + "\" /><input type=\"hidden\" name=\"value\" value=\"" + value + "\"/></dt ><dd>" + typeFreight + " - " + numberToReal(value) + " (" + deadline + ") dias úteis</dd></dl>"
                    }

                    $(".container-freight").html(html);
                    $(".container-freight").find("input[type=radio]").change(function () {
                        $.cookie("cart.typefreight", $(this).val());

                        $(".btn-pay ").removeClass("disabled");
                        $(".select-freight").addClass("text-hide");

                        var valueFreight = parseFloat($(this).parent().find("input[type=hidden]").val());



                        $(".freight").text(numberToReal(valueFreight));

                        var subtotal = parseFloat($(".subtotal").text().replace("R$", "").replace(".", "").replace(",", "."));
                        //console.info(subtotal);

                        var total = valueFreight + subtotal

                        $(".total").text(numberToReal(total));
                    });
                    //console.info(data);
                }
            });
        } else {
            if (callByButton) {
                $(".container-freight").html("");
                ShowErrorMesssage("Digite um CEP ou verifique se o CEP digitado está correto");
            }
        }

    }
}

function AJAXCalculateFreightDeliveryAddress() {

    $("input[name=address]").change(function () {

        $.cookie("ShoppingCart.Address", $(this).val(), {path: "/"})

        var cep = RemoveMask($(this).parent().find("input[name=cep]").val());

        ClearDataDeliveryAddressCards();

        LoadingCardsDeliveryAddress();

        $(".btn-continue").addClass("disabled");

        $.ajax({
            type: "GET",
            url: "/ShoppingCart/CalculateFreight?cepDestiny=" + cep,
            error: function (data) {
                ShowErrorMesssage("Ooops, tivemos um erro ao obter o frete! " + data.Message);

                ClearDataDeliveryAddressCards();
            },
            success: function (data) {

                ClearValues();
                ClearDataDeliveryAddressCards();

                for (var i = 0; i < data.listValues.length; i++) {
                    var typeFreight = data.listValues[i].typeFreight;
                    var value = data.listValues[i].value;
                    var deadline = data.listValues[i].deadline;

                    $(".card-title")[i].innerHTML = typeFreight;

                    $(".card-text")[i].innerHTML = "Prazo de " + deadline + " dias.";

                    $(".card-footer .text")[i].innerHTML = "<input class=\"mt-4\ mr-3\" type=\"radio\"name=\"freight\" value=\" " + typeFreight + "\" id='" + typeFreight + "' /> " + " <strong> " + numberToReal(value) + "</strong > ";

                    //console.info("cart.typefreight" + " - " + typeFreight)
                    //console.info($.cookie("cart.typefreight") == typeFreight);

                    if ($.cookie("cart.typefreight") != undefined && $.cookie("cart.typefreight") == typeFreight) {
                        $(".card-footer .text input[name=freight]").eq(i).attr("checked", "checked");
                        SelectTypeFreightStyle($(".card-footer .text").find("input[name=freight]").eq(i));

                        $(".btn-continue").removeClass("disabled");
                    }
                }

                $(".card-footer .text").find("input[name=freight]").change(function () {
                    $.cookie("cart.typefreight", $(this).val(), { path: '/' });
                    $(".btn-continue").removeClass("disabled");
                    SelectTypeFreightStyle($(this));
                });



                //$(".container-freight").html(html);
                //$(".container-freight").find("input[type=radio]").change(function () {
                //    $.cookie("cart.typefreight", $(this).val());

                //    $(".btn-pay ").removeClass("disabled");
                //    $(".select-freight").addClass("text-hide");

                //    var valueFreight = parseFloat($(this).parent().find("input[type=hidden]").val());



                //    $(".freight").text(numberToReal(valueFreight));

                //    var subtotal = parseFloat($(".subtotal").text().replace("R$", "").replace(".", "").replace(",", "."));
                //    //console.info(subtotal);

                //    var total = valueFreight + subtotal

                //    $(".total").text(numberToReal(total));
                //});
                ////console.info(data);
            }
        });

    });
}



function SelectTypeFreightStyle(obj) {

    $(".card-body").css("background-color", "white");
    $(".card-footer").css("background-color", "rgba(0,0,0,.03)");


    obj.parent().parent().parent().find(".card-footer").css("background-color", "#daeefe");
    obj.parent().parent().parent().find(".card-body").css("background-color", "#daeefe");

    UpdateValues();
}

function UpdateValues() {
    var freight = parseFloat($(".card-footer input[name=freight]:checked").parent().find("strong").text().replace("R$", "").replace(".", "").replace(",", "."));
        
    var product = parseFloat($(".text-product").text().replace("R$", "").replace(".", "").replace(",", "."));

    var total = freight + product;
  
    $(".text-freight").text(numberToReal(freight));
    $(".text-total").text(numberToReal(total));
}

function ClearValues() {
    $(".text-freight").text("");
    $(".text-total").text("");
}

function LoadingCardsDeliveryAddress() {
    for (var i = 0; i < 3; i++) {
        $(".card-text")[i].innerHTML = "<img id='load-freight' src='\\img\\loader.gif'/>";
    }
}

function ClearDataDeliveryAddressCards() {

    for (var i = 0; i < 3; i++) {
        $(".card-title")[i].innerHTML = "-";

        $(".card-text")[i].innerHTML = "-";

        $(".card-footer .text")[i].innerHTML = "-";
    }
}

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


        product.amountProductCartNew = product.amountProductCartOld + 1;

        UpdateAmountAndValue(product);

        AJAXToCommunicateUpdateAmountProduct(product);

    } else if (operation == "amountLess") {
        product.amountProductCartNew = product.amountProductCartOld - 1;

        UpdateAmountAndValue(product);

        AJAXToCommunicateUpdateAmountProduct(product);
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
            AJAXCalculateFreight();
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

    UpdateSubTotal();
}

function UpdateSubTotal() {
    var subTotal = 0;

    var tagsWithClassPrice = $(".price");

    tagsWithClassPrice.each(function () {
        var valueReal = parseFloat($(this).text().replace("R$", "").replace(".", "").replace(" ", "").replace(",", ".").replace("Total:", ""));
        subTotal = subTotal + valueReal;
    });
    $(".subtotal").text(numberToReal(subTotal));
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

        var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;

        var urlWithParameters = URL + "?page=" + page + "&search=" + search + "&ordering=" + ordering + fragment;

        window.location.href = urlWithParameters;
    });
}


function RemoveMask(value) {
    return value.toString() .replace(".", "").replace("-", "");
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