$(document).ready(function () {
    $("#status").hide();
    $("#btLogar").click(function () {
        $.ajax({
            data: { Login: $("#txtLogin").val(), Senha: $("#txtSenha").val() },
            dataType: "json",
            type: "GET",
            url: "/Home/AutenticarLogin",
            async: true,
            beforeSend: function () {
                $("#status").html("Estamos autenticando o usuário... Só um instante.");
                $("#status").show();
            },
            success: function (dados) {
                if (dados.OK) {
                    setTimeout(function () { window.location.href = "/Empreendimentos/Index" }, 5000);
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