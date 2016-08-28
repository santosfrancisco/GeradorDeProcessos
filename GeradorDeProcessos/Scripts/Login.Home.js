$(document).ready(function () {
	$("#status").hide();
	$("#btnLogar").click(function () {
		$.ajax({
			data: { Login: $("#txtLogin").val(), Senha: $("#txtSenha").val() },
			dataType: "json",
			type: "POST",
			url: "/Home/AutenticacaoDeUsuario",
			async: true,
			beforeSend: function () {
				$("#status").html("Estamos autenticando o usuário... Só um instante.");
				$("#status").show();
			},
			success: function (dados) {
				if (dados.OK) {
					setTimeout(function () { window.location.href = "/Home/Index" }, 3000);
					$("#status").html(dados.Mensagem)
					$("#status").show();
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