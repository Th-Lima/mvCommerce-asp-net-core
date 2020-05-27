$(document).ready(function () {

    $(".alert-danger").click(function (e) {
        var result = confirm("Tem certeza que deseja excluir este registro?");

        if (result == false) {
            e.preventDefault();
        }
    });
});