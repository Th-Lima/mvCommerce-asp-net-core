$(document).ready(function () {

    $(".alert-danger").click(function (e) {
        var result = confirm("Deseja realmente concluir esta ação?");

        if (result == false) {
            e.preventDefault();
        }
    });
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
});