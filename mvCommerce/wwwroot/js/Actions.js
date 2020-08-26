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
    $(".btn-image-delete").click(function () {

        var fieldHidden = $(this).parent().find("input[name=image]");
        var image = $(this).parent().find(".img-upload");
        var btnDelete = $(this).parent().find(".btn-image-delete");
        var inputFile = $(this).parent().find(".input-file");

        $.ajax({
            type: "GET",
            url: "/Collaborator/Image/Delete?path=" + fieldHidden.val(),
            error: function () {

            },
            success: function () {
                image.attr("src", "/img/image-default.png");
                btnDelete.addClass("btn-hide");
                fieldHidden.val("");
                inputFile.val("");
            }
        });
    });

    $(".input-file").change(function () {
        //Form of data js
        var binary = $(this)[0].files[0];
        var form = new FormData();
        form.append("file", binary);


        var fieldHidden = $(this).parent().find("input[name=image]");
        var image = $(this).parent().find(".img-upload");
        var btnDelete = $(this).parent().find(".btn-image-delete");

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
                var path = data.path;
                image.attr("src", path);
                fieldHidden.val(path);
                btnDelete.removeClass("btn-hide")
            }
        })
    });
}