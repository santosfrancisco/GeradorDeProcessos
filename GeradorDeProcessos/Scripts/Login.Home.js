$(document).ready(function () {
    $("#status").hide();
    $("#btnLogar").click(function () {
        $.ajax({
            data: { Email: $("#txtLogin").val(), Senha: $("#txtSenha").val() },
            dataType: "json",
            type: "GET",
            url: "/Usuarios/AutenticacaoDeUsuario",
            async: true,
            beforeSend: function () {
                $("#status").html("Estamos autenticando o usuário... Só um instante.");
                $("#status").show();
            },
            success: function (dados) {
                if (dados.OK) {
                    setTimeout(function () { window.location.href = "/Home/Index" }, 5000);
                }
                else {
                    $("#status").html(dados.Mensagem);
                    $("#status").show();
                }
            },
            error: function () {
                $("#status").html(dados.Mensagem);
                $("#status").show()
            }
        });
    });
});