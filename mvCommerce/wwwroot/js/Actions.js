$(document).ready(function () {

    $(".alert-danger").click(function (e) {
        var result = confirm("Deseja realmente concluir esta ação?");

        if (result == false) {
            e.preventDefault();
        }
    });
    $('.money').mask('000.000.000.000.000,00', { reverse: true });

    AjaxUploadImageProduct();
});

function AjaxUploadImageProduct() {
    $(".img-upload").click(function () {
        $(this).parent().find(".input-file").click();
    });

    $(".input-file").change(function () {
        //Form of data js
        var binary = $(this)[0].files[0];
        var form = new FormData();
        form.append("file", binary);

        $.ajax({
            type: "POST",
            url: "/Collaborator/Image/Storage",
            data: form,
            contentType: false,
            processData: false,
            error: function () {
                alert("Erro ao enviar imagem");
            },
            success: function (data) {
                alert("Arquivo enviado com sucesso" + data.path);
            }
        })
    });
}