using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeradorDeProcessos.Models
{
	[MetadataType(typeof(EmpresasMetadado))]
	public partial class Empresas
	{
	}

	public class EmpresasMetadado
	{
		[DisplayName("Empresa")]
		public int IDEmpresa { get; set; }

		[Required(ErrorMessage = "Obrigatório informar nome da empresa")]
		[StringLength(80, ErrorMessage = "Nome da cidade deve possuir no máximo 80 caracteres")]
		public string Nome { get; set; }
	}
}