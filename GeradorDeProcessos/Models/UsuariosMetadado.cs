using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeradorDeProcessos.Models
{
	[MetadataType(typeof(UsuariosMetadado))]
	public partial class Usuarios
	{
	}

	public class UsuariosMetadado
	{
		[DisplayName("Usuário")]
		public int IDUsuario { get; set; }
		[Required(ErrorMessage = "Obrigatório informar nome do usuário")]
		[StringLength(80, ErrorMessage = "Nome do usuário deve possuir no máximo 80 caracteres")]
		public string Nome { get; set; }
		[Required(ErrorMessage = "Obrigatório informar o e-mail")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Obrigatório informar uma senha")]
		[DataType(DataType.Password)]
		public string Senha { get; set; }
		[DisplayName("Empresa")]
		[Required(ErrorMessage = "Obrigatório selecionar a empresa")]
		public int IDEmpresa { get; set; }
		[DisplayName("Tipo de usuário")]
		[Required(ErrorMessage = "Obrigatório selecionar o tipo do usuário")]
		public Nullable<int> TipoUsuario { get; set; }
	}
}