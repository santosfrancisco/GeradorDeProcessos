$(document).ready(function () {
	$("#status").hide();
	$("#loading").hide();
	$("#btnLogar").click(function () {

		$("#status").removeClass('alert-info');
		$("#status").removeClass('alert-success');
		$("#status").removeClass('alert-warning');
		$.ajax({
			data: { Login: $("#txtLogin").val(), Senha: $("#txtSenha").val() },
			dataType: "json",
			type: "POST",
			url: "/Autenticacao/AutenticacaoDeUsuario",
			async: true,
			beforeSend: function () {
				$("#status").html("Estamos autenticando o usuário... Só um instante.");
				$("#status").addClass('alert-info');
				$("#loading").show();
				$("#status").show();
			},
			success: function (dados) {
				if (dados.OK) {
					setTimeout(function () { window.location.href = "/Home/Index" }, 5000);
					$("#status").removeClass('alert-info');
					$("#status").addClass('alert-success');
					$("#status").html(dados.Mensagem);
					$("#status").show();
				}
				else {
					$("#loading").hide();
					$("#status").removeClass('alert-info');
					$("#status").addClass('alert-warning');
					$("#status").html(dados.Mensagem);
					$("#status").show();
				}
			},
			error: function () {
				$("#status").addClass('alert-warning');
				$("#status").html(dados.Mensagem);
				$("#status").show()
			}
		});
	});
});